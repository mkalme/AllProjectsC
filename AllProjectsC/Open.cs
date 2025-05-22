using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AllProjectsC
{
    class Open
    {
        //Open
        public static Model.Catalog OpenProjects(string path) {
            Model.Catalog catalog = new Model.Catalog();

            string[] allProjects = getAllProjects(path);

            for (int i = 0; i < allProjects.Length; i++) {
                Model.Project project = new Model.Project(
                    Path.GetFileName(allProjects[i]),
                    getLinesOfCode(allProjects[i]),
                    getCreationDate(allProjects[i])
                );

                catalog.Projects.Add(project);
            }

            catalog.Projects = catalog.Projects.OrderBy(Project => Project.CreationDate).ToList();

            return catalog;
        }

        private static string[] getAllProjects(string path) {
            return Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
        }

        //Get Lines Of Code
        private static int getLinesOfCode(string path) {
            List<string> fileNames = getFileNames(path);

            int linesOfCode = getLines(
                getRegularFiles(fileNames),
                getDesignerFiles(fileNames)  
            );

            return linesOfCode;
        }

        private static List<string> getFileNames(string path)
        {
            string[] directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

            List<List<string>> allProjects = new List<List<string>>();

            for (int i = 0; i < directories.Length; i++) {
                if (Path.GetFileName(directories[i]) != ".vs") {
                    string projectPath = path + @"\" + Path.GetFileName(directories[i]);

                    allProjects.Add(getProjectFiles(projectPath));
                }
            }

            return allProjects.SelectMany(x => x).ToList();
        }
        public static List<string> getProjectFiles(string path) {
            string[] allSubDierctories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            List<string[]> allFiles = new List<string[]>();

            //Get Top Files
            allFiles.Add(Directory.GetFiles(path, "*.cs", SearchOption.TopDirectoryOnly));

            //Get Sub Files
            for (int i = 0; i < allSubDierctories.Length; i++)
            {
                if (!new string[] { "bin", "obj", "Properties", "Resources" }.Contains(Path.GetFileName(allSubDierctories[i])))
                {
                    string[] array = Directory.GetFiles(allSubDierctories[i], "*.cs", SearchOption.AllDirectories);

                    allFiles.Add(array);
                }
            }

            List<string> allFileList = new List<string>();
            for (int i = 0; i < allFiles.Count; i++)
            {
                for (int b = 0; b < allFiles[i].Length; b++)
                {
                    allFileList.Add(allFiles[i][b]);
                }
            }

            return allFileList;
        }

        private static List<string> getRegularFiles(List<string> fileNames) {
            List<string> regularFiles = new List<string>();

            for (int i = 0; i < fileNames.Count; i++)
            {
                string fileName = Path.GetFileName(fileNames[i]);

                if (!fileName.EndsWith(".Designer.cs"))
                {
                    regularFiles.Add(fileNames[i]);
                }
            }

            return regularFiles;
        }
        private static List<string> getDesignerFiles(List<string> fileNames) {
            List<string> designerFiles = new List<string>();

            for (int i = 0; i < fileNames.Count; i++)
            {
                string fileName = Path.GetFileName(fileNames[i]);

                if (fileName.EndsWith(".Designer.cs"))
                {
                    designerFiles.Add(fileNames[i]);
                }
            }

            return designerFiles;
        }

        private static int getLines(List<string> regularFiles, List<string> designerFiles)
        {
            bool ifFormsApp = designerFiles.Count > 0 ? true : false;

            int linesOfCode = 0;
            for (int i = 0; i < regularFiles.Count; i++)
            {
                if (ifFormsApp)
                {
                    if (Path.GetFileName(regularFiles[i]) != "Program.cs")
                    {
                        linesOfCode += File.ReadLines(regularFiles[i]).Count();
                    }
                }
                else
                {
                    linesOfCode += File.ReadLines(regularFiles[i]).Count();
                }
            }

            return linesOfCode;
        }

        //Get Creation Date
        private static DateTime getCreationDate(string path) {
            return Directory.GetCreationTime(path);
        }
    }
}

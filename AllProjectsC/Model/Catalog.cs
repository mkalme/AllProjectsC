using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProjectsC.Model
{
    class Catalog
    {
        public List<Project> Projects { get; set; }
        public string ProjectFolder { get; set; }

        public Catalog() {
            Projects = new List<Project>();
            ProjectFolder = "";
        }
        public Catalog(List<Project> projects, string projectFolder) {
            this.Projects = projects;
            this.ProjectFolder = projectFolder;
        }
    }
}

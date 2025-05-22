using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace AllProjectsC
{
    public partial class Form1 : Form
    {
        private static Model.Catalog catalog;

        public Form1()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            pathTextBox.Text = folderBrowserDialog1.SelectedPath;
        }

        private void Load_DataGridView() {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < catalog.Projects.Count; i++) {
                dataGridView1.Rows.Add(
                    catalog.Projects[i].Name,
                    catalog.Projects[i].LinesOfCode,
                    catalog.Projects[i].CreationDate.ToString("MMM d, yyyy", new CultureInfo("en-GB"))
                );
            }

            dataGridView1.ClearSelection();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            catalog = Open.OpenProjects(pathTextBox.Text);

            Load_DataGridView();
        }

        private void SaveToCliboardLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string text = "";

            for (int i = 0; i < catalog.Projects.Count; i++) {
                text += catalog.Projects[i].Name + "\t" + catalog.Projects[i].LinesOfCode + "\t" + catalog.Projects[i].CreationDate.ToString("dd/MM/yyyy");
                text += i == catalog.Projects.Count - 1 ? "" : "\n";
            }

            Clipboard.SetText(text);
        }
    }
}

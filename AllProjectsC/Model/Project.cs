using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllProjectsC.Model
{
    class Project
    {
        public string Name { get; set; }
        public int LinesOfCode { get; set; }
        public DateTime CreationDate { get; set; }

        public Project() {
            Name = "";
            LinesOfCode = 0;
            CreationDate = new DateTime();
        }
        public Project(string name, int linesOfCode, DateTime creationDate) {
            this.Name = name;
            this.LinesOfCode = linesOfCode;
            this.CreationDate = creationDate;
        }
    }
}

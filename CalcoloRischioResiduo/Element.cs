using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcoloRischioResiduo
{
    public class Element
    {
        private bool classified = true;


        public FunctionalPerimeters functionalperimeter { get; set; }


        public bool isClassified()
        {
            return classified;
        }

    }
}

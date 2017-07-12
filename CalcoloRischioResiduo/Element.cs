using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcoloRischioResiduo
{
    public class Element
    {
        private bool cited = false;



        public FunctionalPerimeters functionalperimeter { get; set; }


        public Element()
        {
            cited = true;
        }


        public bool isClassified()
        {
            return cited;
        }

        public double CalculateResidualRisk()
        {
            return 1250;
        }

    }
}

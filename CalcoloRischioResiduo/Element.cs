using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CalcoloRischioResiduo.FunctionalPerimeters;
using CalcoloRischioResiduo.RiskAssessment;

namespace CalcoloRischioResiduo
{
    public class Element
    {
        private bool _classified = false;
        private SlimVCI _vci = null;
        private SlimPDS _pds = null;

        private PerimetersAnalysis _perimeters = null;

        public Types Perimeter { get; set; }

        public Element()
        {
            Initialize(false, null, null);
        }

        public Element(bool isClassified)
        {
            Initialize(isClassified, null, null);
        }

        public Element(bool isClassified, SlimVCI vci)
        {
            Initialize(isClassified, vci, null);
        }
        public Element(bool isClassified, SlimVCI vci, SlimPDS pds)
        {
            Initialize(isClassified, vci, pds);
        }

        public Element(SlimVCI vci)
        {
            Initialize(true, vci, null);
            
        }

        public Element(SlimVCI vci, SlimPDS pds)
        {
            Initialize(true, vci, pds);
        }

        private void Initialize(bool isClassified, SlimVCI vci, SlimPDS pds)
        {
            _classified = isClassified;
            _vci = vci;
            _pds = pds;
        }

        public void Classify()
        {
            _classified = true;
        }

        public void AssociateWith(PerimetersAnalysis perimeters)
        {
            _perimeters = perimeters;
        }

        public double EstimateResidualRisk()
        {
            Perimeter perimeter = _perimeters.FindByType(Perimeter);
            if (HasPDS())
            {
                return _pds.GetResidualRiskValue();
            }
            else if (HasVCI())
            {
                return _vci.GetVCIValue();
            }
            else if (perimeter != null)
            {
                return perimeter.EstimatedResidualRisk(_classified);
            }
            else
            {
                return SlimVCI.VCIMAX;
            }
        }

        public bool IsClassified()
        {
            return _classified;
        }

        public bool HasVCI()
        {
            return _vci != null;
        }
        public bool HasPDS()
        {
            return _pds != null;
        }

        public bool IsComplete()
        {
            return IsClassified() && HasVCI() && HasPDS();
        }

        public bool IsIncomplete()
        {
            return IsClassified() && HasVCI() && !HasPDS();
        }

        public bool IsAbsent()
        {
            return (IsClassified() && !HasVCI()) || !IsClassified();
        }

    }
}

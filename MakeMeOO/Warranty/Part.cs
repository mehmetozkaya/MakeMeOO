using System;

namespace MakeMeOO.Warranty
{
    internal class Part
    {
        public DateTime InstallmentDate { get; }
        public DateTime DefectDetectedOn { get; set; }

        public Part(DateTime installmentDate)
        {
            this.InstallmentDate = installmentDate;
        }

        public void MarkDefective(DateTime withDate)
        {
            this.DefectDetectedOn = withDate;
        }
    }
}

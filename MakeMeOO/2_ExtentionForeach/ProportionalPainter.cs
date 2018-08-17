using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMeOO._2_ExtentionForeach
{
    public class ProportionalPainter : IPainter
    {
        public TimeSpan TimePerSqMeter { get; set; }
        public double DollarsPerHour { get; set; }
        public bool IsAvailable => true;
        public double EstimateCompensation(double sqMeters) => 
            this.EstimateTimeToPaint(sqMeters).TotalHours * this.DollarsPerHour;
        public TimeSpan EstimateTimeToPaint(double sqMeters) => 
            TimeSpan.FromHours(this.TimePerSqMeter.TotalHours * sqMeters);
    }
}

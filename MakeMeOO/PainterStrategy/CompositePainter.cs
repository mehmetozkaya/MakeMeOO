using System;
using System.Collections.Generic;
using System.Linq;

namespace MakeMeOO.PainterStrategy
{
    internal class CompositePainter<TPainter> : IPainter
        where TPainter : IPainter
    {
        private IEnumerable<TPainter> Painters { get; }

        protected Func<double, IEnumerable<TPainter>, IPainter> Reduce { get; set; }

        public bool IsAvailable =>
            this.Painters.Any(painter => painter.IsAvailable);

        public CompositePainter(IEnumerable<TPainter> painters,
                               Func<double, IEnumerable<TPainter>, IPainter> reduce)
           : this(painters)
        {
            this.Reduce = reduce;
        }

        protected CompositePainter(IEnumerable<TPainter> painters)
        {
            this.Painters = painters.ToList();
        }

        public TimeSpan EstimateTimeToPaint(double sqMeters) =>
            this.Reduce(sqMeters, this.Painters).EstimateTimeToPaint(sqMeters);

        public double EstimateCompensation(double sqMeters) =>
            this.Reduce(sqMeters, this.Painters).EstimateCompensation(sqMeters);
    }
}

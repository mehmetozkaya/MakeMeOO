using System.Collections.Generic;
using System.Linq;

namespace MakeMeOO.PainterStrategy
{
    internal class Painters
    {
        private IEnumerable<IPainter> ContainedPainters { get; }

        public Painters(IEnumerable<IPainter> painters)
        {
            this.ContainedPainters = painters.ToList();
        }

        public Painters GetAvailable() =>
            new Painters(this.ContainedPainters.Where(painter => painter.IsAvailable));

        public IPainter GetCheapestOne(double sqMeters) =>
            this.ContainedPainters.WithMinimum(painter => painter.EstimateCompensation(sqMeters));

        public IPainter GetFastestOne(double sqMeters) =>
            this.ContainedPainters.WithMinimum(painter => painter.EstimateTimeToPaint(sqMeters));
    }
}

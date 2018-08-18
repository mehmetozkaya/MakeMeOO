namespace MakeMeOO.PainterStrategy
{
    public class PaintingTask<TPainter> where TPainter : IPainter
    {
        public TPainter Painter { get; }
        public double SquareMeters { get; }

        public PaintingTask(TPainter painter, double sqMeters)
        {
            this.Painter = painter;
            this.SquareMeters = sqMeters;
        }
    }
}

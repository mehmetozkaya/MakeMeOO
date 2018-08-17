using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MakeMeOO._2_ExtentionForeach
{
    public class CompositePainter<TPainter> : IPainter
        where TPainter : IPainter
    {
        private IEnumerable<TPainter> Painters { get; }

        public bool IsAvailable => this.Painters.Any(painter => painter.IsAvailable);

        private Func<double, IEnumerable<TPainter>, IPainter> Reduce { get; }

        public CompositePainter(IEnumerable<TPainter> painters, 
                                Func<double, IEnumerable<TPainter>, IPainter> reduce)
        {
            this.Painters = painters.ToList();
            this.Reduce = reduce;
        }
        public TimeSpan EstimateTimeToPaint(double sqMeters) =>
            this.Reduce(sqMeters, this.Painters).EstimateTimeToPaint(sqMeters);

        public double EstimateCompensation(double sqMeters) =>
            this.Reduce(sqMeters, this.Painters).EstimateCompensation(sqMeters);
    }

    class Reason
    {
        private void For()
        {
            TestDelegate test = (x) => { Console.WriteLine(x); };

            del test2 = x => x * x;

            Expression<del> myET = x => x * x;

            Func<int, int, bool> myFunc = (x, y) => x == y;

            var isreal = myFunc(3, 3);

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);

            var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);

            var firstSmallNumbers = numbers.TakeWhile((n, index) => n >= index);

            Func<int,int> asd = n => n * n;
        }

        delegate int del(int i);
        delegate void TestDelegate(string s);
    }
}

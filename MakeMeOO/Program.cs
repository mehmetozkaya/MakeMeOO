using MakeMeOO.PainterStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMeOO
{
    class Program
    {
        static void Main(string[] args)
        {
            #region AccountState
            //Account account = new Account(() => Console.WriteLine("Account operations run"));
            //var amount = new decimal(100.0);
            //account.Deposit(amount);            
            //account.Withdraw(59);
            //account.Freeze();
            //account.Close();
            #endregion

            IEnumerable<ProportionalPainter> painters = new List<ProportionalPainter>
                {
                    new ProportionalPainter { DollarsPerHour = 10, TimePerSqMeter = new TimeSpan(1,1,1) },
                    new ProportionalPainter { DollarsPerHour = 9, TimePerSqMeter = new TimeSpan(2,1,1) },
                    new ProportionalPainter { DollarsPerHour = 8, TimePerSqMeter = new TimeSpan(3,1,1) },
                    new ProportionalPainter { DollarsPerHour = 6, TimePerSqMeter = new TimeSpan(4,1,1) },
                    new ProportionalPainter { DollarsPerHour = 5, TimePerSqMeter = new TimeSpan(5,1,1) }
                };
            
            IPainter painter = CompositePainterFactories.CombineProportional(painters);

            var asd = painter.EstimateTimeToPaint(10);
            var bsd = painter.EstimateCompensation(10);
        }
    }
}

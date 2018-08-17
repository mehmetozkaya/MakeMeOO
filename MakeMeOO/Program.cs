using MakeMeOO._2_ExtentionForeach;
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
            //Account account = new Account(() => Console.WriteLine("Account operations run"));
            //var amount = new decimal(100.0);
            //account.Deposit(amount);            
            //account.Withdraw(59);
            //account.Freeze();
            //account.Close();

            IEnumerable<ProportionalPainter> painters = new ProportionalPainter[10];
            IPainter painter = CompositePainterFactories.CreateGroup(painters);


        }


        private static IPainter FindCheapestPainter(double sqMeters, IEnumerable<IPainter> painters)
        {
            return
                painters
                    .Where(painter => painter.IsAvailable)
                    .WithMinimum(painter => painter.EstimateCompensation(sqMeters));
        }
    }
}

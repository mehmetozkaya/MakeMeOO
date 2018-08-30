using MakeMeOO.PainterStrategy;
using MakeMeOO.Warranty;
using MakeMeOO.Warranty.Common;
using MakeMeOO.Warranty.Common.Interfaces;
using MakeMeOO.Warranty.Rules;
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

            #region PainterStrategy
            //IEnumerable<ProportionalPainter> painters = new List<ProportionalPainter>
            //    {
            //        new ProportionalPainter { DollarsPerHour = 10, TimePerSqMeter = new TimeSpan(1,1,1) },
            //        new ProportionalPainter { DollarsPerHour = 9, TimePerSqMeter = new TimeSpan(2,1,1) },
            //        new ProportionalPainter { DollarsPerHour = 8, TimePerSqMeter = new TimeSpan(3,1,1) },
            //        new ProportionalPainter { DollarsPerHour = 6, TimePerSqMeter = new TimeSpan(4,1,1) },
            //        new ProportionalPainter { DollarsPerHour = 5, TimePerSqMeter = new TimeSpan(5,1,1) }
            //    };

            //IPainter painter = CompositePainterFactories.CombineProportional(painters);

            //var asd = painter.EstimateTimeToPaint(10);
            //var bsd = painter.EstimateCompensation(10);
            #endregion

            #region Warranty
            //DateTime sellingDate = new DateTime(2018, 6, 9);
            //TimeSpan moneyBackSpan = TimeSpan.FromDays(30);
            //TimeSpan warrantySpan = TimeSpan.FromDays(365);

            //IWarranty moneyBack = new TimeLimitedWarranty(sellingDate, moneyBackSpan);
            //IWarranty warranty = new LifetimeWarranty(sellingDate);

            //SoldArticle goods = new SoldArticle(moneyBack, warranty);
            //ClaimWarranty(goods);

            //Console.ReadLine();
            #endregion

            #region Option

            //IOption<string> name = Option<string>.Some("something");

            //name
            //    .When(s => s.Length > 3).Do(s => Console.WriteLine($"{s} long"))
            //    .WhenSome().Do(s => Console.WriteLine($"{s} short"))
            //    .WhenNone().Do(() => Console.WriteLine("missing"))
            //    .Execute();

            //int length =
            //    name
            //        .When(s => s.Length > 3).MapTo(s => s.Length)
            //        .WhenSome().MapTo(s => 3)
            //        .WhenNone().MapTo(() => 0)
            //        .Map();

            //Console.ReadLine();

            #endregion

            #region WarrantyRule

            IWarranty moneyBackGuraantee =
                new TimeLimitedWarranty(DateTime.Today, TimeSpan.FromDays(7));

            IWarranty expressWarranty =
                new TimeLimitedWarranty(DateTime.Today, TimeSpan.FromDays(365));

            IWarranty circuitryWarranty =
                new LifetimeWarranty(DateTime.Today);

            SoldArticle article = new SoldArticle(moneyBackGuraantee, expressWarranty, new ChristmassRules());
            article.InstallCircuitry(new Part(DateTime.Now), circuitryWarranty);

            article = new SoldArticle(moneyBackGuraantee, expressWarranty, new DefaultRules());
            article.InstallCircuitry(new Part(DateTime.Now), circuitryWarranty);

            Console.ReadLine();

            #endregion

            #region DocumentNumber

            int controlDigit = ControlDigitAlgorithms.ForSalesDepartment.GetControlDigit(12345);
            int controlDigitAccount = ControlDigitAlgorithms.ForAccountingDepartment.GetControlDigit(12345);

            Console.ReadLine();

            #endregion
        }

        private static void ClaimWarranty(SoldArticle article)
        {
            DateTime now = DateTime.Now;

            //article.MoneyBackGuarantee
            //    .Claim(now, () => Console.WriteLine("Offer money back."));

            //article.ExpressWarranty
            //    .Claim(now, () => Console.WriteLine("Offer repair."));
        }

    }
}

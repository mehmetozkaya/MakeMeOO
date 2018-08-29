using MakeMeOO.Warranty.Common;
using MakeMeOO.Warranty.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMeOO.Warranty
{
    internal class SoldArticle
    {
        private IWarranty MoneyBackGuarantee { get; }
        private IWarranty NotOperationalWarranty { get; }
        private IWarranty CircuitryWarranty { get; set; }

        private IOption<Part> Circuitry { get; set; } = Option<Part>.None();
        private IWarrantyRules WarrantyRules { get; }

        public SoldArticle(IWarranty moneyBack, IWarranty express, IWarrantyRulesFactory rulesFactory)
        {
            if (moneyBack == null)
                throw new ArgumentNullException(nameof(moneyBack));
            if (express == null)
                throw new ArgumentNullException(nameof(express));

            this.MoneyBackGuarantee = moneyBack;
            this.NotOperationalWarranty = express;
            this.CircuitryWarranty = VoidWarranty.Instance;

            this.WarrantyRules = rulesFactory.Create(
                this.ClaimMoneyBack, this.ClaimNotOperationalWarranty, this.ClaimCircuitryWarranty);            
        }

        private void ClaimMoneyBack(Action action)
        {
            this.MoneyBackGuarantee.Claim(DateTime.Now, action);
        }

        private void ClaimNotOperationalWarranty(Action action)
        {
            this.NotOperationalWarranty.Claim(DateTime.Now, action);
        }

        private void ClaimCircuitryWarranty(Action action)
        {
            this.Circuitry
                .WhenSome()
                .Do(c => this.CircuitryWarranty.Claim(c.DefectDetectedOn, action))
                .Execute();
        }

        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            this.Circuitry = Option<Part>.Some(circuitry);
            this.CircuitryWarranty = extendedWarranty;
            this.WarrantyRules.CircuitryOperational();
        }

        public void CircuitryNotOperational(DateTime detectedOn)
        {
            this.WarrantyRules.CircuitryFailed();
        }

        public void VisibleDamage()
        {
            this.WarrantyRules.VisiblyDamaged();
        }

        public void NotOperational()
        {
            this.WarrantyRules.NotOperational();
        }

        public void Repaired()
        {
            this.WarrantyRules.Operational();
        }

        public void ClaimWarranty(Action onValidClaim)
        {
            this.WarrantyRules.Claim(onValidClaim);
        }
    }
}

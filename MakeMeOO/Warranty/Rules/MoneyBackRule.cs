using System;

namespace MakeMeOO.Warranty.Rules
{
    class MoneyBackRule : ChainedRule
    {
        public MoneyBackRule(Action<Action> claimAction, IWarrantyRules next)
            : base(next)
        {
            base.Claim = claimAction;
        }

        protected override void HandleVisiblyDamaged()
        {
            base.Claim = base.Forward;
        }

        protected override void HandleNotOperational()
        {
            base.Claim = base.Forward;
        }

        protected override void HandleCircuitryFailed()
        {
            base.Claim = base.Forward;
        }
    }
}

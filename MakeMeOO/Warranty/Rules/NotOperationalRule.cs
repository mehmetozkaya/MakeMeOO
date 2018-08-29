using System;

namespace MakeMeOO.Warranty.Rules
{
    internal class NotOperationalRule : ChainedRule
    {
        private Action<Action> ClaimAction { get; }

        public NotOperationalRule(Action<Action> claimAction, IWarrantyRules next)
            : base(next)
        {
            base.Claim = base.Forward;
            this.ClaimAction = claimAction;
        }

        protected override void HandleNotOperational()
        {
            base.Claim = this.ClaimAction;
        }

        protected override void HandleOperational()
        {
            base.Claim = base.Forward;
        }
    }
}

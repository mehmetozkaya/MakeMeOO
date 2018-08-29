using System;

namespace MakeMeOO.Warranty.Rules
{
    internal class ConditionalRule : IWarrantyRules
    {
        private Func<bool> Predicate { get; }
        private Action<Action> ClaimAction { get; }
        private IWarrantyRules Next { get; }

        public ConditionalRule(Func<bool> predicate, Action<Action> claimAction, IWarrantyRules next)
        {
            this.Predicate = predicate;
            this.ClaimAction = claimAction;
            this.Next = next;
        }

        public void CircuitryOperational()
        {
            this.Next.CircuitryOperational();
        }

        public void CircuitryFailed()
        {
            this.Next.CircuitryFailed();
        }

        public void VisiblyDamaged()
        {
            this.Next.VisiblyDamaged();
        }

        public void NotOperational()
        {
            this.Next.NotOperational();
        }

        public void Operational()
        {
            this.Next.Operational();
        }

        public Action<Action> Claim
        {
            get
            {
                if (this.Predicate())
                    return this.AttemptClaim;
                return this.Forward;
            }
        }

        private void Forward(Action onValidClaim)
        {
            this.Next.Claim(onValidClaim);
        }

        private void AttemptClaim(Action onValidClaim)
        {
            Action<Action> subsequentAction = this.Forward;

            this.ClaimAction(() =>
            {
                onValidClaim();
                subsequentAction = (action) => { };
            });

            subsequentAction(onValidClaim);
        }
    }
}

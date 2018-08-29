using System;

namespace MakeMeOO.Warranty.Rules
{
    internal class NotSatisfiedRule : IWarrantyRules
    {
        public void CircuitryOperational() { }
        public void CircuitryFailed() { }
        public void VisiblyDamaged() { }
        public void NotOperational() { }
        public void Operational() { }

        public Action<Action> Claim => (action) => { };
    }
}

using System;

namespace MakeMeOO.Warranty
{
    public interface IWarrantyRules
    {
        void CircuitryOperational();
        void CircuitryFailed();
        void VisiblyDamaged();
        void NotOperational();
        void Operational();
        Action<Action> Claim { get; }
    }
}

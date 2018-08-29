using System;

namespace MakeMeOO.Warranty
{
    public interface IWarrantyRulesFactory
    {
        IWarrantyRules Create(Action<Action> claimMoneyBack,
                              Action<Action> claimNotOperationalWarranty,
                              Action<Action> claimCircuitryWarranty);
    }
}

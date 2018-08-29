using System;

namespace MakeMeOO.Warranty.Rules
{
    internal class ChristmassRules : IWarrantyRulesFactory
    {
        public IWarrantyRules Create(Action<Action> claimMoneyBack, Action<Action> claimNotOperationalWarranty, Action<Action> claimCircuitryWarranty) =>
               new ConditionalRule(IsAroundChristmas, claimMoneyBack,
                   new DefaultRules().Create(claimMoneyBack,
                                             claimNotOperationalWarranty,
                                             claimCircuitryWarranty));

        private static bool IsAroundChristmas() =>
            DateTime.Now.Month == 12;
    }
}

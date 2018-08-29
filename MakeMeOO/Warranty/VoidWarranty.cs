using System;

namespace MakeMeOO.Warranty
{
    internal class VoidWarranty : IWarranty
    {
        [ThreadStatic]
        private static VoidWarranty _instance;

        private VoidWarranty() { }

        public static VoidWarranty Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VoidWarranty();
                return _instance;
            }
        }


        public void Claim(DateTime onDate, Action onValidClaim) { }
    }
}

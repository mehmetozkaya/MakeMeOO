using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMeOO.Warranty
{
    public interface IWarranty
    {
        void Claim(DateTime onDate, Action onValidClaim);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defaults
{
    public sealed class Default
    {
        public static Default Instance => instance.Value;

        private Default()
        {

        }

        public TInterface Get<TInterface>()
        {
            
        }

        private static readonly Lazy<Default> instance = new Lazy<Default>(() => new Default(), true);
    }
}

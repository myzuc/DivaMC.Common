using DivaMC.Common.Common;
using DivaMC.Common.Registries;

namespace DivaMC.Common.IO.Protocol
{
    public sealed class Context
    {
        public required Dictionary<Identifier, Registry> Registries { get; set; }
        public Context()
        {
            
        }
    }
}
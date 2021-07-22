using System;
using Chameleon;

namespace Chameleon.Application
{
    public class MenuLinker : Linker<MenuItem, MenuItem>
    {
        public override (Type destination, MenuItem condition) CreateCondition(MenuItem sourceRecord)
        {
            return (System.Reflection.Assembly.GetEntryAssembly().GetType(sourceRecord.Id), null);
        }
    }
}

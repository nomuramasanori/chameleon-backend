using System;

namespace Chameleon
{
	public class HyperLinkAttribute : FieldAttribute, IHyperLinkOption
    {
        public string IconName { get; set; } = "Help";
    }
}

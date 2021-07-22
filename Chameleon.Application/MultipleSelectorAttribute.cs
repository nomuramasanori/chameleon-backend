using System;

namespace Chameleon
{
    public class MultipleSelectorAttribute : FieldAttribute, IMultipleSelectOption
    {
        public bool Grouping { get; set; } = false;

        public string GetItemMethod { get; set; }

        public MultipleSelectorAttribute(string getItemMethod)
        {
            this.GetItemMethod = getItemMethod;
        }
	}
}

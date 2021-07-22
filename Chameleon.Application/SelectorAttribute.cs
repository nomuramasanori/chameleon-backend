using System;

namespace Chameleon
{
    public class SelectorAttribute : FieldAttribute
    {
        public string GetAllItemMethod { get; set; }

        public string GetItemMethod { get; set; }

        public SelectorAttribute(string getAllItemMethod, string getItemMethod)
        {
            this.GetAllItemMethod = getAllItemMethod;
            this.GetItemMethod = getItemMethod;
        }
	}
}

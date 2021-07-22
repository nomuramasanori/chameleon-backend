using System;

namespace Chameleon
{
    public class DropdownItem
    {
        private string id;
        private string name;

        public string Id
        {
            get
            {
                return this.id ?? this.name;
            }

            set
            {
                this.id = value;
            }
        }
        public string Name
        {
            get
            {
                //return this.Id + ":" + this.name;
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Group{ get; set; } 

		public DropdownItem() { }
		public DropdownItem(string id, string name)
		{
			this.Id = id;
			this.name = name;
		}
	}
}

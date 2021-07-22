using System;

namespace Chameleon
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ListItemAttribute : System.Attribute
    {
        public ListItemType Type { get; set; }

        public ListItemAttribute(ListItemType type)
        {
            this.Type = type;
        }
    }

    public enum ListItemType
    {
        None,
        Image,
        Title,
        Body1,
        Body2
    }
}

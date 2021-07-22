using System;

namespace Chameleon
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class FieldAttribute : Attribute
    {
        public string Name { get; set; } = "";

        public bool ReadOnly { get; set; } = false;

        public bool Visible { get; set; } = true;

        public int Width { get; set; } = 12;

        public bool Required { get; set; } = false;
	}
}

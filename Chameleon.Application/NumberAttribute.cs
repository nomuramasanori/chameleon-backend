using System;

namespace Chameleon
{
	public class NumberAttribute : FieldAttribute, INumberOption
    {
        public double Max { get; set; } = double.MaxValue;

        public double Min { get; set; } = double.MinValue;

        public int DisplayDigit { get; set; } = 0;
	}
}

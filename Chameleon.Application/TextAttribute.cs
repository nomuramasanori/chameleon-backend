using System;

namespace Chameleon
{
	public class TextAttribute : FieldAttribute, ITextOption
	{
        public int Length { get; set; } = 0;

        public int MinLength { get; set; } = 0;

        public int MaxLength { get; set; } = 20;
    }
}

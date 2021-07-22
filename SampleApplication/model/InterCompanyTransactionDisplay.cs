using System;
using Chameleon;

namespace BusinessClass.Entity
{
	public class InterCompanyTransactionDisplay
	{
		[Text(Name = "勘定科目hoge", ReadOnly = true)]
		public string Account { get; set; }
	}
}

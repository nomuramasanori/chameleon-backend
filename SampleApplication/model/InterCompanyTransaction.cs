using System;
using System.Data;
using System.Collections.Generic;
using Chameleon;
using BusinessClass.Entity;

namespace BusinessClass
{
	using CoreData = InterCompanyTransactionCore;
	using DisplayData = InterCompanyTransactionDisplay;

	public class InterCompanyTransaction : Grid<CoreData, DisplayData>
	{
		//public InterCompanyTransaction(Plugin plugin) : base(plugin) {}
		public InterCompanyTransaction(string id = null) : base(id) { }

		protected override List<CoreData> ConvertDisplayDataToCoreData(List<DisplayData> displayData)
		{
			return new List<CoreData>();
		}

		protected override List<DisplayData> ConvertCoreDataToDisplayData(List<CoreData> coreData)
		{
			return new List<DisplayData>();
		}

		protected override List<CoreData> GetData(IDbTransaction transaction)
		{
			return new List<CoreData>();
		}
	}
}

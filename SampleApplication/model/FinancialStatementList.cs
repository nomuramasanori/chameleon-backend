using System;
using System.Collections.Generic;
using Chameleon;
using System.Data;

namespace BusinessClass
{
	using CoreData = FinancialStatementCore;
	using DisplayData = FinancialStatementDisplay;

	public class FinancialStatementList : Grid<DisplayData, DisplayData>
	{
        private string hoge;
        
		public FinancialStatementList(string hoge, string id = null) : base("リスト", id, "list")
		{
            this.hoge = hoge;
		}

		protected override List<DisplayData> ConvertDisplayDataToCoreData(List<DisplayData> displayData)
		{
			return displayData;
		}

		protected override List<DisplayData> ConvertCoreDataToDisplayData(List<DisplayData> coreData)
		{
			return coreData;
		}

		protected override List<DisplayData> GetData(IDbTransaction transaction)
		{
			List<DisplayData> result = new List<DisplayData>();

			Random random = new Random();
			for (int i = 1; i <= 10; i++)
			{
				DisplayData row = new DisplayData();
                row.Company = this.hoge;
                row.Account = "1111111";
                row.AmountMonth1 = (Int16)(i * 100 + 1);
				row.AmountMonth3 = i * 100 + 3;
				row.AmountMonth2 = i * 100 + 2;
				row.AmountMonth4 = i * 100 + 4;
				row.AmountMonth5 = i * 100 + 5;
                row.floatNumber = (float)(i * 100.01);
                row.doubleNumber = i * 100.01;
                row.decimalNumber = (decimal)(i * 100.001);
                //if (i % 3 == 0) row.ReadOnly = true;
                row.desription1 = this.hoge;
                row.Picture = "https://localhost:44335/image/BusinessClass.Budget.png";
                result.Add(row);
			}

			return result;
		}
	}
}
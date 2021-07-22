using System;
using System.Data;
using System.Collections.Generic;
using Chameleon;

namespace BusinessClass
{
	using CoreData = FinancialStatementCore;
	using DisplayData = FinancialStatementDisplay;

	public class FinancialStatement : Grid<DisplayData, DisplayData>
	{
        private string hoge;
        
		public FinancialStatement(string hoge, string id = null) : base("財務諸表", id)
		{
            this.hoge = hoge;
			//this.SetKey(nameof(Company));
			//this.SetKey(nameof(Account));
			//this.SetKey(typeof(Description).FullName);
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

			//var repo = new FinancialStatementRepository(this.DbContext);
			//return repo.Select().Select(rec => new CoreData { Account = rec.Account, Amount = rec.Amount, Month = rec.Month }).ToList();

			Random random = new Random();
			for (int i = 1; i <= 10; i++)
			{
				DisplayData row = new DisplayData();
                //row.Account = "accout" + random.Next(1, 30).ToString() + this.GetParameterOne(ParameterKeys.ACCOUNT);
                //row.Company = this.GetValueOne(nameof(Company));
                row.Company = this.hoge;
                //row.Account = this.GetValueOne(ParameterKeys.ACCOUNT);
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
				//row.desription1 = this.GetValueOne(typeof(Description).FullName) + "が検索条件に入力されました";
                row.desription1 = this.hoge;
                row.Picture = "hoge.png";
                row.MultipleSelect = new string[] { "mul1", "mul2" };
                result.Add(row);
			}

			return result;
		}

		public override void Calculate(IDbTransaction transaction)
		{
			this.CoreData.ForEach(row => row.Total = row.AmountMonth1 + row.AmountMonth2 + row.AmountMonth3 + row.AmountMonth4 + row.AmountMonth5);
		}

		protected override void Save(IDbTransaction transaction) { }

        public override void ValidateAfterCalculation(ref ValidationResults results, IDbConnection connection)
        {
            base.ValidateAfterCalculation(ref results, connection);

			for (int i = 0; i < this.DisplayData.Count; i++)
			{
				if(this.DisplayData[i].AmountMonth1 == this.DisplayData[i].AmountMonth3)
				{
					results.Add(this.Id, nameof(FinancialStatementDisplay.AmountMonth1), i, "hogehoge", ValidationResults.ValidationResultLevel.Error);
				}

                if(this.DisplayData[i].Total > 1000000000)
                {
                    results.Add(this.Id, nameof(FinancialStatementDisplay.Total), i, "big number", ValidationResults.ValidationResultLevel.Warning);
                }
			}
		}
	}
}
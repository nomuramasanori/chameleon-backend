using System;
using Chameleon;
using System.Data;

namespace BusinessClass
{
	//[Form("勘定科目の登録フォーム")]
	public class AccountRegister : Form<FinancialStatementDisplay>
	{
		public AccountRegister() : base("勘定科目の登録フォーム")
		{
            //this.SetKey(nameof(Description));
        }

		protected override FinancialStatementDisplay GetData(IDbTransaction transaction)
		{
			var result = new FinancialStatementDisplay();
            result.Account = "4444444";
            result.Company = "yyyyyy";
            result.AmountMonth1 = 100 + 1;
			result.AmountMonth3 = 100 + 3;
			result.AmountMonth2 = 100 + 2;
			result.AmountMonth4 = 100 + 4;
			result.AmountMonth5 = 100 + 5;
            result.desription1 = "fizz";
            //result.desription2 = this.GetValueOne(nameof(Description));
            result.Picture = "https://localhost:44335/image/BusinessClass.Budget.png";
            result.MultipleSelect = new string[] { "001", "002" };
            return result;
		}

		public override void Calculate(IDbTransaction transaction)
		{
			this.Entity.Total = this.Entity.AmountMonth1 + this.Entity.AmountMonth2 + this.Entity.AmountMonth3 + this.Entity.AmountMonth4 + this.Entity.AmountMonth5;
		}

        public override void ValidateAfterCalculation(ref ValidationResults results, IDbConnection connection)
		{
            base.ValidateAfterCalculation(ref results, connection);

			if(this.Entity.AmountMonth3 == this.Entity.AmountMonth1)
			{
				results.Add(this.Id, nameof(this.Entity.AmountMonth3), 0, "AmountMonth1とAmountMonth3が同値です", ValidationResults.ValidationResultLevel.Error);
			}

            if(this.Entity.Total > 1000000000)
            {
                results.Add(this.Id, nameof(this.Entity.Total), 0, "big number", ValidationResults.ValidationResultLevel.Warning);
            }
		}
    }
}

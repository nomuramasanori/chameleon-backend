using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;

namespace BusinessClass
{
	public class Budget : Content<FinancialStatementDisplay>
	{
        private AccountRegister accountRegister;
        private FinancialStatement financialStatement1;
        private FinancialStatementList financialStatementList;

        public Budget(): base("予算", showAsMenu: true)
        {
            
        }

		public override Layout GetLayout()
		{
            var layout = new Layout(true);
            var row1 = layout.AddRow(12);
            var row2 = layout.AddRow(12);
            var row3 = layout.AddRow(12);
            row1.AddColumn(12).AddContent(this.accountRegister);
            row2.AddColumn(12).AddContent(this.financialStatement1);
            row3.AddColumn(12).AddContent(this.financialStatementList);
            return layout;
		}

        protected override void ConfigureBlock(List<Block> container, string host, FinancialStatementDisplay condition)
        {
            this.accountRegister = new AccountRegister();
            this.financialStatement1 = new FinancialStatement(condition.Company);
            this.financialStatementList = new FinancialStatementList(condition.Company);

            container.Add(this.accountRegister);
            container.Add(this.financialStatement1);
            container.Add(this.financialStatementList);

            var transition = new TransitionSample(typeof(Finder));
            this.financialStatementList.AddLinker(transition);
        }
    }
}

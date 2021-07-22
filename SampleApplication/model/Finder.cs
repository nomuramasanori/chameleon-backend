using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;

namespace BusinessClass
{
    public class Finder : Content<FinancialStatementCore>
    {
        private AccountRegister accountRegister;

        public Finder() : base("検索画面", showAsMenu: true) { }

        //protected override void Calculate(IDbTransaction transaction)
        //{
        //    this.accountRegister.Calculate();
        //}

        protected override void ConfigureBlock(List<Block> container, string host, FinancialStatementCore condition)
        {
            this.accountRegister = new AccountRegister();

            container.Add(this.accountRegister);

            var transition = new TransitionSample(typeof(Budget));
            this.accountRegister.AddLinker(transition);
        }

        public override Layout GetLayout()
        {
            var layout = new Layout();
            var row1 = layout.AddRow(5);
            row1.AddColumn(3).AddContent(this.accountRegister);

            return layout;
        }
    }
}

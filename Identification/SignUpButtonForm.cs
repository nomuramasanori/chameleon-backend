using System;
using System.Data;
using Chameleon;

namespace Identification
{
	public class SignUpButtonForm : Form<SignUpButtonItem>
	{
		public SignUpButtonForm() : base(null, noFrame: true, explain: "ユーザー登録がまだの方はこちらから")
		{
        }

		protected override SignUpButtonItem GetData(IDbTransaction transaction)
		{
            var result = new SignUpButtonItem();

            return result;
		}

        public override void Calculate(IDbTransaction transaction)
        {
        }
    }
}

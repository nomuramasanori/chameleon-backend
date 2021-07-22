using System;
using System.Data;
using Chameleon;

namespace Identification
{
	public class SignInForm : Form<SignInItem>
	{
		public SignInForm(string host) : base("Sign-in")
		{
            this.AuthenticateUrl = $"https://{host}/login";
        }

		protected override SignInItem GetData(IDbTransaction transaction)
		{
            var result = new SignInItem();

            return result;
		}

        public override void Calculate(IDbTransaction transaction)
        {
        }
    }
}

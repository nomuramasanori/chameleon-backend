using System;
using System.Data;
using Chameleon;

namespace Identification
{
	public class SignUpForm : Form<SignUpItem>
	{
		public SignUpForm(string host) : base("Sign-in")
		{
            this.AuthenticateUrl = $"https://{host}/login";
        }

		protected override SignUpItem GetData(IDbTransaction transaction)
		{
            var result = new SignUpItem();

            return result;
		}

        public override void Calculate(IDbTransaction transaction)
        {
        }
    }
}

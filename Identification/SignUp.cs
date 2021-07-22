using System;
using System.Data;
using System.Collections.Generic;
using Chameleon;

namespace Identification
{
    public class SignUp : Content<NoCondition>
    {
        public SignUp() : base("Sign in") { }

        private SignUpForm SignUpForm;

        protected override void ConfigureBlock(List<Block> container, string host, NoCondition condition)
        {
            this.SignUpForm = new SignUpForm(host);

            container.Add(this.SignUpForm);
        }
    }
}

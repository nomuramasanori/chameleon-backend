using System;
using System.Data;
using System.Collections.Generic;
using Chameleon;

namespace Identification
{
    public class SignIn : Content<SignInItem>
    {
        public SignIn() : base("Sign in") { }

        private SignInForm TrainerFinderForm;
        private SignUpButtonForm SignUpForm;

        protected override void ConfigureBlock(List<Block> container, string host, SignInItem condition)
        {
            this.TrainerFinderForm = new SignInForm(host);
            this.SignUpForm = new SignUpButtonForm();

            container.Add(this.TrainerFinderForm);
            container.Add(this.SignUpForm);

            this.SignUpForm.AddLinker(new SignUpButtonTransition());
        }

        public override Layout GetLayout()
        {
            var layout = new Layout();
            layout.AddRow().AddColumn(4).AddContent(this.TrainerFinderForm);
            layout.AddRow().AddColumn(4).AddContent(this.SignUpForm);
            return layout;
        }
    }
}

using System;
using Chameleon;

namespace Identification
{
    public class SignUpButtonTransition : Linker<NoCondition, NoCondition>
    {
        public SignUpButtonTransition()
        {
            this.Name = "Matchoアカウントを作成";
        }

        public override (Type destination, NoCondition condition) CreateCondition(NoCondition sourceRecord)
        {
            return (typeof(SignUp), new NoCondition());
        }

    }
}

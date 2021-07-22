using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Chameleon;

namespace Identification
{
	public class SignUpItem
	{
        [Text(Name = "Account")]
        public string Account { get; set; }

        [Text(Name = "Password")]
        public string PassWord { get; set; }
    }
}

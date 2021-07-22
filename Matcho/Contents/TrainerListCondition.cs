using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Chameleon;
using Dapper;

namespace Matcho.Contents
{
	public class TrainerListCondition
	{
        public string[] Area { get; set; }

        public string[] Genre { get; set; }

        public string FreeWord { get; set; }
    }
}

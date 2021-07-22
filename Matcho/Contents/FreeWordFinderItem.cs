using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Chameleon;
using Dapper;

namespace Matcho.Contents
{
	public class FreeWordFinderItem
    {
        [Text(Name = "検索ワード")]
        public string FreeWord { get; set; }
    }
}

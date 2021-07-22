using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Chameleon;
using Dapper;

namespace Matcho.Contents
{
	public class GenreFinderItem
	{
        [MultipleSelector(nameof(GetDropdownItems), Name = "ジャンル")]
        public string[] Genre { get; set; }

        [Number(Name = "検索結果")]
        public int Count { get; set; }

        public List<DropdownItem> GetDropdownItems(IDbConnection connection)
        {
            //var result = connection.Query<DropdownItem>("select name as \"Name\" from genre").ToList();
            var result = connection.Query<DropdownItem>("select name as \"Id\", name as \"Name\", 'hoge' as \"Group\" from genre").ToList();
            return result;
        }
    }
}

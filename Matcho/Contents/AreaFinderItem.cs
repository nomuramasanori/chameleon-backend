using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Chameleon;
using Dapper;

namespace Matcho.Contents
{
	public class AreaFinderItem
	{
        [MultipleSelector(nameof(GetDropdownItems), Name = "エリア", Grouping = true)]
        public string[] Area { get; set; }

        [Number(Name = "検索結果")]
        public int Count { get; set; }

        public List<DropdownItem> GetDropdownItems(IDbConnection connection)
        {
            var result = connection.Query<DropdownItem>(@"
                select
                    m.code as ""Id"",
                    m.name as ""Name"",
                    p.name as ""Group""
                from
                    municipality m
                    inner join prefecture p
                    on
                        m.prefecture = p.code
                where
                    exists (
                        select * from trainer_area ta where ta.municipality = m.code
                    )
            ").ToList();

            return result;
        }
    }
}

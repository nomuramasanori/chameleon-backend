using System;
using System.Data;
using Dapper;
using Chameleon;

namespace Matcho.Contents
{
	public class AreaFinderForm1 : Form<AreaFinderItem>
	{
        public AreaFinderForm1() : base("エリア検索")
		{
        }

		protected override AreaFinderItem GetData(IDbTransaction transaction)
		{
            var result = new AreaFinderItem();

            return result;
		}

        public override void Calculate(IDbTransaction transaction)
        {
            if(this.Entity.Area == null)
            {
                this.Entity.Count = 0;
            }
            else
            {
                string inClauseGenre = string.Join("', '", this.Entity.Area);
                
                this.Entity.Count = transaction.Connection.QuerySingle<int>($@"
                select
                    count(*)
                from
                    trainer_area
                where
                    1 = 1
                    and municipality in ('{inClauseGenre}')
            ");
            }
        }
    }
}

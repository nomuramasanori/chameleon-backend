using System;
using System.Data;
using Dapper;
using Chameleon;

namespace Matcho.Contents
{
	public class GenreFinderForm1 : Form<GenreFinderItem>
	{
        public GenreFinderForm1() : base("ジャンル検索")
		{
        }

		protected override GenreFinderItem GetData(IDbTransaction transaction)
		{
            var result = new GenreFinderItem();

            return result;
		}

        public override void Calculate(IDbTransaction transaction)
        {
            if(this.Entity.Genre == null)
            {
                this.Entity.Count = 0;
            }
            else
            {
                string inClauseGenre = string.Join("', '", this.Entity.Genre);

                var genreCondition = $@"and account in (select account from trainer_genre where genre in ('{inClauseGenre}'))";

                this.Entity.Count = transaction.Connection.QuerySingle<int>($@"
                select
                    count(*)
                from
                    trainer 
                where
                    1 = 1
                    {genreCondition}
            ");
            }
        }
    }
}

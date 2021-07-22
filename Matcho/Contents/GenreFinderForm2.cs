using System;
using System.Data;
using Dapper;
using Chameleon;

namespace Matcho.Contents
{
	public class GenreFinderForm2 : Form<GenreFinderItem>
	{
        private TrainerListCondition condition;

        public GenreFinderForm2(TrainerListCondition condition) : base("ジャンル検索")
		{
            this.condition = condition;
        }

		protected override GenreFinderItem GetData(IDbTransaction transaction)
		{
            var result = new GenreFinderItem();

            return result;
		}

        public override void Calculate(IDbTransaction transaction)
        {
            if(this.Entity.Genre == null || this.condition.Area == null)
            {
                this.Entity.Count = 0;
            }
            else
            {
                string inClauseArea = string.Join("', '", this.condition.Area);
                string inClauseGenre = string.Join("', '", this.Entity.Genre);

                var areaCondition = $@"and account in (select account from trainer_area where municipality in ('{inClauseArea}'))";
                var genreCondition = $@"and account in (select account from trainer_genre where genre in ('{inClauseGenre}'))";

                this.Entity.Count = transaction.Connection.QuerySingle<int>($@"
                    select
                        count(*)
                    from
                        trainer 
                    where
                        1 = 1
                        {areaCondition}
                        {genreCondition}
                ");
            }
        }
    }
}

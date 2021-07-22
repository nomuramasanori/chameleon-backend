using System;
using System.Data;
using Dapper;
using Chameleon;

namespace Matcho.Contents
{
	public class AreaFinderForm2 : Form<AreaFinderItem>
	{
        private TrainerListCondition condition;

        public AreaFinderForm2(TrainerListCondition condition) : base("エリア検索")
		{
            this.condition = condition;
        }

		protected override AreaFinderItem GetData(IDbTransaction transaction)
		{
            var result = new AreaFinderItem();

            return result;
		}

        public override void Calculate(IDbTransaction transaction)
        {
            if (this.Entity.Area == null || this.condition.Genre == null)
            {
                this.Entity.Count = 0;
            }
            else
            {
                string inClauseGenre = string.Join("', '", this.condition.Genre);
                string inClauseArea = string.Join("', '", this.Entity.Area);

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

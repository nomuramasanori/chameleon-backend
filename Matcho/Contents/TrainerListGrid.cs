using System;
using System.Collections.Generic;
using System.Linq;
using Chameleon;
using System.Data;
using Dapper;

namespace Matcho.Contents
{
	public class TrainerListGrid : Grid<TrainerListItem, TrainerListItem>
	{
        private TrainerListCondition condition;

        public TrainerListGrid(TrainerListCondition condition, string id = null) : base("トレーナーリスト", id, "list")
		{
            this.condition = condition;
		}

		protected override List<TrainerListItem> ConvertDisplayDataToCoreData(List<TrainerListItem> displayData)
		{
			return displayData;
		}

		protected override List<TrainerListItem> ConvertCoreDataToDisplayData(List<TrainerListItem> coreData)
		{
			return coreData;
		}

		protected override List<TrainerListItem> GetData(IDbTransaction transaction)
		{
            string inClauseArea = this.condition.Area != null ? string.Join("', '", this.condition.Area) : "";
            string inClauseGenre = this.condition.Genre != null ? string.Join("', '", this.condition.Genre) : "";

            var areaCondition = $@"and account in (select account from trainer_area where municipality in ('{inClauseArea}'))";
            var genreCondition = $@"and account in (select account from trainer_genre where genre in ('{inClauseGenre}'))";
            var freeWordCondition = $@"and (name like '%{this.condition.FreeWord}%') or (self_introduction like '%{this.condition.FreeWord}%')";

            var result = transaction.Connection.Query<TrainerListItem>($@"
                select
                    account as ""Account"",
                    name as ""Name"",
                    self_introduction as ""SelfIntroduction"",
                    service as ""Profile""
                from
                    trainer 
                where
                    1 = 1
                    {(this.condition.Area == null ? "" : areaCondition)}
                    {(this.condition.Genre == null ? "" : genreCondition)}
                    {(this.condition.FreeWord == null ? "" : freeWordCondition)}
            ");

            return result.ToList();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;

namespace Matcho.Contents
{
    public class TrainerDetail : Content<TrainerDetailCondition>
    {
        private TrainerDetailForm trainerList;

        public TrainerDetail() : base("トレーナー詳細", showAsMenu: false) { }

        protected override void Calculate(IDbTransaction transaction)
        {
        }

        public override Layout GetLayout()
        {
            var layout = new Layout(true);
            var row1 = layout.AddRow(5);
            row1.AddColumn(3).AddContent(this.trainerList);
            return layout;
        }

        protected override void ConfigureBlock(List<Block> container, string host, TrainerDetailCondition condition)
        {
            this.trainerList = new TrainerDetailForm(condition.Account);

            container.Add(this.trainerList);
        }
    }
}

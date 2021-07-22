using System;
using System.Collections.Generic;
using System.Data;
using Chameleon;

namespace Matcho.Contents
{
    public class TrainerList : Content<TrainerListCondition>
    {
        private TrainerListGrid trainerList;

        public TrainerList() : base("トレーナーリスト", showAsMenu : false) { }

        protected override void Calculate(IDbTransaction transaction)
        {
        }

        protected override void ConfigureBlock(List<Block> container, string host, TrainerListCondition condition)
        {
            this.trainerList = new TrainerListGrid(condition);

            this.trainerList.AddLinker(new TrainerListTransition());
            container.Add(this.trainerList);
        }
    }
}

using System;
using Chameleon;

namespace Matcho.Contents
{
    public class AreaFinderTransition2 : Linker<AreaFinderItem, TrainerListCondition>
    {
        private TrainerListCondition condition;

        public AreaFinderTransition2(TrainerListCondition condition)
        {
            this.condition = condition;
            this.Name = "トレーナー検索";
        }

        public override (Type destination, TrainerListCondition condition) CreateCondition(AreaFinderItem sourceRecord)
        {
            return (typeof(TrainerList), new TrainerListCondition { Area = sourceRecord.Area, Genre = this.condition.Genre });
        }
    }
}

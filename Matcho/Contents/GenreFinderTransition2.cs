using System;
using Chameleon;

namespace Matcho.Contents
{
    public class GenreFinderTransition2 : Linker<GenreFinderItem, TrainerListCondition>
    {
        private TrainerListCondition condition;

        public GenreFinderTransition2(TrainerListCondition condition)
        {
            this.Name = "トレーナー検索";
            this.condition = condition;
        }

        public override (Type destination, TrainerListCondition condition) CreateCondition(GenreFinderItem sourceRecord)
        {
            return (typeof(TrainerList), new TrainerListCondition { Area = this.condition.Area, Genre = sourceRecord.Genre });
        }

    }
}

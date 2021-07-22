using System;
using Chameleon;

namespace Matcho.Contents
{
    public class AreaFinderTransition3 : Linker<AreaFinderItem, TrainerListCondition>
    {
        public AreaFinderTransition3()
        {
            this.Name = "トレーナー検索";
        }

        public override (Type destination, TrainerListCondition condition) CreateCondition(AreaFinderItem sourceRecord)
        {
            return (typeof(TrainerList), new TrainerListCondition { Area = sourceRecord.Area ?? new string[0] });
        }

    }
}

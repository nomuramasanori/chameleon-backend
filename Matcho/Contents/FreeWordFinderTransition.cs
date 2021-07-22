using System;
using Chameleon;

namespace Matcho.Contents
{
    public class FreeWordFinderTransition : Linker<FreeWordFinderItem, TrainerListCondition>
    {
        public FreeWordFinderTransition()
        {
            this.Name = "トレーナー検索";
        }

        public override (Type destination, TrainerListCondition condition) CreateCondition(FreeWordFinderItem sourceRecord)
        {
            return (typeof(TrainerList), new TrainerListCondition { FreeWord = sourceRecord.FreeWord });
        }

    }
}

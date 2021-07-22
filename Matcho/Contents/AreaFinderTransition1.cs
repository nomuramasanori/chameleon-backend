using System;
using Chameleon;

namespace Matcho.Contents
{
    public class AreaFinderTransition1 : Linker<AreaFinderItem, TrainerListCondition>
    {
        public AreaFinderTransition1()
        {
            this.Name = "検索条件を追加";
        }

        public override (Type destination, TrainerListCondition condition) CreateCondition(AreaFinderItem sourceRecord)
        {
            return (typeof(GenreFinder2), new TrainerListCondition { Area = sourceRecord.Area });
        }
    }
}

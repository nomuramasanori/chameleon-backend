using System;
using Chameleon;

namespace Matcho.Contents
{
    public class GenreFinderTransition1 : Linker<GenreFinderItem, TrainerListCondition>
    {
        public GenreFinderTransition1()
        {
            this.Name = "検索条件を追加";
        }

        public override (Type destination, TrainerListCondition condition) CreateCondition(GenreFinderItem sourceRecord)
        {
            return (typeof(AreaFinder2), new TrainerListCondition { Genre = sourceRecord.Genre });
        }
    }
}

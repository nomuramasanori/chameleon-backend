using System;
using Chameleon;

namespace Matcho.Contents
{
    public class GenreFinderTransition3 : Linker<GenreFinderItem, TrainerListCondition>
    {
        public GenreFinderTransition3()
        {
            this.Name = "トレーナー検索";
        }

        public override (Type destination, TrainerListCondition condition) CreateCondition(GenreFinderItem sourceRecord)
        {
            return (typeof(TrainerList), new TrainerListCondition { Genre = sourceRecord.Genre ?? new string[0] });
        }

    }
}

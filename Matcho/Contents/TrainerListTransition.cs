using System;
using Chameleon;

namespace Matcho.Contents
{
    public class TrainerListTransition : Linker<TrainerListItem, TrainerDetailCondition>
    {
        public override (Type destination, TrainerDetailCondition condition) CreateCondition(TrainerListItem sourceRecord)
        {
            return (typeof(TrainerDetail), new TrainerDetailCondition { Account = sourceRecord.Account });
        }
    }
}

using System;
using Chameleon;

namespace BusinessClass
{
    public class TransitionSample : Linker<FinancialStatementDisplay, FinancialStatementDisplay>
    {
        private Type destination;

        public TransitionSample(Type destination)
        {
            this.destination = destination;
        }

        //public override FinancialStatementDisplay CreateCondition(FinancialStatementDisplay sourceRecord)
        //{
        //    return sourceRecord;
        //}

        public override (Type destination, FinancialStatementDisplay condition) CreateCondition(FinancialStatementDisplay sourceRecord)
        {
            return (this.destination, sourceRecord);
        }
    }
}

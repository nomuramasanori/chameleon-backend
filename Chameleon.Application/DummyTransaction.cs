using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Chameleon.Application
{
    public class DummyTransaction : System.Data.Common.DbTransaction
    {
        public override IsolationLevel IsolationLevel => throw new NotImplementedException();

        protected override DbConnection DbConnection => throw new NotImplementedException();

        public override void Commit()
        {
        }

        public override void Rollback()
        {
        }
    }
}

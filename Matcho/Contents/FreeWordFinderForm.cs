using System;
using System.Data;
using Dapper;
using Chameleon;

namespace Matcho.Contents
{
	public class FreeWordFinderForm : Form<FreeWordFinderItem>
	{
        public FreeWordFinderForm() : base("フリーワード検索") {}

		protected override FreeWordFinderItem GetData(IDbTransaction transaction)
		{
            var result = new FreeWordFinderItem();

            return result;
		}

        public override void Calculate(IDbTransaction transaction){}
    }
}

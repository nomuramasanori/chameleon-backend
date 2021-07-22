using System;
using Chameleon;
using System.Data;
using Dapper;

namespace Matcho.Contents
{
	public class TrainerDetailForm : Form<TrainerDetailItem>
	{
        private string account;

		public TrainerDetailForm(string account) : base("トレーナー詳細")
		{
            this.account = account;
        }

		protected override TrainerDetailItem GetData(IDbTransaction transaction)
		{
            var result = transaction.Connection.QueryFirstOrDefault<TrainerDetailItem>($@"
                select
                    account as ""Account"",
                    name as ""Name"",
                    gym as ""gym"",
                    delivery as ""Delivery"",
                    online as ""Online"",
                    disco as ""Disco"",
                    license as ""License"",
                    self_introduction as ""SelfIntroduction"",
                    service as ""Service"",
                    website as ""WebSite"",
                    line as ""Line"",
                    twitter as ""Twitter"",
                    facebook as ""Facebook"",
                    youtube as ""YouTube"",
                    etc as ""Etc"",
                    matcho_url as ""MatchoUrl"",
                    instagram as ""Instagram""
                from
                    trainer
                where
                    account = '{this.account}'
            ");

            return result;
		}
    }
}

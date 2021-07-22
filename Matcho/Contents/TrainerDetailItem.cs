using System;
using Chameleon;

namespace Matcho.Contents
{
	public class TrainerDetailItem
	{
        [Image(ReadOnly =true)]
        public string picture { get; set; }

		[Text(Name = "アカウント", ReadOnly = true)]
        public string Account { get; set; }

        [Text(Name = "名前", ReadOnly = true)]
        public string Name { get; set; }

        [Text(Name = "ジム", ReadOnly = true)]
        public string Gym { get; set; }

        [Checkbox(Name = "出張", ReadOnly = true)]
        public bool Delivery { get; set; }

        [Checkbox(Name = "オンライン", ReadOnly = true)]
        public bool Online { get; set; }

        [Text(Name = "経歴", ReadOnly = true)]
        public string Disco { get; set; }

        [Text(Name = "資格", ReadOnly = true)]
        public string License { get; set; }

        [Text(Name = "自己紹介", ReadOnly = true)]
        public string SelfIntroduction { get; set; }

        [Text(Name = "サービス", ReadOnly = true)]
        public string Service { get; set; }

        [Text(Name = "その他", ReadOnly = true)]
        public string Etc { get; set; }

        [HyperLink(Name = "Web", ReadOnly = true, IconName = "Language")]
        public string WebSite { get; set; }

        [HyperLink(Name = "LINE", ReadOnly = true, IconName = "Language")]
        public string Line { get; set; }

        [HyperLink(Name = "Twitter", ReadOnly = true, IconName = "Twitter")]
        public string Twitter { get; set; }

        [HyperLink(Name = "Facebook", ReadOnly = true, IconName = "Facebook")]
        public string Facebook { get; set; }

        [HyperLink(Name = "YouTube", ReadOnly = true, IconName = "YouTube")]
        public string YouTube { get; set; }

        [HyperLink(Name = "Matcho", ReadOnly = true, IconName = "Language")]
        public string MatchoUrl { get; set; }

        [HyperLink(Name = "Instagram", ReadOnly = true, IconName = "Instagram")]
        public string Instagram { get; set; }
    }
}

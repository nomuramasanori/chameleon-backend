using System;
using Chameleon;

namespace Matcho.Contents
{
	public class TrainerListItem
	{
		[Text(Name = "アカウント")]
        public string Account { get; set; }

        [Text(Name ="名前")]
        [ListItem(ListItemType.Title)]
        public string Name { get; set; }

		[Text(Name = "自己紹介")]
        [ListItem(ListItemType.Body1)]
        public string SelfIntroduction { get; set; }

        [Text(Name = "経歴")]
        [ListItem(ListItemType.Body2)]
        public string Profile { get; set; }
    }
}

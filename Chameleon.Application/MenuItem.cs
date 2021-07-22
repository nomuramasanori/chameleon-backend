using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chameleon.Application
{
    public class MenuItem
    {
        [Text]
        public string Id { get; set; }

        [Text]
        [ListItem(ListItemType.Title)]
        public string Name { get; set; }

        [Text]
        [ListItem(ListItemType.Body1)]
        public string Description { get; set; }

        [ListItem(ListItemType.Image)]
        [Image(Name = "画像")]
        public string Picture { get; set; }
    }
}

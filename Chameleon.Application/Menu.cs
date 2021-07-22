using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Chameleon.Application
{
    public class Menu : Grid<MenuItem, MenuItem>
    {
        private string host;

        public Menu(string host) : base("", null, "card")
        {
            this.host = host;
            this.AddLinker(new MenuLinker());
        }

        protected override List<MenuItem> ConvertCoreDataToDisplayData(List<MenuItem> coreData)
        {
            return coreData;
        }

        protected override List<MenuItem> ConvertDisplayDataToCoreData(List<MenuItem> displayData)
        {
            return displayData;
        }

        protected override List<MenuItem> GetData(IDbTransaction dbTransaction)
        {
            var result = new List<MenuItem>();

            var applicationManager = new ApplicationManager();
            applicationManager.GetContents().ForEach(content => {
                if (!content.ShowAsMenu) return;

                result.Add(new MenuItem
                {
                    Id = content.GetType().FullName,
                    Name = content.Name,
                    Description = content.Description,
                    Picture = $@"https://{this.host}/image/{content.GetType().FullName}.png"
                });
            });

            return result;
        }
    }
}

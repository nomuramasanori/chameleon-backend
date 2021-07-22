using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chameleon.Models
{
    public class Application
    {
        public List<Server> Servers { get; set; }

        public List<string> GetIDs()
        {
            List<string> ids = new List<string>();
            this.Servers.ForEach(server => ids.Add(server.ID));
            return ids;
        }

        public string GetUrl(string id)
        {
            return this.Servers.First(server => { return server.ID == id; }).Url;
        }
    }
}

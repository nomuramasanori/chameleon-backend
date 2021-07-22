using System;
using Newtonsoft.Json;

namespace Chameleon
{
    public abstract class Linker<TSourceRecord, TDestinationCondition> : ILinker where TSourceRecord : class
    {
        //public Type Destination { get; private set; }

        public string Id
        {
            get
            {
                return this.GetType().FullName;
            }
        }

        public string Name { get; set; } = "untitled";

        public Linker()
        {
            //this.Destination = destination;
        }

        public Link CreateLink(string sourceRecord)
        {
            //return new Link(this.Destination, this.CreateCondition(JsonConvert.DeserializeObject<TSourceRecord>(sourceRecord)));
            var link = this.CreateCondition(JsonConvert.DeserializeObject<TSourceRecord>(sourceRecord));
            return new Link(link.destination, link.condition);
        }

        public abstract (Type destination, TDestinationCondition condition) CreateCondition(TSourceRecord sourceRecord);
    }
}

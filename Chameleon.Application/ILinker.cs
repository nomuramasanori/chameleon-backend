using System;

namespace Chameleon
{
    public interface ILinker
    {
        string Id { get; }

        string Name { get; set; }

        Link CreateLink(string sourceRecord);
    }
}

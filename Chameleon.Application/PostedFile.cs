using System;
using System.IO;

namespace Chameleon
{
    public class PostedFile
    {
        public MemoryStream MemoryStream { get; set; }

        public string ServerUrl { get; set; }

        public string WebRootPath { get; set; }

        public string Type { get; set; }

        public PostedFile(MemoryStream memoryStream, string serverUrl, string webRootPath, string type)
        {
            this.MemoryStream = memoryStream;
            this.ServerUrl = serverUrl;
            this.WebRootPath = webRootPath;
            this.Type = type;
        }
    }
}

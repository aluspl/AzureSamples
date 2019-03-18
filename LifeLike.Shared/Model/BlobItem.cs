using System;
using System.IO;

namespace LifeLike.Shared.BlobStorage
{
    public class BlobItem
    {
        public string Container { get; set; }
        public Uri Uri { get; set; }
        public Uri StorageUri { get; set; }
        public Stream Stream { get; set; }
        public string Name { get; set; }
    }
}
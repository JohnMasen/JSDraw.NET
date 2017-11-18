using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JSDraw.NET
{
    interface IResourceLoader
    {
        Func<string,Stream> ResourceLocator { get; set; }
    }
}

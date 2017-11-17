using System;
using System.Collections.Generic;
using System.Text;

namespace JSDraw.NET
{
    public interface IManaged<TTag>
    {
        int ID { get;  }
        bool IsPersistent { get; set; }
        TTag Tag { get; set; }
    }
}

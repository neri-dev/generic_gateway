using System;
using System.Collections.Generic;

namespace infrastructure
{
    public interface IImporter
    {
        void Start();
        void Stop();
        event EventHandler<object> DataReady;
    }
}


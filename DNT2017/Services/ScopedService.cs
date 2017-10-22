using System;

namespace DNT2017.Services
{
    public interface IScopedService : IGuid
    {
    }

    public class ScopedService : IScopedService
    {
        public Guid Guid { get; set; }

        public ScopedService()
        {
            Guid = Guid.NewGuid();
        }
    }
}

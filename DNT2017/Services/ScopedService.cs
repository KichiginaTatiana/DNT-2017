using System;

namespace DNT2017.Services
{
    public interface IScopedService
    {
        Guid Guid { get; set; }
    }

    public class ScopedService : IScopedService
    {
        public Guid Guid { get; set; }

        public ScopedService()
        {
            Guid = new Guid();
        }
    }
}

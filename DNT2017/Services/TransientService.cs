using System;

namespace DNT2017.Services
{
    public interface ITransientService : IGuid
    {
    }

    public class TransientService : ITransientService
    {
        public Guid Guid { get; set; }

        public TransientService()
        {
            Guid = Guid.NewGuid();
        }
    }
}

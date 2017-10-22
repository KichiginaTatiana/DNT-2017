using System;

namespace DNT2017.Services
{
    public interface ITransientService
    {
        Guid Guid { get; set; }
    }

    public class TransientService : ITransientService
    {
        public Guid Guid { get; set; }

        public TransientService()
        {
            Guid = new Guid();
        }
    }
}

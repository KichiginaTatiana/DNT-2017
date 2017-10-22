using System;

namespace DNT2017.Services
{
    public interface ISingletonService : IGuid
    {
    }

    public class SingletonService : ISingletonService, ISingletomInstanceService
    {
        public Guid Guid { get; set; }

        public SingletonService()
        {
            Guid = Guid.NewGuid();
        }

        public SingletonService(Guid guid)
        {
            Guid = guid;
        }
    }

    public interface ISingletomInstanceService 
    {
    }
}

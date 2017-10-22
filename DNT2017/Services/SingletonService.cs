using System;

namespace DNT2017.Services
{
    public interface ISingletonService
    {
        Guid Guid { get; set; }
    }

    public class SingletonService : ISingletonService, ISingletomInstanceService
    {
        public Guid Guid { get; set; }

        public SingletonService()
        {
            Guid = new Guid();
        }

        public SingletonService(string guid)
        {
            Guid = new Guid(guid);
        }
    }

    public interface ISingletomInstanceService
    {
    }
}

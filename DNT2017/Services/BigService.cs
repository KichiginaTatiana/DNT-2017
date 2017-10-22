namespace DNT2017.Services
{
    public interface IBigService
    {
    }

    public class BigService : IBigService
    {
        public ITransientService TransientService { get; }
        public IScopedService ScopedService { get; }
        public ISingletonService SingletonService { get; }
        public ISingletomInstanceService SingletomInstanceService { get; }

        public BigService(ITransientService transientService, IScopedService scopedService,
            ISingletonService singletonService, ISingletomInstanceService singletomInstanceService)
        {
            TransientService = transientService;
            ScopedService = scopedService;
            SingletonService = singletonService;
            SingletomInstanceService = singletomInstanceService;
        }
    }
}
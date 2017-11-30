using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => await new VacancyViewer().View()).Wait();
        }
    }
}

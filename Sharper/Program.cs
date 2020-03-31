using Sharper.Calculation;
using Sharper.Features;
using Sharper.Other;
using Sharper.Structs;

namespace Sharper
{
    class Program
    {
        static void Main(string[] args)
        {
            PreparationProcess.Run();
            Memory.Run(PreparationProcess.Handle.Id);

            var threadManager = new ThreadManager();
            threadManager.Add(OffsetReader.Run);
            threadManager.Add(ConfigReader.Run);
            threadManager.Add(Aimbot.Run);
            threadManager.Add(Triggerbot.Run);
            threadManager.Add(WallBeeper.Run);
            //threadManager.Add(DefuseScanner.Run);
            //threadManager.Add(BombTimeScanner.Run);

            threadManager.StartAll();
        }
    }
}

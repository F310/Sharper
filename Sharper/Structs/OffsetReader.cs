using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sharper.Calculation;
using Sharper.Other;
using Sharper.Structs;
using static Sharper.Structs.OffsetStruct;

namespace Sharper.Structs
{
    public class OffsetReader
    {
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(1);

                GlobalVars.BaseStruct = Memory.ReadMemory<GlobalVars_t>((int)Base.Engine + Offsets.dwGlobalVars);
                GlobalVars.Extensions.ServerTime = LocalPlayer.BaseStruct.TickBase * GlobalVars.BaseStruct.Interval_Per_Tick;

                LocalPlayer.Base = Memory.ReadMemory<int>((int)Base.Client + Offsets.dwLocalPlayer);
                LocalPlayer.BaseStruct = Memory.ReadMemory<LocalPlayer_t>(LocalPlayer.Base);

                EnemyCrosshair.Base = (int)Base.Client + Offsets.dwEntityList + (LocalPlayer.BaseStruct.CrosshairID - 1) * 0x10;
                EnemyCrosshair.BaseStruct = Memory.ReadMemory<Enemy_Crosshair_t>(Memory.ReadMemory<int>(EnemyCrosshair.Base));

                ClientState.Base = Memory.ReadMemory<int>((int)Base.Engine + Offsets.dwClientState);
                ClientState.BaseStruct = Memory.ReadMemory<ClientState_t>(ClientState.Base);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharper.Calculation;
using Sharper.Enums;
using Sharper.Other;
using Sharper.Structs;

namespace Sharper.Features
{
    //public class DefuseScanner
    //{
    //    [STAThread]
    //    public static void Run()
    //    {
    //        var lastD = string.Empty;
    //        var defuseType = string.Empty;
    //        float lastT = 0f;
    //        float timeLeft = 0f;
    //        int lastEnt = 0;

    //        while (true)
    //        {
    //            Thread.Sleep(1);

    //            var isPlanted = Memory.ReadMemory<bool>(Memory.ReadMemory<int>((int)OffsetStruct.Base.Client + Offsets.dwGameRulesProxy) + Offsets.m_bBombPlanted);
    //            if (!isPlanted)
    //                continue;

    //            for (int i = 0; i < 2048; i++)
    //            {
    //                var entity = Memory.ReadMemory<int>((int)OffsetStruct.Base.Client + Offsets.dwEntityList + (i - 1) * 0x10);
    //                if (entity == 0) { continue; }

    //                var entityClassId = Calcs.GetClassID(entity);
    //                var entityStruct = Memory.ReadMemory<OffsetStruct.Enemy_t>(entity);

    //                if (entityClassId == 128)
    //                {
    //                    Console.SetCursorPosition(0, 0);
    //                    var bombBlowTime = Memory.ReadMemory<float>(entity + Offsets.m_flC4Blow);
    //                    lastT = timeLeft;
    //                    timeLeft = (float)Math.Round(bombBlowTime - OffsetStruct.GlobalVars.BaseStruct.Curtime, 0);
    //                    if (timeLeft > 0) { Console.Write($"Planted: {timeLeft}"); }
    //                    if (timeLeft != lastT)
    //                    {
    //                        Console.SetCursorPosition(0, 0);
    //                        Console.Write(new string(' ', Console.WindowWidth));
    //                    }
    //                }
    //                else if (!entityStruct.Dormant && entityStruct.Team == (int)TeamID.CounterTerrorist)
    //                {
    //                    Console.SetCursorPosition(0, 1);
    //                    lastD = defuseType;
    //                    defuseType = entityStruct.Defusing ? entityStruct.HasDefuser ? "Defusing with Kit" : "Defusing" : string.Empty;
    //                    Console.Write(defuseType);

    //                    if (defuseType != lastD || lastEnt != entity)
    //                    {
    //                        Console.SetCursorPosition(0, 1);
    //                        Console.Write(new string(' ', Console.WindowWidth));
    //                    }
    //                }

    //                lastEnt = entity;
    //            }
    //        }
    //    }
    //}
}

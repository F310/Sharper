using System;
using System.Collections.Generic;
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
    //public class BombTimeScanner
    //{
    //    [STAThread]
    //    public static void Run()
    //    {
    //        while (true)
    //        {
    //            Thread.Sleep(15);

    //            var isPlanted = Memory.ReadMemory<bool>(Memory.ReadMemory<int>((int)OffsetStruct.Base.Client + Offsets.dwGameRulesProxy) + Offsets.m_bBombPlanted);
    //            if (!isPlanted)
    //                continue;

    //            var maxPlayers = OffsetStruct.ClientState.BaseStruct.MaxPlayers;
    //            var entities = Memory.ReadMemory((int)OffsetStruct.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);

    //            for (int i = 0; i < 2048; i++)
    //            {
    //                var entity = Memory.ReadMemory<int>((int)OffsetStruct.Base.Client + Offsets.dwEntityList + (i - 1) * 0x10);
    //                var entityStruct = Memory.ReadMemory<OffsetStruct.Enemy>(entity);
    //                var entityClassId = Calcs.GetClassID(entity);
    //                if (entityClassId == 128 )
    //                {
    //                    var timeLeft = GetBombTimeleft(entity);
    //                    if (timeLeft < 0f)
    //                        continue;

    //                    Console.WriteLine(timeLeft);
    //                }
    //            }
    //        }
    //    }

    //    private static float GetBombTimeleft(int bombEntity)
    //    {
    //        var bombBlowTime = Memory.ReadMemory<float>(bombEntity + Offsets.m_flC4Blow);
    //        return (float)Math.Round(bombBlowTime - OffsetStruct.GlobalVars.BaseStruct.Curtime, 0);
    //    }

    //    private static void StartBombTimer(int bombEntity)
    //    {
    //        var isExploded = false;
    //        var bombBlowTime = Memory.ReadMemory<float>(bombEntity + Offsets.m_flC4Blow);
    //        var timeLeft = (float)Math.Round(bombBlowTime - OffsetStruct.GlobalVars.BaseStruct.Curtime, 0);
    //        var currentCount = timeLeft;
    //        if (timeLeft < 0f)
    //        {
    //            Console.Clear();
    //            return;
    //        }

    //        var cursorTop = 0;
    //        Console.Write("Planted: ");
    //        var cursorLeft = Console.CursorLeft;

    //        while (!isExploded)
    //        {
    //            currentCount = timeLeft;
    //            timeLeft = (float)Math.Round(bombBlowTime - OffsetStruct.GlobalVars.BaseStruct.Curtime, 0);

    //            if (currentCount == timeLeft)
    //                continue;

    //            if (timeLeft > -1f)
    //            {
    //                Console.SetCursorPosition(cursorLeft, cursorTop);
    //                Console.Write(timeLeft);
    //            }
    //            else
    //            {
    //                isExploded = true;
    //            }
    //        }
    //    }
    //}
}

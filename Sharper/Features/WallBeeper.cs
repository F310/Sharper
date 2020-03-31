using Sharper.Calculation;
using Sharper.Other;
using Sharper.Structs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace Sharper.Features
{
    public class WallBeeper
    {
        [STAThread]
        public static void Run()
        {
            while (true)
            { 
                Thread.Sleep(15);

                if (OffsetStruct.LocalPlayer.BaseStruct.Health < 1 ||
                    !Verification.IsWindowFocused(PreparationProcess.Handle) ||
                    !ConfigStruct.Wallbeeper.Enabled)
                    continue;

                var maxPlayers = OffsetStruct.ClientState.BaseStruct.MaxPlayers;
                var entities = Memory.ReadMemory((int)OffsetStruct.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);

                for (int i = 0; i < maxPlayers; i++)
                {
                    var currentEntity = Calcs.GetInt(entities, i * 0x10);
                    var currentEntityStruct = Memory.ReadMemory<OffsetStruct.Enemy_t>(currentEntity);

                    if (currentEntityStruct.Team == OffsetStruct.LocalPlayer.BaseStruct.Team ||
                        currentEntityStruct.Health < 1 ||
                        currentEntityStruct.Dormant ||
                        currentEntityStruct.Spotted)
                        continue;

                    var bonePosition = Calcs.GetBonePos(currentEntity, 5);

                    if (bonePosition == Vector3.Zero)
                        continue;

                    var destination = Calcs.CalcAngle(OffsetStruct.LocalPlayer.BaseStruct.Position, bonePosition, OffsetStruct.LocalPlayer.BaseStruct.AimPunch, OffsetStruct.LocalPlayer.BaseStruct.VecView, 0f, 0f);

                    if (destination == Vector3.Zero)
                        continue;

                    var distance = Calcs.GetDistance3D(destination, OffsetStruct.ClientState.BaseStruct.ViewAngles);

                    if (distance > 1.2f)
                        continue;

                    Console.Beep(1000, 300);
                    Thread.Sleep(3000);
                }
            }
        }
    }
}

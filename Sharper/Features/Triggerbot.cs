using Sharper.Calculation;
using Sharper.Other;
using Sharper.Structs;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Sharper.Features
{
    public class Triggerbot
    {
        [STAThread]
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(1);

                if (!Convert.ToBoolean(Import.GetAsyncKeyState(Keys.XButton1)) ||
                    !Verification.IsWindowFocused(PreparationProcess.Handle) ||
                    OffsetStruct.EnemyCrosshair.BaseStruct.Health < 1 ||
                    OffsetStruct.EnemyCrosshair.BaseStruct.Team == OffsetStruct.LocalPlayer.BaseStruct.Team ||
                    !ConfigStruct.Triggerbot.Enabled)
                    continue;
                
                Thread.Sleep(ConfigStruct.Triggerbot.Delay);

                Memory.WriteMemory<int>((int)OffsetStruct.Base.Client + Offsets.dwForceAttack, 6);
            }
        }
    }
}

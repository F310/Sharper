using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharper.Config;
using Sharper.Other;

namespace Sharper.Structs
{
    public class ConfigReader
    {
        public static void Run()
        {
            while (true)
            {
                Thread.Sleep(50);
                if (Convert.ToBoolean(Import.GetAsyncKeyState(Keys.F12)))
                {
                    ConfigParser.Parse(PreparationProcess.Config);
                    ConfigParser.Parse(PreparationProcess.WeaponConfig);
                    Console.Beep(1000, 100);
                    continue;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Threading;
using Sharper.Config;
using Sharper.Structs;

namespace Sharper.Other
{
    public class PreparationProcess
    {
        private static readonly string processName = "csgo";
        private static readonly string[] modules = { "client_panorama.dll", "engine.dll" };
        public const string Config = @".\config.ini";
        public const string WeaponConfig = @"weaponconfig.ini";
        public static Process Handle { get; private set; }

        public static void Run()
        {
            Console.Title = string.Empty;
            LoadConfig();
            LoadProcess();
            LoadModules();
        }

        private static void LoadConfig()
        {
            ConfigParser.Parse(Config);
            ConfigParser.Parse(WeaponConfig);
        }

        private static void LoadProcess()
        {
            Process process = null;

            do
            {
                process = Process.GetProcessesByName(processName)[0];
                Thread.Sleep(50);
            }
            while (process == null);

            Handle = process;
        }

        private static void LoadModules()
        {
            foreach (ProcessModule module in Handle.Modules)
            {
                if (module.ModuleName == modules[0] && OffsetStruct.Base.Client == IntPtr.Zero)
                {
                    OffsetStruct.Base.Client = module.BaseAddress;
                }
                else if (module.ModuleName == modules[1] && OffsetStruct.Base.Engine == IntPtr.Zero)
                {
                    OffsetStruct.Base.Engine = module.BaseAddress;
                }
            }
        }
    }
}

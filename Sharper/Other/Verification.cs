using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharper.Other
{
    public static class Verification
    {
        public static bool IsWindowFocused(Process process)
        {
            Import.GetWindowThreadProcessId(Import.GetForegroundWindow(), out int activeProcessId);
            return process.Id == activeProcessId;
        }
    }
}

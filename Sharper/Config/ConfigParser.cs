using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Sharper.Other;
using Sharper.Structs;

namespace Sharper.Config
{
    public class ConfigParser
    {
        public static void Parse(string configPath)
        {
            using (var streamReader = new StreamReader(configPath))
            {
                string line = string.Empty;

                while (!streamReader.EndOfStream)
                {
                    string nameOf = string.Empty;
                    string key = string.Empty;
                    float? fov = null;
                    float? smooth = null;
                    float? disableAimlockAt = null;
                    int? delay = null;
                    int? beepDelayAfterEnemie = null;
                    int? hitbox = null;
                    bool? enabled = null;

                    if (line.Contains('[') && line.Contains(']'))
                    {
                        nameOf = line.Replace("[", string.Empty).Replace("]", string.Empty);

                        do
                        {
                            line = streamReader.ReadLine();
                            var lineSplit = line.Split('=');

                            if (lineSplit[0] == "FOV")
                            {
                                fov = float.Parse(lineSplit[1], CultureInfo.InvariantCulture);
                            }
                            else if (lineSplit[0] == "Smooth")
                            {
                                smooth = float.Parse(lineSplit[1], CultureInfo.InvariantCulture);
                            }
                            else if (lineSplit[0] == "DisableAimLockAt")
                            {
                                disableAimlockAt = float.Parse(lineSplit[1], CultureInfo.InvariantCulture);
                            }
                            else if (lineSplit[0] == "Delay")
                            {
                                delay = int.Parse(lineSplit[1]);
                            }
                            else if (lineSplit[0] == "BeepDelayAfterEnemie")
                            {
                                beepDelayAfterEnemie = int.Parse(lineSplit[1]);
                            }
                            else if (lineSplit[0] == "Enabled")
                            {
                                enabled = lineSplit[1] == "1";
                            }
                            else if (lineSplit[0] == "Key")
                            {
                                key = lineSplit[1];
                            }
                            else if (lineSplit[0] == "Hitbox")
                            {
                                hitbox = int.Parse(lineSplit[1]);
                            }

                        } while (!line.Contains("[") && !streamReader.EndOfStream);

                        SetStruct(nameOf, enabled, fov, smooth, disableAimlockAt, delay, beepDelayAfterEnemie, hitbox, key);
                    }
                    else
                    {
                        line = streamReader.ReadLine();
                    }
                }
            }
        }

        public static void SetStruct(string nameOf, bool? enabled, float? fov, float? smooth, float? disableAimlockAt, int? delay, int? beepDelayAfterEnemie, int? hitbox, string key)
        {
            switch (nameOf)
            {
                case "Aimbot":
                    {
                        ConfigStruct.Aimbot.Enabled = (bool)enabled;
                        break;
                    }

                case "Triggerbot":
                    {
                        ConfigStruct.Triggerbot.Enabled = (bool)enabled;
                        ConfigStruct.Triggerbot.Key = key;
                        ConfigStruct.Triggerbot.Delay = (int)delay;
                        break;
                    }

                case "Wallbeeper":
                    {
                        ConfigStruct.Wallbeeper.Enabled = (bool)enabled;
                        ConfigStruct.Wallbeeper.BeepDelayAfterEnemie = (int)beepDelayAfterEnemie;
                        break;
                    }

                case "Bombscanner":
                    {
                        ConfigStruct.Bombscanner.Enabled = (bool)enabled;
                        break;
                    }

                case "DEAGLE":
                    {
                        ConfigStruct.WeaponConfig.DEAGLE.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.DEAGLE.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.DEAGLE.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.DEAGLE.Hitbox = (int)hitbox;
                        break;
                    }

                case "ELITE":
                    {
                        ConfigStruct.WeaponConfig.ELITE.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.ELITE.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.ELITE.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.ELITE.Hitbox = (int)hitbox;
                        break;
                    }

                case "FIVESEVEN":
                    {
                        ConfigStruct.WeaponConfig.FIVESEVEN.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.FIVESEVEN.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.FIVESEVEN.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.FIVESEVEN.Hitbox = (int)hitbox;
                        break;
                    }

                case "GLOCK":
                    {
                        ConfigStruct.WeaponConfig.GLOCK.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.GLOCK.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.GLOCK.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.GLOCK.Hitbox = (int)hitbox;
                        break;
                    }

                case "AK47":
                    {
                        ConfigStruct.WeaponConfig.AK47.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.AK47.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.AK47.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.AK47.Hitbox = (int)hitbox;
                        break;
                    }

                case "AUG":
                    {
                        ConfigStruct.WeaponConfig.AUG.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.AUG.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.AUG.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.AUG.Hitbox = (int)hitbox;
                        break;
                    }

                case "AWP":
                    {
                        ConfigStruct.WeaponConfig.AWP.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.AWP.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.AWP.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.AWP.Hitbox = (int)hitbox;
                        break;
                    }

                case "FAMAS":
                    {
                        ConfigStruct.WeaponConfig.FAMAS.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.FAMAS.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.FAMAS.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.FAMAS.Hitbox = (int)hitbox;
                        break;
                    }

                case "G3SG1":
                    {
                        ConfigStruct.WeaponConfig.G3SG1.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.G3SG1.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.G3SG1.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.G3SG1.Hitbox = (int)hitbox;
                        break;
                    }

                case "GALILAR":
                    {
                        ConfigStruct.WeaponConfig.GALILAR.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.GALILAR.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.GALILAR.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.GALILAR.Hitbox = (int)hitbox;
                        break;
                    }

                case "M249":
                    {
                        ConfigStruct.WeaponConfig.M249.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.M249.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.M249.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.M249.Hitbox = (int)hitbox;
                        break;
                    }

                case "M4A1":
                    {
                        ConfigStruct.WeaponConfig.M4A1.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.M4A1.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.M4A1.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.M4A1.Hitbox = (int)hitbox;
                        break;
                    }

                case "MAC10":
                    {
                        ConfigStruct.WeaponConfig.MAC10.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.MAC10.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.MAC10.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.MAC10.Hitbox = (int)hitbox;
                        break;
                    }

                case "P90":
                    {
                        ConfigStruct.WeaponConfig.P90.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.P90.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.P90.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.P90.Hitbox = (int)hitbox;
                        break;
                    }

                case "UMP45":
                    {
                        ConfigStruct.WeaponConfig.UMP45.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.UMP45.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.UMP45.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.UMP45.Hitbox = (int)hitbox;
                        break;
                    }

                case "XM1014":
                    {
                        ConfigStruct.WeaponConfig.XM1014.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.XM1014.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.XM1014.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.XM1014.Hitbox = (int)hitbox;
                        break;
                    }

                case "BIZON":
                    {
                        ConfigStruct.WeaponConfig.BIZON.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.BIZON.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.BIZON.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.BIZON.Hitbox = (int)hitbox;
                        break;
                    }

                case "MAG7":
                    {
                        ConfigStruct.WeaponConfig.MAG7.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.MAG7.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.MAG7.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.MAG7.Hitbox = (int)hitbox;
                        break;
                    }

                case "NEGEV":
                    {
                        ConfigStruct.WeaponConfig.NEGEV.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.NEGEV.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.NEGEV.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.NEGEV.Hitbox = (int)hitbox;
                        break;
                    }

                case "SAWEDOFF":
                    {
                        ConfigStruct.WeaponConfig.SAWEDOFF.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.SAWEDOFF.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.SAWEDOFF.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.SAWEDOFF.Hitbox = (int)hitbox;
                        break;
                    }

                case "TEC9":
                    {
                        ConfigStruct.WeaponConfig.TEC9.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.TEC9.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.TEC9.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.TEC9.Hitbox = (int)hitbox;
                        break;
                    }

                case "HKP2000":
                    {
                        ConfigStruct.WeaponConfig.HKP2000.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.HKP2000.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.HKP2000.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.HKP2000.Hitbox = (int)hitbox;
                        break;
                    }

                case "MP7":
                    {
                        ConfigStruct.WeaponConfig.MP7.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.MP7.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.MP7.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.MP7.Hitbox = (int)hitbox;
                        break;
                    }

                case "MP9":
                    {
                        ConfigStruct.WeaponConfig.MP9.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.MP9.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.MP9.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.MP9.Hitbox = (int)hitbox;
                        break;
                    }

                case "NOVA":
                    {
                        ConfigStruct.WeaponConfig.NOVA.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.NOVA.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.NOVA.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.NOVA.Hitbox = (int)hitbox;
                        break;
                    }

                case "P250":
                    {
                        ConfigStruct.WeaponConfig.P250.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.P250.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.P250.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.P250.Hitbox = (int)hitbox;
                        break;
                    }

                case "SCAR20":
                    {
                        ConfigStruct.WeaponConfig.SCAR20.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.SCAR20.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.SCAR20.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.SCAR20.Hitbox = (int)hitbox;
                        break;
                    }

                case "SG556":
                    {
                        ConfigStruct.WeaponConfig.SG556.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.SG556.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.SG556.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.SG556.Hitbox = (int)hitbox;
                        break;
                    }

                case "SSG08":
                    {
                        ConfigStruct.WeaponConfig.SSG08.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.SSG08.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.SSG08.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.SSG08.Hitbox = (int)hitbox;
                        break;
                    }

                case "M4A1_SILENCER":
                    {
                        ConfigStruct.WeaponConfig.M4A1_SILENCER.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.M4A1_SILENCER.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.M4A1_SILENCER.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.M4A1_SILENCER.Hitbox = (int)hitbox;
                        break;
                    }

                case "USP_SILENCER":
                    {
                        ConfigStruct.WeaponConfig.USP_SILENCER.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.USP_SILENCER.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.USP_SILENCER.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.USP_SILENCER.Hitbox = (int)hitbox;
                        break;
                    }

                case "CZ75A":
                    {
                        ConfigStruct.WeaponConfig.CZ75A.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.CZ75A.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.CZ75A.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.CZ75A.Hitbox = (int)hitbox;
                        break;
                    }

                case "REVOLVER":
                    {
                        ConfigStruct.WeaponConfig.REVOLVER.FOV = (float)fov;
                        ConfigStruct.WeaponConfig.REVOLVER.DisableSpraylockAt = (float)disableAimlockAt;
                        ConfigStruct.WeaponConfig.REVOLVER.Smooth = (float)smooth;
                        ConfigStruct.WeaponConfig.REVOLVER.Hitbox = (int)hitbox;
                        break;
                    }
            }
        }
    }
}

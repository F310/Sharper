using Sharper.Calculation;
using Sharper.Enums;
using Sharper.Other;
using Sharper.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;

// ToDo: Stop Aimer if jumping

namespace Sharper.Features
{
    public class Aimbot
    {
        [STAThread]
        public static void Run()
        {
            Random random = new Random();
            
            while (true)
            {
                Thread.Sleep(1);

                if (!Convert.ToBoolean((long)Import.GetAsyncKeyState(Keys.LButton)) ||
                    Convert.ToBoolean((long)Import.GetAsyncKeyState(Keys.XButton1)) ||
                    !Verification.IsWindowFocused(PreparationProcess.Handle) ||
                    OffsetStruct.LocalPlayer.BaseStruct.Health < 1 ||
                    !ConfigStruct.Aimbot.Enabled)
                    continue;

                LoadCurrentWeaponStruct();

                if (ConfigStruct.CurrentWeapon.FOV == 0)
                    continue;

                var maxPlayers = OffsetStruct.ClientState.BaseStruct.MaxPlayers;
                var entities = Memory.ReadMemory((int)OffsetStruct.Base.Client + Offsets.dwEntityList, maxPlayers * 0x10);
                var possibleTargets = new Dictionary<float, Vector3>();

                for (int i = 0; i < maxPlayers; i++)
                {
                    var currentEntity = Calcs.GetInt(entities, i * 0x10);
                    var currentEntityStruct = Memory.ReadMemory<OffsetStruct.Enemy_t>(currentEntity);

                    if (currentEntityStruct.Team == OffsetStruct.LocalPlayer.BaseStruct.Team ||
                        currentEntityStruct.Health < 1 ||
                        currentEntityStruct.Dormant ||
                        !currentEntityStruct.Spotted)
                        continue;

                    var bonePosition = Calcs.GetBonePos(currentEntity, ConfigStruct.CurrentWeapon.Hitbox);

                    if (bonePosition == Vector3.Zero)
                        continue;

                    var destination = Calcs.CalcAngle(OffsetStruct.LocalPlayer.BaseStruct.Position, bonePosition, OffsetStruct.LocalPlayer.BaseStruct.AimPunch, OffsetStruct.LocalPlayer.BaseStruct.VecView, 0f, 0f);

                    if (destination == Vector3.Zero)
                        continue;

                    var distance = Calcs.GetDistance3D(destination, OffsetStruct.ClientState.BaseStruct.ViewAngles);

                    if (distance > ConfigStruct.CurrentWeapon.FOV)
                        continue;

                    possibleTargets.Add(distance, destination);
                }

                if (!possibleTargets.Any())
                    continue;

                if (ConfigStruct.CurrentWeapon.DisableSpraylockAt != 0 && OffsetStruct.LocalPlayer.BaseStruct.AimPunch.X < (ConfigStruct.CurrentWeapon.DisableSpraylockAt * (-1)))
                    continue;

                var aimAngle = possibleTargets.OrderByDescending(x => x.Key).LastOrDefault().Value;

                var qDelta = aimAngle - OffsetStruct.ClientState.BaseStruct.ViewAngles;
                qDelta += new Vector3(qDelta.Y / 2.5f, qDelta.X / 2.5f, qDelta.Z);

                aimAngle = OffsetStruct.ClientState.BaseStruct.ViewAngles + qDelta;
                aimAngle = Calcs.NormalizeAngle(aimAngle);
                aimAngle = Calcs.ClampAngle(aimAngle);

                if (ConfigStruct.CurrentWeapon.Smooth > 0f)
                {
                    var randomizedSmooth = (float)Randomize(random, (ConfigStruct.CurrentWeapon.Smooth - 0.5f), (ConfigStruct.CurrentWeapon.Smooth + 0.5f));
                    aimAngle = Calcs.SmoothAim(OffsetStruct.ClientState.BaseStruct.ViewAngles, aimAngle, randomizedSmooth);
                }

                Memory.WriteMemory<Vector3>(OffsetStruct.ClientState.Base + Offsets.dwClientState_ViewAngles, aimAngle);
            }
        }

        private static void LoadCurrentWeaponStruct()
        {
            var weaponEntity = Memory.ReadMemory<int>(OffsetStruct.LocalPlayer.Base + Offsets.m_hActiveWeapon) & 0xFFF;
            var currentWeaponEntity = Memory.ReadMemory<int>((int)OffsetStruct.Base.Client + Offsets.dwEntityList + (weaponEntity - 1) * 0x10);
            var weaponId = Memory.ReadMemory<int>(currentWeaponEntity + Offsets.m_iItemDefinitionIndex);

            switch (weaponId)
            {
                case (int)WeaponID.WEAPON_DEAGLE:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.DEAGLE.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.DEAGLE.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.DEAGLE.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.DEAGLE.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_AK47:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.AK47.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.AK47.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.AK47.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.AK47.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_AUG:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.AUG.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.AUG.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.AUG.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.AUG.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_AWP:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.AWP.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.AWP.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.AWP.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.AWP.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_BIZON:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.BIZON.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.BIZON.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.BIZON.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.BIZON.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_CZ75A:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.CZ75A.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.CZ75A.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.CZ75A.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.CZ75A.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_ELITE:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.ELITE.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.ELITE.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.ELITE.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.ELITE.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_FAMAS:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.FAMAS.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.FAMAS.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.FAMAS.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.FAMAS.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_FIVESEVEN:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.FIVESEVEN.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.FIVESEVEN.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.FIVESEVEN.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.FIVESEVEN.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_G3SG1:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.G3SG1.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.G3SG1.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.G3SG1.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.G3SG1.Hitbox;
                        break;
                    }
                case (int)WeaponID.WEAPON_GALILAR:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.GALILAR.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.GALILAR.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.GALILAR.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.GALILAR.Hitbox;
                        break;
                    }
                case (int)WeaponID.WEAPON_GLOCK:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.GLOCK.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.GLOCK.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.GLOCK.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.GLOCK.Hitbox;
                        break;
                    }
                case (int)WeaponID.WEAPON_HKP2000:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.HKP2000.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.HKP2000.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.HKP2000.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.HKP2000.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_M249:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.M249.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.M249.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.M249.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.M249.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_M4A1:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.M4A1.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.M4A1.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.M4A1.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.M4A1.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_M4A1_SILENCER:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.M4A1_SILENCER.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.M4A1_SILENCER.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.M4A1_SILENCER.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.M4A1_SILENCER.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_MP7:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.MP7.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.MP7.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.MP7.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.MP7.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_MAC10:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.MAC10.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.MAC10.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.MAC10.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.MAC10.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_MAG7:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.MAG7.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.MAG7.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.MAG7.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.MAG7.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_MP9:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.MP9.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.MP9.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.MP9.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.MP9.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_NEGEV:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.NEGEV.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.NEGEV.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.NEGEV.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.NEGEV.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_NOVA:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.NOVA.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.NOVA.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.NOVA.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.NOVA.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_P250:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.P250.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.P250.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.P250.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.P250.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_P90:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.P90.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.P90.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.P90.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.P90.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_REVOLVER:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.REVOLVER.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.REVOLVER.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.REVOLVER.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.REVOLVER.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_SAWEDOFF:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.SAWEDOFF.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.SAWEDOFF.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.SAWEDOFF.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.SAWEDOFF.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_SG556:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.SG556.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.SG556.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.SG556.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.SG556.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_SCAR20:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.SCAR20.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.SCAR20.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.SCAR20.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.SCAR20.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_SSG08:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.SSG08.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.SSG08.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.SSG08.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.SSG08.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_TEC9:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.TEC9.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.TEC9.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.TEC9.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.TEC9.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_UMP45:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.UMP45.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.UMP45.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.UMP45.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.UMP45.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_USP_SILENCER:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.USP_SILENCER.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.USP_SILENCER.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.USP_SILENCER.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.USP_SILENCER.Hitbox;
                        break;
                    }

                case (int)WeaponID.WEAPON_XM1014:
                    {
                        ConfigStruct.CurrentWeapon.FOV = ConfigStruct.WeaponConfig.XM1014.FOV;
                        ConfigStruct.CurrentWeapon.DisableSpraylockAt = ConfigStruct.WeaponConfig.XM1014.DisableSpraylockAt;
                        ConfigStruct.CurrentWeapon.Smooth = ConfigStruct.WeaponConfig.XM1014.Smooth;
                        ConfigStruct.CurrentWeapon.Hitbox = ConfigStruct.WeaponConfig.XM1014.Hitbox;
                        break;
                    }
            }
        }

        private static double Randomize(Random generator, float minValue, float maxValue)
        {
            var next = generator.NextDouble();

            return minValue + (next * (maxValue - minValue));
        }
    }
}

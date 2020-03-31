using Sharper.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Sharper.Structs.OffsetStruct;

namespace Sharper.Structs
{
    public class OffsetStruct
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct LocalPlayer_t
        {
            [FieldOffset(Offsets.m_lifeState)]
            public int LifeState;

            [FieldOffset(Offsets.m_iHealth)]
            public int Health;

            [FieldOffset(Offsets.m_fFlags)]
            public int Flags;

            [FieldOffset(Offsets.m_iTeamNum)]
            public int Team;

            [FieldOffset(Offsets.m_iShotsFired)]
            public int ShotsFired;

            [FieldOffset(Offsets.m_iCrosshairId)]
            public int CrosshairID;

            [FieldOffset(Offsets.m_flFlashDuration)]
            public int FlashDuration;

            [FieldOffset(Offsets.m_flFlashMaxAlpha)]
            public int FlashMaxAlpha;

            [FieldOffset(Offsets.m_bDormant)]
            public bool Dormant;

            [FieldOffset(Offsets.m_MoveType)]
            public int MoveType;

            [FieldOffset(Offsets.m_nTickBase)]
            public int TickBase;

            [FieldOffset(Offsets.m_vecOrigin)]
            public Vector3 Position;

            [FieldOffset(Offsets.m_aimPunchAngle)]
            public Vector3 AimPunch;

            [FieldOffset(Offsets.m_vecViewOffset)]
            public Vector3 VecView;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Enemy_Crosshair_t
        {
            [FieldOffset(Offsets.m_iHealth)]
            public int Health;

            [FieldOffset(Offsets.m_iTeamNum)]
            public int Team;

            [FieldOffset(Offsets.m_bDormant)]
            public bool Dormant;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Enemy_t
        {
            [FieldOffset(Offsets.m_iHealth)]
            public int Health;

            [FieldOffset(Offsets.m_iTeamNum)]
            public int Team;

            [FieldOffset(Offsets.m_bDormant)]
            public bool Dormant;

            [FieldOffset(Offsets.m_bSpotted)]
            public bool Spotted;

            [FieldOffset(Offsets.m_bIsDefusing)]
            public bool Defusing;

            [FieldOffset(Offsets.m_bHasDefuser)]
            public bool HasDefuser;

            [FieldOffset(Offsets.m_vecOrigin)]
            public Vector3 Position;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct ClientState_t
        {
            [FieldOffset(Offsets.dwClientState_State)]
            public int GameState;

            [FieldOffset(Offsets.dwClientState_MaxPlayer)]
            public int MaxPlayers;

            [FieldOffset(Offsets.dwClientState_ViewAngles)]
            public Vector3 ViewAngles;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct GlobalVars_t
        {
            [FieldOffset(0x0000)]
            public float RealTime; // 0x00

            [FieldOffset(0x0004)]
            public int FrameCount;

            [FieldOffset(0x0008)]
            public float AbsoluteFrametime;

            [FieldOffset(0x000C)]
            public float AbsoluteFrameStartTimestddev;

            [FieldOffset(0x0010)]
            public float Curtime;

            [FieldOffset(0x0014)]
            public float Frametime;

            [FieldOffset(0x0018)]
            public int MaxClients;

            [FieldOffset(0x001c)]
            public int TickCount;

            [FieldOffset(0x0020)]
            public float Interval_Per_Tick;

            [FieldOffset(0x0024)]
            public float Interpolation_Amount;

            [FieldOffset(0x0028)]
            public int SimTicksThisFrame;

            [FieldOffset(0x002c)]
            public int Network_Protocol;

            [FieldOffset(0x0030)]
            public IntPtr pSaveData;

            [FieldOffset(0x0031)]
            public bool m_bClient;

            [FieldOffset(0x0032)]
            public bool m_bRemoteClient;

            [FieldOffset(0x0036)]
            public int nTimestampNetworkingBase;

            [FieldOffset(0x003A)]
            public int nTimestampRandomizeWindow;
        }

        public struct Base
        {
            public static IntPtr Client { get; set; }
            public static IntPtr Engine { get; set; }
        }

        public struct LocalPlayer
        {
            public static int Base { get; set; }
            public static LocalPlayer_t BaseStruct { get; set; }
        }

        public struct EnemyCrosshair
        {
            public static int Base { get; set; }
            public static Enemy_Crosshair_t BaseStruct { get; set; }
        }

        public struct Enemy
        {
            public static int Base { get; set; }
            public static Enemy_t BaseStruct { get; set; }
    }

        public struct ClientState
        {
            public static int Base { get; set; }
            public static ClientState_t BaseStruct { get; set; }
    }

        public struct GlobalVars
        {
            public static int Base { get; set; }
            public static GlobalVars_t BaseStruct { get; set; }

            public static class Extensions
            {
                public static float ServerTime { get; set; }
            }
        }
    }
}

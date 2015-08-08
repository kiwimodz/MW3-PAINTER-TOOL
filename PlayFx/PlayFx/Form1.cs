using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;


namespace PlayFx
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        // This Tool Was Made Fast For Release Purposes Only So! Sorry If Its A MESS!
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 18; i++)
            {
                dataGridView1.RowCount = 18;
                dataGridView1.Rows[i].Cells[0].Value = i;
                button2.ForeColor = Color.Crimson;
                button1.ForeColor = Color.Crimson;
            }
        }

        #region PS3
        public class PS3
        {

            public static void WriteInt(uint Offset, int Value)
            {
                byte[] buffer = BitConverter.GetBytes(Value);
                Array.Reverse(buffer);
                PS3.SetMemory(Offset, buffer);
            }

            public static float[] ReadFloatLength(uint Offset, int Length)
            {
                byte[] buffer = new byte[Length * 4];
                PS3.GetMemory(Offset, ref buffer);
                Array.Reverse(buffer);
                float[] FArray = new float[Length];
                for (int i = 0; i < Length; i++)
                {
                    FArray[i] = BitConverter.ToSingle(buffer, (Length - 1 - i) * 4);
                }
                return FArray;
            }

            private static uint GetProcessID()
            {
                uint[] array;
                PS3TMAPI.GetProcessList(0, out array);
                return array[0];
            }
            public static Int32 Target = 0;
            public static String GetTargetName()
            {
                if ((Parameters.ConsoleName == null) || (Parameters.ConsoleName == string.Empty))
                {
                    PS3TMAPI.InitTargetComms();
                    PS3TMAPI.TargetInfo targetInfo = new PS3TMAPI.TargetInfo
                    {
                        Flags = PS3TMAPI.TargetInfoFlag.TargetID,
                        Target = Target
                    };
                    PS3TMAPI.GetTargetInfo(ref targetInfo);
                    Parameters.ConsoleName = targetInfo.Name;
                }
                return Parameters.ConsoleName;
            }
            public static UInt32 ProcessID()
            {
                return Parameters.ProcessID;
            }
            public class Parameters
            {
                public static PS3TMAPI.ConnectStatus connectStatus;
                public static string ConsoleName;
                public static string info;
                public static string MemStatus;
                public static uint ProcessID;
                public static uint[] processIDs;
                public static byte[] Retour;
                public static string snresult;
                public static string Status;
                public static string usage;
            }
            public enum ResetTarget
            {
                Hard,
                Quick,
                ResetEx,
                Soft
            }
            public static Boolean Attach()
            {
                Boolean flag = false;
                PS3TMAPI.GetProcessList((Int32)Target, out Parameters.processIDs);
                if (Parameters.processIDs.Length > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                if (flag)
                {
                    ulong num = Parameters.processIDs[0];
                    Parameters.ProcessID = Convert.ToUInt32(num);
                    PS3TMAPI.ProcessAttach((Int32)Target, PS3TMAPI.UnitType.PPU, Parameters.ProcessID);
                    PS3TMAPI.ProcessContinue((Int32)Target, Parameters.ProcessID);
                    Parameters.info = "The Process 0x" + Parameters.ProcessID.ToString("X8") + " Has Been Attached !";
                }
                return flag;
            }
            public static Boolean Connect(Int32 TargetInPS3 = 0)
            {
                Boolean flag = false;
                Target = TargetInPS3;
                flag = PS3TMAPI.SUCCEEDED(PS3TMAPI.InitTargetComms());
                return PS3TMAPI.SUCCEEDED(PS3TMAPI.Connect(TargetInPS3, null));
            }
            public static void GetMemory(uint addr, ref byte[] Buffer)
            {
                PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, Parameters.ProcessID, 0, addr, ref Buffer);
            }
            public static void SetMemory(UInt32 Address, Byte[] bytes)
            {
                PS3TMAPI.ProcessSetMemory(0, PS3TMAPI.UnitType.PPU, Parameters.ProcessID, 0L, (ulong)Address, bytes);
            }
            public static Byte[] GetMem(UInt32 Address, Int32 Length)
            {
                Byte[] buff = new Byte[Length];
                PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, Parameters.ProcessID, 0, Address, ref buff);
                return buff;
            }
            public static Byte[] SetMem(UInt32 Address, Int32 Length)
            {
                Byte[] bytes = new Byte[Length];
                PS3TMAPI.ProcessSetMemory(0, PS3TMAPI.UnitType.PPU, Parameters.ProcessID, 0L, (ulong)Address, bytes);
                return bytes;
            }
            public static float ReadFloat(UInt32 offset)
            {
                byte[] myBuffer = PS3.GetMem(offset, 4);
                Array.Reverse(myBuffer, 0, 4);
                return BitConverter.ToSingle(myBuffer, 0);
            }
            public static void WriteFloat(UInt32 offset, float input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                PS3.SetMemory(offset, array);
            }

            public static Byte ReadByte(UInt32 address)
            {
                return PS3.GetMem(address, 1)[0];
            }

            public static Byte[] ReadBytes(UInt32 address, Int32 length)
            {
                return PS3.GetMem(address, length);
            }

            public static Int32 ReadInt32(UInt32 address)
            {
                Byte[] memory = PS3.GetMem(address, 4);
                Array.Reverse(memory, 0, 4);
                return BitConverter.ToInt32(memory, 0);
            }

            public static float ReadSingle(UInt32 address)
            {
                Byte[] memory = PS3.GetMem(address, 4);
                Array.Reverse(memory, 0, 4);
                return BitConverter.ToSingle(memory, 0);
            }

            public static float[] ReadSingle(UInt32 address, Int32 length)
            {
                Byte[] memory = PS3.GetMem(address, length * 4);
                ReverseBytes(memory);
                float[] numArray = new float[length];
                for (Int32 i = 0; i < length; i++)
                {
                    numArray[i] = BitConverter.ToSingle(memory, ((length - 1) - i) * 4);
                }
                return numArray;
            }

            public static string ReadString(UInt32 address)
            {
                Int32 length = 40;
                Int32 num2 = 0;
                string source = "";
                do
                {
                    Byte[] memory = PS3.GetMem(address + ((UInt32)num2), length);
                    source = source + Encoding.UTF8.GetString(memory);
                    num2 += length;
                }
                while (!source.Contains<char>('\0'));
                Int32 inPS3 = source.IndexOf('\0');
                string str2 = source.Substring(0, inPS3);
                source = string.Empty;
                return str2;
            }

            public static Byte[] ReverseBytes(Byte[] toReverse)
            {
                Array.Reverse(toReverse);
                return toReverse;
            }

            public static void WriteByte(UInt32 address, Byte input)
            {
                PS3.SetMemory(address, new Byte[] { input });
            }

            public static void WriteBytes(UInt32 address, Byte[] input)
            {
                PS3.SetMemory(address, input);
            }

            public static bool WriteBytesToggle(uint Offset, Byte[] On, Byte[] Off)
            {
                bool flag = ReadByte(Offset) == On[0];
                WriteBytes(Offset, !flag ? On : Off);
                return flag;
            }

            public static void WriteInt16(UInt32 address, short input)
            {
                Byte[] array = new Byte[2];
                ReverseBytes(BitConverter.GetBytes(input)).CopyTo(array, 0);
                PS3.SetMemory(address, array);
            }

            public static void WriteInt32(UInt32 address, Int32 input)
            {
                Byte[] array = new Byte[4];
                ReverseBytes(BitConverter.GetBytes(input)).CopyTo(array, 0);
                PS3.SetMemory(address, array);
            }

            public static void WriteSingle(UInt32 address, float input)
            {
                Byte[] array = new Byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                PS3.SetMemory(address, array);
            }

            public static void WriteSingle(UInt32 address, float[] input)
            {
                Int32 length = input.Length;
                Byte[] array = new Byte[length * 4];
                for (Int32 i = 0; i < length; i++)
                {
                    ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, (Int32)(i * 4));
                }
                PS3.SetMemory(address, array);
            }

            public static void WriteString(UInt32 address, String input)
            {
                Byte[] Bytes = Encoding.UTF8.GetBytes(input);
                Array.Resize<byte>(ref Bytes, Bytes.Length + 1);
                PS3.SetMemory(address, Bytes);
            }

            public static void WriteUInt16(UInt32 address, ushort input)
            {
                Byte[] array = new Byte[2];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 2);
                PS3.SetMemory(address, array);
            }

            public static void WriteUInt32(UInt32 address, UInt32 input)
            {
                Byte[] array = new Byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                PS3.SetMemory(address, array);
            }

        }
        #region TMAPI DEX ONLY FOOLS
        public class PS3TMAPI
        {
            private static IntPtr AllocUtf8FromString(string wcharString)
            {
                if (wcharString == null)
                {
                    return IntPtr.Zero;
                }
                byte[] bytes = Encoding.UTF8.GetBytes(wcharString);
                IntPtr destination = Marshal.AllocHGlobal((int)(bytes.Length + 1));
                Marshal.Copy(bytes, 0, destination, bytes.Length);
                Marshal.WriteByte((IntPtr)(destination.ToInt64() + bytes.Length), 0);
                return destination;
            }

            public static SNRESULT Connect(int target, string application)
            {
                if (!Is32Bit())
                {
                    return ConnectX64(target, application);
                }
                return ConnectX86(target, application);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Connect", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ConnectX64(int target, string application);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Connect", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ConnectX86(int target, string application);
            public static SNRESULT Disconnect(int target)
            {
                if (!Is32Bit())
                {
                    return DisconnectX64(target);
                }
                return DisconnectX86(target);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Disconnect", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT DisconnectX64(int target);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Disconnect", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT DisconnectX86(int target);
            public static bool FAILED(SNRESULT res)
            {
                return !SUCCEEDED(res);
            }

            public static SNRESULT GetConnectionInfo(int target, out TCPIPConnectProperties connectProperties)
            {
                connectProperties = null;
                ScopedGlobalHeapPtr ptr = new ScopedGlobalHeapPtr(Marshal.AllocHGlobal(Marshal.SizeOf(typeof(TCPIPConnectProperties))));
                SNRESULT res = Is32Bit() ? GetConnectionInfoX86(target, ptr.Get()) : GetConnectionInfoX64(target, ptr.Get());
                if (SUCCEEDED(res))
                {
                    connectProperties = new TCPIPConnectProperties();
                    Marshal.PtrToStructure(ptr.Get(), connectProperties);
                }
                return res;
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetConnectionInfo", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetConnectionInfoX64(int target, IntPtr connectProperties);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetConnectionInfo", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetConnectionInfoX86(int target, IntPtr connectProperties);
            public static SNRESULT GetConnectStatus(int target, out ConnectStatus status, out string usage)
            {
                IntPtr ptr;
                uint num;
                SNRESULT snresult = Is32Bit() ? GetConnectStatusX86(target, out num, out ptr) : GetConnectStatusX64(target, out num, out ptr);
                status = (ConnectStatus)num;
                usage = Utf8ToString(ptr, uint.MaxValue);
                return snresult;
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetConnectStatus", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetConnectStatusX64(int target, out uint status, out IntPtr usage);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetConnectStatus", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetConnectStatusX86(int target, out uint status, out IntPtr usage);
            public static SNRESULT GetProcessList(int target, out uint[] processIDs)
            {
                processIDs = null;
                uint count = 0;
                SNRESULT res = Is32Bit() ? GetProcessListX86(target, ref count, IntPtr.Zero) : GetProcessListX64(target, ref count, IntPtr.Zero);
                if (!FAILED(res))
                {
                    ScopedGlobalHeapPtr ptr = new ScopedGlobalHeapPtr(Marshal.AllocHGlobal((int)(4 * count)));
                    res = Is32Bit() ? GetProcessListX86(target, ref count, ptr.Get()) : GetProcessListX64(target, ref count, ptr.Get());
                    if (FAILED(res))
                    {
                        return res;
                    }
                    IntPtr unmanagedBuf = ptr.Get();
                    processIDs = new uint[count];
                    for (uint i = 0; i < count; i++)
                    {
                        unmanagedBuf = ReadDataFromUnmanagedIncPtr<uint>(unmanagedBuf, ref processIDs[i]);
                    }
                }
                return res;
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessList", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetProcessListX64(int target, ref uint count, IntPtr processIdArray);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessList", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetProcessListX86(int target, ref uint count, IntPtr processIdArray);
            public static SNRESULT GetTargetFromName(string name, out int target)
            {
                ScopedGlobalHeapPtr ptr = new ScopedGlobalHeapPtr(AllocUtf8FromString(name));
                if (!Is32Bit())
                {
                    return GetTargetFromNameX64(ptr.Get(), out target);
                }
                return GetTargetFromNameX86(ptr.Get(), out target);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetTargetFromName", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetTargetFromNameX64(IntPtr name, out int target);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetTargetFromName", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetTargetFromNameX86(IntPtr name, out int target);
            public static SNRESULT GetTargetInfo(ref TargetInfo targetInfo)
            {
                TargetInfoPriv targetInfoPriv = new TargetInfoPriv
                {
                    Flags = targetInfo.Flags,
                    Target = targetInfo.Target
                };
                SNRESULT res = Is32Bit() ? GetTargetInfoX86(ref targetInfoPriv) : GetTargetInfoX64(ref targetInfoPriv);
                if (!FAILED(res))
                {
                    targetInfo.Flags = targetInfoPriv.Flags;
                    targetInfo.Target = targetInfoPriv.Target;
                    targetInfo.Name = Utf8ToString(targetInfoPriv.Name, uint.MaxValue);
                    targetInfo.Type = Utf8ToString(targetInfoPriv.Type, uint.MaxValue);
                    targetInfo.Info = Utf8ToString(targetInfoPriv.Info, uint.MaxValue);
                    targetInfo.HomeDir = Utf8ToString(targetInfoPriv.HomeDir, uint.MaxValue);
                    targetInfo.FSDir = Utf8ToString(targetInfoPriv.FSDir, uint.MaxValue);
                    targetInfo.Boot = targetInfoPriv.Boot;
                }
                return res;
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3GetTargetInfo", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetTargetInfoX64(ref TargetInfoPriv targetInfoPriv);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3GetTargetInfo", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT GetTargetInfoX86(ref TargetInfoPriv targetInfoPriv);
            public static SNRESULT InitTargetComms()
            {
                if (!Is32Bit())
                {
                    return InitTargetCommsX64();
                }
                return InitTargetCommsX86();
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3InitTargetComms", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT InitTargetCommsX64();
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3InitTargetComms", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT InitTargetCommsX86();
            private static bool Is32Bit()
            {
                return (IntPtr.Size == 4);
            }

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern int MultiByteToWideChar(int codepage, int flags, IntPtr utf8, int utf8len, StringBuilder buffer, int buflen);
            public static SNRESULT PowerOff(int target, bool bForce)
            {
                uint force = bForce ? (uint)1 : 0;
                if (!Is32Bit())
                {
                    return PowerOffX64(target, force);
                }
                return PowerOffX86(target, force);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3PowerOff", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT PowerOffX64(int target, uint force);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PowerOff", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT PowerOffX86(int target, uint force);
            public static SNRESULT PowerOn(int target)
            {
                if (!Is32Bit())
                {
                    return PowerOnX64(target);
                }
                return PowerOnX86(target);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3PowerOn", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT PowerOnX64(int target);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3PowerOn", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT PowerOnX86(int target);
            public static SNRESULT ProcessAttach(int target, UnitType unit, uint processID)
            {
                if (!Is32Bit())
                {
                    return ProcessAttachX64(target, (uint)unit, processID);
                }
                return ProcessAttachX86(target, (uint)unit, processID);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessAttach", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ProcessAttachX64(int target, uint unitId, uint processId);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessAttach", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ProcessAttachX86(int target, uint unitId, uint processId);
            public static SNRESULT ProcessContinue(int target, uint processID)
            {
                if (!Is32Bit())
                {
                    return ProcessContinueX64(target, processID);
                }
                return ProcessContinueX86(target, processID);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessContinue", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ProcessContinueX64(int target, uint processId);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessContinue", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ProcessContinueX86(int target, uint processId);
            public static SNRESULT ProcessGetMemory(int target, UnitType unit, uint processID, ulong threadID, ulong address, ref byte[] buffer)
            {
                if (!Is32Bit())
                {
                    return ProcessGetMemoryX64(target, unit, processID, threadID, address, buffer.Length, buffer);
                }
                return ProcessGetMemoryX86(target, unit, processID, threadID, address, buffer.Length, buffer);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessGetMemory", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ProcessGetMemoryX64(int target, UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessGetMemory", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ProcessGetMemoryX86(int target, UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);
            public static SNRESULT ProcessSetMemory(int target, UnitType unit, uint processID, ulong threadID, ulong address, byte[] buffer)
            {
                if (!Is32Bit())
                {
                    return ProcessSetMemoryX64(target, unit, processID, threadID, address, buffer.Length, buffer);
                }
                return ProcessSetMemoryX86(target, unit, processID, threadID, address, buffer.Length, buffer);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3ProcessSetMemory", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ProcessSetMemoryX64(int target, UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3ProcessSetMemory", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ProcessSetMemoryX86(int target, UnitType unit, uint processId, ulong threadId, ulong address, int count, byte[] buffer);
            private static IntPtr ReadDataFromUnmanagedIncPtr<T>(IntPtr unmanagedBuf, ref T storage)
            {
                storage = (T)Marshal.PtrToStructure(unmanagedBuf, typeof(T));
                return new IntPtr(unmanagedBuf.ToInt64() + Marshal.SizeOf((T)storage));
            }

            public static SNRESULT Reset(int target, ResetParameter resetParameter)
            {
                if (!Is32Bit())
                {
                    return ResetX64(target, (ulong)resetParameter);
                }
                return ResetX86(target, (ulong)resetParameter);
            }

            [DllImport("PS3TMAPIX64.dll", EntryPoint = "SNPS3Reset", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ResetX64(int target, ulong resetParameter);
            [DllImport("PS3TMAPI.dll", EntryPoint = "SNPS3Reset", CallingConvention = CallingConvention.Cdecl)]
            private static extern SNRESULT ResetX86(int target, ulong resetParameter);
            public static bool SUCCEEDED(SNRESULT res)
            {
                return (res >= SNRESULT.SN_S_OK);
            }

            public static string Utf8ToString(IntPtr utf8, uint maxLength)
            {
                int capacity = MultiByteToWideChar(0xfde9, 0, utf8, -1, null, 0);
                if (capacity == 0)
                {
                    throw new Win32Exception();
                }
                StringBuilder buffer = new StringBuilder(capacity);
                capacity = MultiByteToWideChar(0xfde9, 0, utf8, -1, buffer, capacity);
                return buffer.ToString();
            }

            [Flags]
            public enum BootParameter : ulong
            {
                BluRayEmuOff = 4L,
                BluRayEmuUSB = 0x20L,
                DebugMode = 0x10L,
                Default = 0L,
                DualNIC = 0x80L,
                HDDSpeedBluRayEmu = 8L,
                HostFSTarget = 0x40L,
                MemSizeConsole = 2L,
                ReleaseMode = 1L,
                SystemMode = 0x11L
            }

            public enum ConnectStatus
            {
                Connected,
                Connecting,
                NotConnected,
                InUse,
                Unavailable
            }

            [Flags]
            public enum ResetParameter : ulong
            {
                Hard = 1L,
                Quick = 2L,
                ResetEx = 9223372036854775808L,
                Soft = 0L
            }

            private class ScopedGlobalHeapPtr
            {
                private IntPtr m_intPtr = IntPtr.Zero;

                public ScopedGlobalHeapPtr(IntPtr intPtr)
                {
                    this.m_intPtr = intPtr;
                }

                ~ScopedGlobalHeapPtr()
                {
                    if (this.m_intPtr != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(this.m_intPtr);
                    }
                }

                public IntPtr Get()
                {
                    return this.m_intPtr;
                }
            }

            public enum SNRESULT
            {
                SN_E_BAD_ALIGN = -28,
                SN_E_BAD_MEMSPACE = -18,
                SN_E_BAD_PARAM = -21,
                SN_E_BAD_TARGET = -3,
                SN_E_BAD_UNIT = -11,
                SN_E_BUSY = -22,
                SN_E_CHECK_TARGET_CONFIGURATION = -33,
                SN_E_COMMAND_CANCELLED = -36,
                SN_E_COMMS_ERR = -5,
                SN_E_COMMS_EVENT_MISMATCHED_ERR = -39,
                SN_E_CONNECT_TO_GAMEPORT_FAILED = -35,
                SN_E_CONNECTED = -38,
                SN_E_DATA_TOO_LONG = -26,
                SN_E_DECI_ERROR = -23,
                SN_E_DEPRECATED = -27,
                SN_E_DLL_NOT_INITIALISED = -15,
                SN_E_ERROR = -2147483648,
                SN_E_EXISTING_CALLBACK = -24,
                SN_E_FILE_ERROR = -29,
                SN_E_HOST_NOT_FOUND = -8,
                SN_E_INSUFFICIENT_DATA = -25,
                SN_E_LICENSE_ERROR = -32,
                SN_E_LOAD_ELF_FAILED = -10,
                SN_E_LOAD_MODULE_FAILED = -31,
                SN_E_MODULE_NOT_FOUND = -34,
                SN_E_NO_SEL = -20,
                SN_E_NO_TARGETS = -19,
                SN_E_NOT_CONNECTED = -4,
                SN_E_NOT_IMPL = -1,
                SN_E_NOT_LISTED = -13,
                SN_E_NOT_SUPPORTED_IN_SDK_VERSION = -30,
                SN_E_OUT_OF_MEM = -12,
                SN_E_PROTOCOL_ALREADY_REGISTERED = -37,
                SN_E_TARGET_IN_USE = -9,
                SN_E_TARGET_RUNNING = -17,
                SN_E_TIMEOUT = -7,
                SN_E_TM_COMMS_ERR = -6,
                SN_E_TM_NOT_RUNNING = -2,
                SN_E_TM_VERSION = -14,
                SN_S_NO_ACTION = 6,
                SN_S_NO_MSG = 3,
                SN_S_OK = 0,
                SN_S_PENDING = 1,
                SN_S_REPLACED = 5,
                SN_S_TARGET_STILL_REGISTERED = 7,
                SN_S_TM_VERSION = 4
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct TargetInfo
            {
                public PS3TMAPI.TargetInfoFlag Flags;
                public int Target;
                public string Name;
                public string Type;
                public string Info;
                public string HomeDir;
                public string FSDir;
                public PS3TMAPI.BootParameter Boot;
            }

            [Flags]
            public enum TargetInfoFlag : uint
            {
                Boot = 0x20,
                FileServingDir = 0x10,
                HomeDir = 8,
                Info = 4,
                Name = 2,
                TargetID = 1
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct TargetInfoPriv
            {
                public PS3TMAPI.TargetInfoFlag Flags;
                public int Target;
                public IntPtr Name;
                public IntPtr Type;
                public IntPtr Info;
                public IntPtr HomeDir;
                public IntPtr FSDir;
                public PS3TMAPI.BootParameter Boot;
            }

            [StructLayout(LayoutKind.Sequential)]
            public class TCPIPConnectProperties
            {
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0xff)]
                public string IPAddress;
                public uint Port;
            }

            public enum UnitType
            {
                PPU,
                SPU,
                SPURAW
            }
        }
        #endregion
        #endregion
        #region Vezah FPS RPC
        public class RPC
        {
            
            public static uint func_address = 0x0277208;
            
            public static uint GetFuncReturn()
            {
                byte[] ret = new byte[4];
                PS3.GetMemory(0x114AE64, ref ret);
                Array.Reverse(ret);
                return BitConverter.ToUInt32(ret, 0);
            }
            public static void Enable()
            {
                byte[] CheckRPC = new byte[1];
                PS3.GetMemory(0x27720C, ref CheckRPC);
                if (CheckRPC[0] == 0x80)
                {
                    byte[] WritePPC = new byte[] {0x3F,0x80,0x10,0x05,0x81,0x9C,0x00,0x48,0x2C,0x0C,0x00,0x00,0x41,0x82,0x00,0x78,
                                        0x80,0x7C,0x00,0x00,0x80,0x9C,0x00,0x04,0x80,0xBC,0x00,0x08,0x80,0xDC,0x00,0x0C,
                                        0x80,0xFC,0x00,0x10,0x81,0x1C,0x00,0x14,0x81,0x3C,0x00,0x18,0x81,0x5C,0x00,0x1C,
                                        0x81,0x7C,0x00,0x20,0xC0,0x3C,0x00,0x24,0xC0,0x5C,0x00,0x28,0xC0,0x7C,0x00,0x2C,
                                        0xC0,0x9C,0x00,0x30,0xC0,0xBC,0x00,0x34,0xC0,0xDC,0x00,0x38,0xC0,0xFC,0x00,0x3C,
                                        0xC1,0x1C,0x00,0x40,0xC1,0x3C,0x00,0x44,0x7D,0x89,0x03,0xA6,0x4E,0x80,0x04,0x21,
                                        0x38,0x80,0x00,0x00,0x90,0x9C,0x00,0x48,0x90,0x7C,0x00,0x4C,0xD0,0x3C,0x00,0x50,
                                        0x48,0x00,0x00,0x14};
                    PS3.SetMemory(func_address, new byte[] { 0x41 });
                    PS3.SetMemory(func_address + 4, WritePPC);
                    PS3.SetMemory(func_address, new byte[] { 0x40 });
                    Thread.Sleep(10);
                    RPC.DestroyAll();


                }
                else if (CheckRPC[0] == 0x3F)
                {}                
            }
            public static Int32 Call(UInt32 address, params Object[] parameters)
            {
                Int32 length = parameters.Length;
                Int32 index = 0;
                UInt32 count = 0;
                UInt32 Strings = 0;
                UInt32 Single = 0;
                UInt32 Array = 0;
                while (index < length)
                {
                    if (parameters[index] is Int32)
                    {
                        PS3.WriteInt32(0x10050000 + (count * 4), (Int32)parameters[index]);
                        count++;
                    }
                    else if (parameters[index] is UInt32)
                    {
                        PS3.WriteUInt32(0x10050000 + (count * 4), (UInt32)parameters[index]);
                        count++;
                    }
                    else if (parameters[index] is Int16)
                    {
                        PS3.WriteInt16(0x10050000 + (count * 4), (Int16)parameters[index]);
                        count++;
                    }
                    else if (parameters[index] is UInt16)
                    {
                        PS3.WriteUInt16(0x10050000 + (count * 4), (UInt16)parameters[index]);
                        count++;
                    }
                    else if (parameters[index] is Byte)
                    {
                        PS3.WriteByte(0x10050000 + (count * 4), (Byte)parameters[index]);
                        count++;
                    } //Should work now :D let me try
                    else
                    {
                        UInt32 pointer;
                        if (parameters[index] is String)
                        {
                            pointer = 0x10052000 + (Strings * 0x400);
                            PS3.WriteString(pointer, Convert.ToString(parameters[index]));
                            PS3.WriteUInt32(0x10050000 + (count * 4), pointer);
                            count++;
                            Strings++;
                        }
                        else if (parameters[index] is Single)
                        {
                            WriteSingle(0x10050024 + (Single * 4), (Single)parameters[index]);
                            Single++;
                        }
                        else if (parameters[index] is Single[])
                        {
                            Single[] Args = (Single[])parameters[index];
                            pointer = 0x10051000 + Array * 4;
                            WriteSingle(pointer, Args);
                            PS3.WriteUInt32(0x10050000 + count * 4, pointer);
                            count++;
                            Array += (UInt32)Args.Length;
                        }

                    }
                    index++;
                }
                PS3.WriteUInt32(0x10050048, address);
                Thread.Sleep(20);
                return PS3.ReadInt32(0x1005004c);
            }
            private static void WriteSingle(uint address, float input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                PS3.SetMemory(address, array);
            }
            private static byte[] ReverseBytes(byte[] inArray)
            {
                Array.Reverse(inArray);
                return inArray;
            }
            private static void WriteSingle(uint address, float[] input)
            {
                int length = input.Length;
                byte[] array = new byte[length * 4];
                for (int i = 0; i < length; i++)
                {
                    ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, (int)(i * 4));
                }
                PS3.SetMemory(address, array);
            }
            public static void DestroyAll()
            {
                Byte[] clear = new Byte[0xB4 * 1024];
                PS3.SetMemory(0xF0E10C, clear);
            }

            public static Single[] ReadSingle(uint address, int length)
            {
                byte[] mem = PS3.ReadBytes(address, length * 4);
                Array.Reverse(mem);
                float[] numArray = new float[length];
                for (int index = 0; index < length; ++index)
                    numArray[index] = BitConverter.ToSingle(mem, (length - 1 - index) * 4);
                return numArray;
            }
        #endregion     
        #region Offsets
            class Offsets
            {
                public static UInt32 FuncAddr = 0x277208;
                public static UInt32 G_Client = 0x110a280;
                public static UInt32 G_ClientSize = 0x3980;
                public static UInt32 G_Entity = 0xfca280;
                public static UInt32 G_EntitySize = 640;
                public class Funcs
                {
                    public static UInt32 G_Client(Int32 clientIndex, UInt32 Mod = 0)
                    {
                        return ((Offsets.G_Client + Mod) + ((UInt32)(Offsets.G_ClientSize * clientIndex)));
                    }

                    public static UInt32 G_Entity(Int32 clientIndex, UInt32 Mod = 0)
                    {
                        return ((Offsets.G_Entity + Mod) + ((UInt32)(Offsets.G_EntitySize * clientIndex)));
                    }
                }

            }

        }
            #endregion
        #region Restart
        public static void restartGame()
        {
            RPC.Call(0x00223B20);
        }
        #endregion
        #region ServerInFo
        public static class ServerInfo
        {//credits to Seb5594 for this

            public static string GetName(int Client)
            {
                byte[] buffer = new byte[16];
                PS3.GetMemory(0x0110D694 + 0x3980 * (uint)Client, ref buffer);
                string names = Encoding.ASCII.GetString(buffer);
                names = names.Replace("\0", "");
                return names;
            }
            public static String ReturnInfos(Int32 Index)
            {
                return Encoding.ASCII.GetString(PS3.ReadBytes(0x8360d5, 0x100)).Replace(@"\", "|").Split(new char[] { '|' })[Index];

            }
            public static String getHostName()
            {
                String str = ReturnInfos(0x10);
                switch (str)
                {
                    case "Modern Warfare 3":
                        return "Dedicated Server (No Player is Host)";
                    case "":
                        return "You are not In-Game";
                }
                return str;
            }
            public static String getGameMode()
            {
                switch (ReturnInfos(2))
                {
                    case "war":
                        return "Team Deathmatch";
                    case "dm":
                        return "Free for All";
                    case "sd":
                        return "Search and Destroy";
                    case "dom":
                        return "Domination";
                    case "conf":
                        return "Kill Confirmed";
                    case "sab":
                        return "Sabotage";
                    case "koth":
                        return "Head Quartes";
                    case "ctf":
                        return "Capture The Flag";
                    case "infect":
                        return "Infected";
                    case "sotf":
                        return "Hunted";
                    case "dd":
                        return "Demolition";
                    case "grnd":
                        return "Drop Zone";
                    case "tdef":
                        return "Team Defender";
                    case "tjugg":
                        return "Team Juggernaut";
                    case "jugg":
                        return "Juggernaut";
                    case "gun":
                        return "Gun Game";
                    case "oic":
                        return "One In The Chamber";
                }
                return "Unknown Gametype";
            }
            public static String getMapName()
            {
                switch (ReturnInfos(6))
                {
                    case "mp_alpha":
                        return "Lockdown";
                    case "mp_bootleg":
                        return "Bootleg";
                    case "mp_bravo":
                        return "Mission";
                    case "mp_carbon":
                        return "Carbon";
                    case "mp_dome":
                        return "Dome";
                    case "mp_exchange":
                        return "Downturn";
                    case "mp_hardhat":
                        return "Hardhat";
                    case "mp_interchange":
                        return "Interchange";
                    case "mp_lambeth":
                        return "Fallen";
                    case "mp_mogadishu":
                        return "Bakaara";
                    case "mp_paris":
                        return "Resistance";
                    case "mp_plaza2":
                        return "Arkaden";
                    case "mp_radar":
                        return "Outpost";
                    case "mp_seatown":
                        return "Seatown";
                    case "mp_underground":
                        return "Underground";
                    case "mp_village":
                        return "Village";
                    case "mp_aground_ss":
                        return "Aground";
                    case "mp_aqueduct_ss":
                        return "Aqueduct";
                    case "mp_cement":
                        return "Foundation";
                    case "mp_hillside_ss":
                        return "Getaway";
                    case "mp_italy":
                        return "Piazza";
                    case "mp_meteora":
                        return "Sanctuary";
                    case "mp_morningwood":
                        return "Black Box";
                    case "mp_overwatch":
                        return "Overwatch";
                    case "mp_park":
                        return "Liberation";
                    case "mp_qadeem":
                        return "Oasis";
                    case "mp_restrepo_ss":
                        return "Lookout";

                    case "mp_terminal_cls":
                        return "Terminal";
                }
                return "Unknown Map";
            }
        }
        #endregion
        #region PlayerFX

        public static uint Element(uint Index)
        {
            return 0xF0E10C + ((Index) * 0xB4);
        }

        public static uint StoreText(uint Index, decimal Client, string Text, int Font, float FontScale, int X, int Y, decimal R, decimal G, decimal B, decimal A, decimal R1, decimal G1, decimal B1, decimal A1)
        {
            uint elem = Element(Index);
            PS3.WriteInt(elem + 0x84, RPC.Call(0x1BE6CC, Text));
            PS3.WriteInt(elem + 0x24, Font);
            PS3.WriteFloat(elem + 0x14, FontScale);
            PS3.WriteFloat(elem + 0x4, X);
            PS3.WriteFloat(elem + 0x8, Y);
            PS3.SetMemory(elem + 0xa7, new byte[] { 7 });
            PS3.SetMemory(elem + 0x30, new byte[] { (byte)R, (byte)G, (byte)B, (byte)A });
            PS3.SetMemory(elem + 0x8C, new byte[] { (byte)R1, (byte)G1, (byte)B1, (byte)A1 });
            PS3.WriteInt(elem + 0xA8, (int)Client);
            System.Threading.Thread.Sleep(20);
            PS3.WriteInt(elem, 1);
            return elem;
            // Credits to xCSBKx. Used this from his MysteryBox source :P 
        }

        public static float[] GetOrigin(uint Client)
        {
            return PS3.ReadFloatLength(0x110a29c + (Client * 0x3980), 3);
        }

        public static float[] GetAngles(uint Client)
        {
            return PS3.ReadFloatLength(0x110a3d8 + (Client * 0x3980), 3);
        }

        public static float[] AnglesToForward(float[] Origin, float[] Angles, uint Distance)
        {
            float diff = Distance;
            float num = ((float)Math.Sin((Angles[0] * Math.PI) / 180)) * diff;
            float num1 = (float)Math.Sqrt(((diff * diff) - (num * num)));
            float num2 = ((float)Math.Sin((Angles[1] * Math.PI) / 180)) * num1;
            float num3 = ((float)Math.Cos((Angles[1] * Math.PI) / 180)) * num1;
            return new float[] { Origin[0] + num3, Origin[1] + num2, Origin[2] - num };
        }

        public static uint SetFX(UInt32 Client, Int32 FX_Value, uint Distance = 120)
        {
            float[] Origin = GetOrigin(Client);
            float[] Angles = GetAngles(Client);
            Origin[2] += 59;
            return PlayFX(AnglesToForward(Origin, Angles, Distance), Angles, FX_Value);
        }

        public static uint PlayFX(float[] Origin, float[] Angles, int EffectIndex)
        {
            uint ent = (uint)RPC.Call(0x1C0B7C, Origin, 0x56); //G_Temp
            PS3.WriteInt32(ent + 0xA0, EffectIndex);
            PS3.WriteInt32(ent + 0xD8, 0);
            PS3.WriteFloat(ent + 0x40, 0f);
            PS3.WriteFloat(ent + 0x44, 0f);
            PS3.WriteFloat(ent + 0x3C, 270f);
            return ent;
        }

        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PS3.Connect();
                System.Threading.Thread.Sleep(300); 
                PS3.Attach();
                System.Threading.Thread.Sleep(300);
                RPC.Enable();
                button1.ForeColor = Color.Green;
                button1.Text = "Process Attached";
            }
            catch
            {
                button1.ForeColor = Color.Crimson;
                button1.Text = "Fatal Error!!";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Painting [ OFF ]")
            {
                Painter.Start();
                button2.Text = "Painting [ ON ]";
                button2.ForeColor = Color.Green;
                StoreText(500 + ((uint)FXC.Value), (FXC.Value), "Press And Hold [{+speed_throw}] To Start Drawing", 7, 0.8f, 195, 300, 255, 255, 255, 255, 0, 0, 0, 0);
            }
            else if (button2.Text == "Painting [ ON ]")
            {
                Painter.Stop();
                button2.Text = "Painting [ OFF ]";
                button2.ForeColor = Color.Crimson;
                for (uint i = 0; i < 18; i++)
                {
                    PS3.SetMemory(Element(500 + (uint)i), new byte[0xB4]);
                }

            }
        }

        private void Fx_Tick(object sender, EventArgs e)
        {
            if (PS3.ReadFloat(0x110a5f8 + ((uint)FXC.Value * 0x3980)) > 0)//Checks if L1 is down// Thanks xCSBKx
            {
                SetFX((uint)FXC.Value, (Int32)FXValue.Value);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            restartGame();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                HOST.Text = ServerInfo.getHostName();
                MAP.Text = ServerInfo.getMapName();
                MODE.Text = ServerInfo.getGameMode();

                for (int i = 0; i < 18; i++)
                {
                    dataGridView1.Enabled = true;
                    dataGridView1.RowCount = 18;
                    dataGridView1.Rows[i].Cells[0].Value = i;
                    dataGridView1.Rows[i].Cells[1].Value = ServerInfo.GetName(i);
                    Application.DoEvents();

                }
            }
            catch
            {
                for (int i = 0; i < 18; i++)
                {
                    HOST.Text = "Null";
                    MAP.Text = "Null";
                    MODE.Text = "Null";
                    dataGridView1.RowCount = 18;
                    dataGridView1.Rows[i].Cells[0].Value = i;
                    dataGridView1.Rows[i].Cells[1].Value = "";
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            FXC.Value = dataGridView1.CurrentRow.Index;
        }

        private void Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Box.Text == "Green") && (MAP.Text == "Dome"))
            {
                FXValue.Value = 92;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Dome"))
            {
                FXValue.Value = 66;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Seatown"))
            {
                FXValue.Value = 90;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Seatown"))
            {
                FXValue.Value = 64;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Arkaden"))
            {
                FXValue.Value = 98;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Arkaden"))
            {
                FXValue.Value = 72;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Bakaara"))
            {
                FXValue.Value = 88;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Bakaara"))
            {
                FXValue.Value = 62;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Resistance"))
            {
                FXValue.Value = 84;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Resistance"))
            {
                FXValue.Value = 58;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Downturn"))
            {
                FXValue.Value = 95;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Downturn"))
            {
                FXValue.Value = 69;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Bootleg"))
            {
                FXValue.Value = 105;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Bootleg"))
            {
                FXValue.Value = 79;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Bootleg"))
            {
                FXValue.Value = 105;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Bootleg"))
            {
                FXValue.Value = 79;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Carbon"))
            {
                FXValue.Value = 132;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Carbon"))
            {
                FXValue.Value = 106;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Hardhat"))
            {
                FXValue.Value = 100;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Hardhat"))
            {
                FXValue.Value = 74;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Lockdown"))
            {
                FXValue.Value = 86;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Lockdown"))
            {
                FXValue.Value = 60;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Village"))
            {
                FXValue.Value = 85;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Village"))
            {
                FXValue.Value = 59;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Fallen"))
            {
                FXValue.Value = 102;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Fallen"))
            {
                FXValue.Value = 76;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Outpost"))
            {
                FXValue.Value = 118;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Outpost"))
            {
                FXValue.Value = 92;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Interchange"))
            {
                FXValue.Value = 83;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Interchange"))
            {
                FXValue.Value = 57;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Underground"))
            {
                FXValue.Value = 109;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Underground"))
            {
                FXValue.Value = 83;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Mission"))
            {
                FXValue.Value = 87;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Mission"))
            {
                FXValue.Value = 61;
            }
            else if ((Box.Text == "Green") && (MAP.Text == "Terminal"))
            {
                FXValue.Value = 92;
            }
            else if ((Box.Text == "Red") && (MAP.Text == "Terminal"))
            {
                FXValue.Value = 66;
            }
        }

    }
}
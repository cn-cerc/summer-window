using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace vine_window_standard
{
    public class Externs
    {
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(String Name); //调用win api将指定名称的打印机设置为默认打印机
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumPrinters(PrinterEnumFlags Flags, string Name, uint Level, IntPtr pPrinterEnum, uint cbBuff, ref uint pcbNeeded, ref uint pcReturned);

        /// <summary>
        /// 获取默认打印机
        /// </summary>
        private static PrintDocument fPrintDocument = new PrintDocument();
        public static String DefaultPrinter
        {
            get { return fPrintDocument.PrinterSettings.PrinterName; }
        }


       [FlagsAttribute]
        public enum PrinterEnumFlags
        {
            PRINTER_ENUM_DEFAULT = 0x00000001,
            PRINTER_ENUM_LOCAL = 0x00000002,
            PRINTER_ENUM_CONNECTIONS = 0x00000004,
            PRINTER_ENUM_FAVORITE = 0x00000004,
            PRINTER_ENUM_NAME = 0x00000008,
            PRINTER_ENUM_REMOTE = 0x00000010,
            PRINTER_ENUM_SHARED = 0x00000020,
            PRINTER_ENUM_NETWORK = 0x00000040,
            PRINTER_ENUM_EXPAND = 0x00004000,
            PRINTER_ENUM_CONTAINER = 0x00008000,
            PRINTER_ENUM_ICONMASK = 0x00ff0000,
            PRINTER_ENUM_ICON1 = 0x00010000,
            PRINTER_ENUM_ICON2 = 0x00020000,
            PRINTER_ENUM_ICON3 = 0x00040000,
            PRINTER_ENUM_ICON4 = 0x00080000,
            PRINTER_ENUM_ICON5 = 0x00100000,
            PRINTER_ENUM_ICON6 = 0x00200000,
            PRINTER_ENUM_ICON7 = 0x00400000,
            PRINTER_ENUM_ICON8 = 0x00800000,
            PRINTER_ENUM_HIDE = 0x01000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PRINTER_INFO_1
        {
            int flags;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDescription;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pComment;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PRINTER_INFO_2
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pServerName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPrinterName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pShareName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPortName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDriverName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pComment;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pLocation;
            public IntPtr pDevMode;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pSepFile;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pPrintProcessor;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDatatype;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public uint Attributes;
            public uint Priority;
            public uint DefaultPriority;
            public uint StartTime;
            public uint UntilTime;
            public uint Status;
            public uint cJobs;
            public uint AveragePPM;
        }


        // 获取网络打印机
        public static PRINTER_INFO_1[] GetEnumPrinters(PrinterEnumFlags flags)
        {
            PRINTER_INFO_1[] printerInfo1 = new PRINTER_INFO_1[] { };
            uint pcbNeeded = 0;
            uint pcReturned = 0;
            IntPtr pPrInfo4 = IntPtr.Zero;
            uint size = 0;
            if (EnumPrinters(flags, null, 1, IntPtr.Zero, size, ref pcbNeeded, ref pcReturned))
            {
                return printerInfo1;
            }
            if (pcbNeeded != 0)
            {
                pPrInfo4 = Marshal.AllocHGlobal((int)pcbNeeded);
                size = pcbNeeded;
                EnumPrinters(flags, null, 1, pPrInfo4, size, ref pcbNeeded, ref pcReturned);
                if (pcReturned != 0)
                {
                    printerInfo1 = new PRINTER_INFO_1[pcReturned];
                    int offset = pPrInfo4.ToInt32();
                    Type type = typeof(PRINTER_INFO_1);
                    int increment = Marshal.SizeOf(type);
                    for (int i = 0; i < pcReturned; i++)
                    {
                        printerInfo1[i] = (PRINTER_INFO_1)Marshal.PtrToStructure(new IntPtr(offset), type);
                        offset += increment;
                    }
                    Marshal.FreeHGlobal(pPrInfo4);
                }
            }

            return printerInfo1;
        }

        // 获取本地打印机
        public static PRINTER_INFO_2[] GetEnumPrinters2()
        {
            PRINTER_INFO_2[] printerInfo2 = new PRINTER_INFO_2[] { };
            uint pcbNeeded = 0;
            uint pcReturned = 0;
            IntPtr pPrInfo4 = IntPtr.Zero;
            if (EnumPrinters(PrinterEnumFlags.PRINTER_ENUM_LOCAL, null, 2, IntPtr.Zero, 0, ref pcbNeeded, ref pcReturned))
            {
                return printerInfo2;
            }
            if (pcbNeeded != 0)
            {
                pPrInfo4 = Marshal.AllocHGlobal((int)pcbNeeded);
                EnumPrinters(PrinterEnumFlags.PRINTER_ENUM_LOCAL, null, 2, pPrInfo4, pcbNeeded, ref pcbNeeded, ref pcReturned);
                if (pcReturned != 0)
                {
                    printerInfo2 = new PRINTER_INFO_2[pcReturned];
                    int offset = pPrInfo4.ToInt32();
                    for (int i = 0; i < pcReturned; i++)
                    {
                        printerInfo2[i] = (PRINTER_INFO_2)Marshal.PtrToStructure(new IntPtr(offset), typeof(PRINTER_INFO_2));
                        offset += Marshal.SizeOf(typeof(PRINTER_INFO_2));
                    }
                    Marshal.FreeHGlobal(pPrInfo4);
                }
            }

            return printerInfo2;
        }

        public static List<string> getAllPrinters()
        {
            List<string> listPrinters = new List<string>();

            PRINTER_INFO_2[] printers = GetEnumPrinters2();
            foreach (PRINTER_INFO_2 printer in printers)
            {
                listPrinters.Add(printer.pPrinterName);
            }
            PRINTER_INFO_1[] printers2 = GetEnumPrinters(PrinterEnumFlags.PRINTER_ENUM_LOCAL);
            for (int i = 0; i < printers2.Length; i++)
            {
                listPrinters.Add(printers2[i].pName);
            }
            return listPrinters;
        }
    }
}

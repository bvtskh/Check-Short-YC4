using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CheckShort_YC4.Entities
{
    public class Settings
    {
        public class Port
        {
            public static string PortName = "COM1";
        }
        public class Option
        {
            public static string ProcessName;
            public static int SleepTime = 300;
            public static string SignalOK = "#OK*";
            public static string SignalNG = "#NG*";
            public static string SignalBAR = "#BAR*";
            public static string SignalRST = "#RST*";
            public static string SignalReady = "#READY*";
            public static string SignalERROR = "#ERROR*";
            public static string SignalCHECK = "#CHECKING*";
        }

        public class State
        {
            public static int Waiting = 0; // đợi tín hiệu trả về từ sensor
            public static int Ready = 1; // Sẵn sàng đọc barcode
            public static int Stop = 2; // khi cổng COM không kết nối
            public static int Finish = 3; // Không làm gì cả
        }
        public static void Read()
        {
            Port.PortName = Ultils.GetValueRegistryKey("PortName");
        }
        public static void Write()
        {
            Ultils.WriteRegistry("PortName", Port.PortName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
namespace 无线采集
{
    class Record
    {
        public static bool[] comadd = new bool[216];
        public static byte[,] netAdd = new byte[216, 8];
        public static SerialPort sr;
        public static string[] modulename = new string[216];
        public static string path;
        public static string projectName;
        public static byte[] intervib = new byte[4];
        public static byte[] interdef = new byte[4];
             
    }
}

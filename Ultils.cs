using CheckShort_YC4.Business;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace CheckShort_YC4
{
    public static class Ultils
    {
        public static string GetRunningVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static void RegisterInStartup(bool isChecked, string executablePath)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue("ApplicationName", executablePath);
            }
            else
            {
                registryKey.DeleteValue("ApplicationName");
            }
        }

        /// <summary>
        /// create log
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="productionId"></param>
        /// <param name="status"></param>
        /// <param name="process"></param>
        public static void CreateFileLog(string model, string productId, string status, string process, string dateCheck)
        {

            string fileName = $"{dateCheck}_{productId}.txt";
            string folderRoot = $@"{GetValueRegistryKey("OutputLog")}\";

            bool exists = Directory.Exists(folderRoot);
            if (!exists)
                Directory.CreateDirectory(folderRoot);

            string path = folderRoot + fileName;
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine($"{model}|{productId}|{dateCheck}|{status}|{process}");
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.WriteLine($"{model}|{productId}|{dateCheck}|{status}|{process}");
                    tw.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <param name="newline"></param>
        /// <returns></returns>
        public static string ReadLastLine(string path, Encoding encoding, string newline)
        {
            string line = null;
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (reader.Peek() == -1)
                    {
                        return line;
                    }
                }
                reader.Close();
            }
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int CountLine(string path)
        {
            int count = 0;
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(file))
            {
                while (reader.ReadLine() != null)
                {
                    count++;
                }
                reader.Close();
                return count;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string GetValueInLine(string path, int line)
        {
            string value;
            using (var sr = new StreamReader(path, Encoding.UTF8, true))
            {
                for (int i = 1; i < line; i++)
                {
                    sr.ReadLine();
                }
                value = sr.ReadLine();
                sr.Dispose();
                return value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string GetLine(string path, int line)
        {
            string value;
            using (var sr = new StreamReader(path))
            {
                for (int i = 1; i < line; i++)
                {
                    sr.ReadLine();
                }
                value = sr.ReadLine();
                sr.Dispose();
                return value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="content"></param>
        public static void WriteRegistry(string keyName, string content)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SUPPORT_WIP\DX_SUPPORT\Configs");
            if (!string.IsNullOrEmpty(keyName) && !string.IsNullOrEmpty(content))
            {
                key.SetValue(keyName, content);
                key.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="content"></param>
        public static void WriteRegistryArray(string keyName, string content)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SUPPORT_WIP\Station_Initialize_Boards\Configs");
            if (!string.IsNullOrEmpty(keyName) && !string.IsNullOrEmpty(content))
            {
                string exitsValue = GetValueRegistryKey(keyName);
                if (exitsValue != null)
                {
                    exitsValue += content + ";";
                    key.SetValue(keyName, exitsValue);
                }
                else
                {
                    key.SetValue(keyName, content + ";");
                }
                key.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetValueRegistryKey(string keyName)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SUPPORT_WIP\DX_SUPPORT\Configs");
            string value = null;
            if (key != null)
            {
                if (key.GetValue(keyName) != null)
                {
                    value = key.GetValue(keyName).ToString();
                    key.Close();
                    return value;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileProcessName(string filePath)
        {
            Process[] procs = Process.GetProcesses();
            string fileName = Path.GetFileName(filePath);

            foreach (Process proc in procs)
            {
                if (proc.MainWindowHandle != new IntPtr(0) && !proc.HasExited)
                {
                    ProcessModule[] arr = new ProcessModule[proc.Modules.Count];

                    foreach (ProcessModule pm in proc.Modules)
                    {
                        if (pm.ModuleName == fileName)
                            return proc.ProcessName;
                    }
                }
            }

            return null;
        }
        public static string GetFullNameByProcess(string sortName)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.MainWindowTitle.Contains(sortName))
                {
                    return proc.MainWindowTitle;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (File.Open(filePath, FileMode.Open)) { }
                return false;
            }
            catch (IOException e)
            {
                var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);

                return errorCode == 32 || errorCode == 33;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }


        const int ERROR_SHARING_VIOLATION = 32;
        const int ERROR_LOCK_VIOLATION = 33;
        private static bool IsFileLocked(Exception exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == ERROR_SHARING_VIOLATION || errorCode == ERROR_LOCK_VIOLATION;
        }

        public static bool CanReadFile(string filePath)
        {
            //Try-Catch so we dont crash the program and can check the exception
            try
            {
                //The "using" is important because FileStream implements IDisposable and
                //"using" will avoid a heap exhaustion situation when too many handles  
                //are left undisposed.
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    if (fileStream != null)
                        fileStream.Close();  //This line is me being overly cautious, fileStream will never be null unless an exception occurs... and I know the "using" does it but its helpful to be explicit - especially when we encounter errors - at least for me anyway!
                }
            }
            catch (IOException ex)
            {
                //THE FUNKY MAGIC - TO SEE IF THIS FILE REALLY IS LOCKED!!!
                if (IsFileLocked(ex))
                {
                    // do something, eg File.Copy or present the user with a MsgBox - I do not recommend Killing the process that is locking the file
                    return false;
                }
            }
            finally
            {
            }
            return true;
        }
        public static string MakePO(string po)
        {
            return po.Contains("-") ? $"{po.LeftOf('-').Right(6)}{Convert.ToInt32(po.RightOf('-')).ToString("D4")}" : po.Right(10);
        }
        public static string GetMACAddress()
        {
            ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMOS.Get();
            string macAddress = String.Empty;
            foreach (ManagementObject objMO in objMOC)
            {
                object tempMacAddrObj = objMO["MacAddress"];

                if (tempMacAddrObj == null) //Skip objects without a MACAddress
                {
                    continue;
                }
                if (macAddress == String.Empty) // only return MAC Address from first card that has a MAC Address
                {
                    macAddress = tempMacAddrObj.ToString();
                }
                objMO.Dispose();
            }
            macAddress = macAddress.Replace(":", "");
            return macAddress;
        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            var a = host.AddressList;
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static string FriendlyName()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
        }
        public static bool IsWindowsActivated()
        {
            ManagementScope scope = new ManagementScope(@"\\" + System.Environment.MachineName + @"\root\cimv2");
            scope.Connect();

            SelectQuery searchQuery = new SelectQuery("SELECT * FROM SoftwareLicensingProduct WHERE ApplicationID = '55c92734-d682-4d71-983e-d6ec3f16059f' and LicenseStatus = 1");
            ManagementObjectSearcher searcherObj = new ManagementObjectSearcher(scope, searchQuery);

            using (ManagementObjectCollection obj = searcherObj.Get())
            {
                return obj.Count > 0;
            }
        }
        public static string GetGuiIdApp()
        {
            var assembly = typeof(Program).Assembly;
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            var id = attribute.Value;
            return id;
        }
        //public static bool IsWindows64bit()
        //{
        //    return Environment.Is64BitOperatingSystem;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace CSharp_Get_Disk_Drive_Info
{
    class Program
    {
        static void Main(string[] args)
        {
            RetrieveInfoOperatingSystem();
            RetrieveInfoLogicalDisk();

            Console.ReadLine();
        }

        static void RetrieveInfoOperatingSystem() // Get info for Operating System
        {
            ManagementPath path = new ManagementPath()
            {
                NamespacePath = @"root\cimv2",
                Server = "[COMPUTER-NAME_OR_IP]"
            };

            ManagementScope scope = new ManagementScope(path);
            scope.Connect();

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection collection = searcher.Get();

            Console.WriteLine(" --** Operating System **--\n");

            foreach (ManagementObject m in collection)
            {
                Console.WriteLine("  Computer Name : {0}", m["csname"]);
                Console.WriteLine("  Windows Directory : {0}", m["WindowsDirectory"]);
                Console.WriteLine("  Operating System: {0}", m["Caption"]);
                Console.WriteLine("  Version: {0}", m["Version"]);
                Console.WriteLine("  Manufacturer : {0}", m["Manufacturer"]);
                Console.WriteLine();

            }
        }

        static void RetrieveInfoLogicalDisk() // Get Info for Logical Disk
        {
            ManagementPath path = new ManagementPath()
            {
                NamespacePath = @"root\cimv2",
                Server = "[COMPUTER-NAME_OR_IP]"
            };

            ManagementScope scope = new ManagementScope(path);
            scope.Connect();

            SelectQuery queryLD = new SelectQuery("SELECT * FROM Win32_LogicalDisk"); // condition : WHERE DeviceID = 3, only for Local Disk

            ManagementObjectSearcher searcherLD = new ManagementObjectSearcher(scope, queryLD);
            ManagementObjectCollection queryCollectionLD = searcherLD.Get();

            Console.WriteLine(" --** Logical Disk **--\n");

            foreach (ManagementObject mo in queryCollectionLD)
            {
                Console.WriteLine("  Disk Name : {0}", mo["Name"]);
                Console.WriteLine("  Disk Size : {0}", mo["Size"]);
                Console.WriteLine("  FreeSpace : {0}", mo["FreeSpace"]);
                Console.WriteLine("  Disk DeviceID : {0}", mo["DeviceID"]);

                switch ((uint)(mo["DriveType"]))
                {
                    case 1:
                        {
                            Console.WriteLine("  DriveType: No root directory.");
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("  DriveType: Removable drive.");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("  DriveType: Local hard disk.");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("  DriveType: Network disk.");
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("  DriveType: Compact disk.");
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("  DriveType: RAM disk.");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("  Drive type could not be determined.");
                            break;
                        }
                }

                Console.WriteLine("  Disk VolumeName : {0}", mo["VolumeName"]);
                Console.WriteLine("  Disk SystemName : {0}", mo["SystemName"]);
                Console.WriteLine("  Disk VolumeSerialNumber : {0}", mo["VolumeSerialNumber"]);
                Console.WriteLine("  MediaType : {0}", mo["MediaType"]);
                Console.WriteLine();
            }
        }
    }
}

using Ivi.Visa.Interop;
using System;

namespace VisaNi {
    internal class Program {
        static void Main(string[] args) {
            string visaAddress = string.Empty;
            string command = string.Empty;

            if (args.Length < 2)
            {
                //Console.WriteLine("COM port parameter is missing.");
                // Environment.Exit(1); // ถ้าคุณต้องการออกจากโปรแกรม
                
                //visaAddress = "USB0::0x05FF::0xEE3A::LCRY2150C02023::INSTR";
                //command = "*IDN?";
                //command = "TRig_MoDe SINGLE";
                //command = "TRig_MoDe STOP";
                //command = "TRig_MoDe?";
            }
            else
            {
                visaAddress = args[0];
                command = args[1];
            }

            // สร้างอินสแตนซ์ของ VisaResourceManager
            ResourceManager rm = new ResourceManager();

            // เปิดการเชื่อมต่อกับอุปกรณ์ผ่าน Visa
            FormattedIO488 visaDevice = new FormattedIO488();

            try
            {
                if (string.IsNullOrEmpty(visaAddress))
                {
                    string[] visaList = rm.FindRsrc("?*");
                    foreach (string list in visaList)
                    {
                        Console.WriteLine(list);
                    }
                }
                else
                {
                    // เปิดการเชื่อมต่อกับอุปกรณ์ที่มีที่อยู่ VISA
                    visaDevice.IO = (IMessage)rm.Open(visaAddress, AccessMode.NO_LOCK, 2000, "");

                    // ส่งคำสั่งไปยังอุปกรณ์
                    visaDevice.WriteString(command, true);

                    // รับข้อมูลจากอุปกรณ์
                    string response = "No Error";
                    if (command.Contains("?"))
                    {
                        response = visaDevice.ReadString();
                    }

                    // แสดงผลลัพธ์
                    Console.Write(response);
                }
            } catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            } finally
            {
                // ปิดการเชื่อมต่ออุปกรณ์
                if (!string.IsNullOrEmpty(visaAddress))
                {
                    visaDevice.IO.Close();
                }
            }

            //Console.ReadLine();
        }
    }
}

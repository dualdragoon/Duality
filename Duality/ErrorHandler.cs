using System;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Diagnostics;

namespace Duality
{
    /// <summary>
    /// Handles crashes/errors.
    /// </summary>
    public static class ErrorHandler
    {
        private static bool
            existingRecordFiles = true;
        private static int
            numOfRecordFiles = 0,
            identifyRecordNum = 0;
        private static string
            operatingSystem, computerName, computerManufacturer,
            numOfCPU, numOfLogicCPU, totalRAM,
            nameCPU, nameGPU,
            computerModel, recordName;
        private static FileStream fs;
        private static StreamWriter sw;
        private static StreamReader sr;
        private static DateTime currentDateTime = DateTime.Now;

        /// <summary>
        /// Records a new Error.
        /// </summary>
        /// <param name="errorLevel">How severe of a problem is occurring.</param>
        /// <param name="errorCode">Specific identification number, usually 101-199.</param>
        /// <param name="helpfulInfo">What may have caused the error, possible info on how to fix.</param>
        /// <param name="errorDetails">Exception object in catch.</param>
        public static void RecordError(int errorLevel, int errorCode, String helpfulInfo, String errorDetails)
        {
            try
            {
                fs = new FileStream(@"Error Folder\"+ recordName,
                                FileMode.Append,
                                FileAccess.Write);
                sw = new StreamWriter(fs);

                if (errorLevel == 1)
                {
                    sw.WriteLine(currentDateTime.ToString() + "\tINFORMATION");
                    sw.WriteLine("   Error Code:\t\t" + errorCode);
                    sw.WriteLine("   Helpful Info:\t" + helpfulInfo);
                    sw.WriteLine("");
                }
                if (errorLevel == 2)
                {
                    sw.WriteLine(currentDateTime.ToString() + "\tWarning");
                    sw.WriteLine("   Error Code:\t\t" + errorCode);
                    sw.WriteLine("   Helpful Info:\t" + helpfulInfo);
                    sw.WriteLine("   Details:");
                    sw.WriteLine(errorDetails);
                    sw.WriteLine("");
                }
                if (errorLevel == 3)
                {
                    sw.WriteLine(currentDateTime.ToString() + "\tError");
                    sw.WriteLine("   Error Code:\t\t" + errorCode);
                    sw.WriteLine("   Helpful Info:\t" + helpfulInfo);
                    sw.WriteLine("   Details:");
                    sw.WriteLine(errorDetails);
                    sw.WriteLine("");
                }
                sw.Close();
                fs.Close();
                if (errorLevel == 3)
                {
                    MessageBox.Show("Critical Error!\nError code: " + errorCode + "\nRefer to " + recordName +
                        "\nLocation: " + Path.GetFullPath(recordName).ToString(), "Runtime Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception d)
            {
                MessageBox.Show("WARNING:\nUnable to create an new error record!/n" + d.ToString(),
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// On-Start handler initialization.
        /// </summary>
        public static void Initialize()
        {
            GatherSystemInfo();
            System.IO.Directory.CreateDirectory("Error Folder");
            try
            {
                //Checks to see how many record files exists
                while (existingRecordFiles)
                {
                    if (File.Exists(@"Error Folder\Error_Record_" + (numOfRecordFiles + 1) + ".txt"))
                    {
                        numOfRecordFiles++;
                        Console.WriteLine("Found Record File");
                    }
                    else
                    {
                        existingRecordFiles = false;
                    }
                }

                //Check to see if there is an existing record for the current computer
                for (int i = 1; i <= numOfRecordFiles; i++)
                {
                    if (CheckFileForComputerID(@"Error Folder\Error_Record_" + i + ".txt"))
                    {
                        identifyRecordNum = i;
                        continue;
                    }
                }

                //If no file was found or no file that matched
                if (identifyRecordNum == 0)
                {
                    Console.WriteLine("No Matching record");
                    Console.WriteLine("Current session will be recorded in Error_Record_" + (numOfRecordFiles + 1) + ".txt");
                    CreateNewRecord(@"Error Folder\Error_Record_" + (numOfRecordFiles + 1) + ".txt");
                    recordName = "Error_Record_" + (numOfRecordFiles + 1) + ".txt";
                }
                else
                {
                    Console.WriteLine("Found Matching Record");
                    Console.WriteLine("Current event was recorded in Error_Record_" + identifyRecordNum + ".txt");
                    recordName = "Error_Record_" + identifyRecordNum + ".txt";
                }
            }
            catch (Exception d)
            {
                MessageBox.Show("WARNING:\nThis game session is unable to record error data.\n" + d.ToString(),
                    "Error Record Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Creates a new Error Record log if a matching one does not exist.
        /// </summary>
        /// <param name="filename">Name of the Error Record.</param>
        private static void CreateNewRecord(string filename)
        {
            try
            {
                fs = new FileStream(filename,
                            FileMode.Create,
                            FileAccess.Write);
                sw = new StreamWriter(fs);

                sw.WriteLine("---------------ERROR RECORD-------------");
                sw.WriteLine("File Created " + currentDateTime.ToString());
                sw.WriteLine("");
                sw.WriteLine("---------------SYSTEM INFO--------------");
                sw.WriteLine("Operating System:\t" + operatingSystem);
                sw.WriteLine("Computer Name:\t\t" + computerName);
                sw.WriteLine("Computer Manufacturer:\t" + computerManufacturer);
                sw.WriteLine("Computer Model:\t\t" + computerModel);
                sw.WriteLine("Computer GPU:\t\t" + nameGPU);
                sw.WriteLine("Computer CPU:\t\t" + nameCPU);
                sw.WriteLine("Number of CPUs:\t\t" + numOfCPU);
                sw.WriteLine("Number of Logical CPUs:\t" + numOfLogicCPU);
                sw.WriteLine("Total amount of RAM:\t" + totalRAM);
                sw.WriteLine("");
                sw.WriteLine("---------------ERROR INFO---------------");

                sw.Close();
                fs.Close();
                Console.WriteLine("Created Error Record Successfully");
            }
            catch (Exception d)
            {
                MessageBox.Show("WARNING:\nUnable to create an error record for this system!/n" + d.ToString(),
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Checks for matching Error Record.
        /// </summary>
        /// <param name="filename">Name of the Error Record.</param>
        /// <returns>True if they're equal, False if they're not.</returns>
        private static bool CheckFileForComputerID(string filename)
        {
            string data;
            fs = new FileStream(filename,
                        FileMode.Open,
                        FileAccess.Read);
            sr = new StreamReader(fs);

            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();
            sr.ReadLine();
            data = sr.ReadLine().Substring(18);
            if (operatingSystem.Equals(data))
            {
                data = sr.ReadLine().Substring(16);
                if (computerName.Equals(data))
                {
                    data = sr.ReadLine().Substring(23);
                    if (computerManufacturer.Equals(data))
                    {
                        data = sr.ReadLine().Substring(17);
                        if (computerModel.Equals(data))
                        {
                            data = sr.ReadLine().Substring(15);
                            if (nameGPU.Equals(data))
                            {
                                data = sr.ReadLine().Substring(15);
                                if (nameCPU.Equals(data))
                                {
                                    data = sr.ReadLine().Substring(17);
                                    if (numOfCPU.Equals(data))
                                    {
                                        data = sr.ReadLine().Substring(24);
                                        if (numOfLogicCPU.Equals(data))
                                        {
                                            data = sr.ReadLine().Substring(21);
                                            if (totalRAM.Equals(data))
                                            {
                                                sr.Close();
                                                fs.Close();
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            sr.Close();
            fs.Close();
            return false;
        }

        /// <summary>
        /// Gathers info for the Error Record.
        /// </summary>
        private static void GatherSystemInfo()
        {
            ManagementObjectSearcher searcher;
            try
            {
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (ManagementObject os in searcher.Get())
                {
                    operatingSystem = os["Caption"].ToString();
                    break;
                }
                Console.WriteLine("OS data retrieved");
            }
            catch
            {
                Console.WriteLine("OS data retrieval FAILED");
            }
            try
            {
                searcher = new ManagementObjectSearcher("\\root\\CIMV2", "SELECT * FROM Win32_ComputerSystem");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    computerName = queryObj["Name"].ToString();
                    computerManufacturer = queryObj["Manufacturer"].ToString();
                    computerModel = queryObj["Model"].ToString();
                    numOfCPU = queryObj["NumberOfProcessors"].ToString();
                    numOfLogicCPU = queryObj["NumberOfLogicalProcessors"].ToString();
                    totalRAM = (Convert.ToDouble(queryObj["TotalPhysicalMemory"]) / 1073741824).ToString() + " GB";    //convert byte to gigabyte
                }
                Console.WriteLine("Computer System Data retrieved");
            }
            catch
            {
                Console.WriteLine("Computer System Data retrieval FAILED");
            }
            try
            {
                searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject sysProcessor in searcher.Get())
                {
                    nameCPU = sysProcessor["Name"].ToString();
                }
                Console.WriteLine("Processor Data retrieved");
            }
            catch
            {
                Console.WriteLine("Processor Data retrieval FAILED");
            }
            try
            {
                searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
                foreach (ManagementObject sysVC in searcher.Get())
                {
                    nameGPU = sysVC["Name"].ToString();
                }
                Console.WriteLine("GPU Data retrieved");
            }
            catch
            {
                Console.WriteLine("GPU Data retrieval FAILED");
            }
        }
    }
}
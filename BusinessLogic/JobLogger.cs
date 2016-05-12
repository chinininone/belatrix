using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace BusinessLogic
{
    public class JobLogger
    {
        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        private static bool _logToDatabase;
        
        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase)
        {
            _logError = Convert.ToBoolean(ConfigurationManager.AppSettings["LogErrors"].ToString());
            _logMessage = Convert.ToBoolean(ConfigurationManager.AppSettings["LogMessages"].ToString());
            _logWarning = Convert.ToBoolean(ConfigurationManager.AppSettings["LogWarnings"].ToString());
            _logToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }

        private static void ValidateRequest(string Message, bool isMessage, bool isWarning, bool isError)
        {
            Message.Trim();
            if (String.IsNullOrEmpty(Message))
            {
                throw new Exception("Text message missing");
            }
            if (!_logToConsole && !_logToFile && !_logToDatabase)
            {
                throw new Exception("Invalid configuration");
            }
            if ((!_logError && !_logMessage && !_logWarning) || (!isMessage && !isWarning && !isError))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

        }

        private static void LogToDataBase(string Message, int typeMessage)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
                
                SqlCommand command = new SqlCommand("LogData_Insert",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@var_Message", Message);
                command.Parameters.AddWithValue("@int_Type", typeMessage);
                connection.Open();
                command.ExecuteNonQuery();

            }catch(Exception ex)
            {
                throw ex;
            }           

        }

        private static void LogToFile(string Message, int typeMessage)
        {
            try
            {
                string logContent = typeMessage.ToString() + "-" + DateTime.Now.ToString() + "-" + Message;
                string filePath=ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToString("yyyyMMdd")+".txt";

                if (File.Exists(filePath))
                {
                    File.AppendAllText(filePath, Environment.NewLine + logContent);
                }
                else
                {
                    File.WriteAllText(filePath, logContent);
                }            
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void LogToConsole(string Message, int typeMessage)
        {
            try
            {
                switch (typeMessage)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Yellow;                        
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }

                Console.WriteLine(DateTime.Now.ToShortDateString() + Message);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void LogMessage(string Message, bool isMessage, bool isWarning, bool isError)
        {
            try
            {
                //Check parameters and settings
                ValidateRequest(Message, isMessage, isWarning, isError);

                //Setting type of message to show, no type (0) by default
                int typeMessage = 0;
                if (isMessage && _logMessage)
                    typeMessage = 1;
                else if (isError && _logError)
                    typeMessage = 2;
                else if (isWarning && _logWarning)
                    typeMessage = 3;
                
                //Must be an allowed type to be showed
                if(typeMessage>0)
                {
                    if (_logToDatabase)
                        LogToDataBase(Message, typeMessage);

                    if (_logToFile)
                        LogToFile(Message, typeMessage);
                
                    if (_logToConsole)
                        LogToConsole(Message, typeMessage);
                }else
                    throw new Exception("There is not message to log");


            }catch(Exception ex)
            {
                throw ex;

            }
        }
    }

}

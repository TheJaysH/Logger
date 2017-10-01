using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Logger
{
    public class Log
    {
        private static string LogFile { get; set; }
      
        private static string User
        {
            get
            {
                return Environment.UserName;
            }
        }

        /// <summary>
        /// Return the current Date String 
        /// TODO: Allow Timezone slection etc..
        /// </summary>
        private string Now
        {   get
            {
                return $"{DateTime.Now.ToLocalTime().ToLongTimeString()} {DateTime.Now.ToLocalTime().ToShortDateString()}";
            }
        }

        public enum Type
        {
            info,warn,error,debug,verbose
        }

        /// <summary>
        /// Main constructor
        /// </summary>
        public Log(string _LogFile)
        {
            LogFile = _LogFile;
        }

        /// <summary>
        /// Write a blank line to the lgofile;
        /// </summary>
        public void WriteLine()
        {
            WriteToFile(string.Empty);
        }

        /// <summary>
        /// Write a line of text to the logfile
        /// </summary>
        /// <param name="Message">String to log</param>
        /// <param name="ShowUser">Display the username of the person running the program</param>
        /// <param name="type">Type of the log [Default: debug]</param>
        public void WriteLine(string Message, bool ShowUser, Type type = Type.debug)
        {
            string output = string.Empty;

            if (ShowUser)
                output  = $"[{Now}][{type.ToString()}][{User}] - {Message}";
            else
                output = $"[{Now}][{type.ToString()}] - {Message}";

            WriteToFile(output);
        }

        /// <summary>
        /// Write a line of text to the logfile
        /// </summary>
        /// <param name="Message">String to log</param>
        /// <param name="type">Type of the log [Default: debug]</param>
        public void WriteLine(string Message, Type type = Type.debug)
        {
            string output = $"[{Now}][{type.ToString()}] - {Message}\n";
            WriteToFile(output);
        }

        /// <summary>
        /// Open the log file for writing, and append the output
        /// </summary>
        /// <param name="output"></param>
        private void WriteToFile(string output)
        {           
            try
            {
                var file = File.AppendText(LogFile);
                file.WriteLine(output);
                file.Close();
            }
            catch (Exception ex)
            {
                Error err = new Error(ex);                                
            }
        }
    }

    /// <summary>
    /// Class for handling internal error on a seperate thread
    /// </summary>
    internal class Error
    {
        private static string Title { get; set; }
        private static Thread ErrorThread { get; set; }

        public Error(Exception ex, string _Title = "Error")
        {
            Title = "Logger " + _Title;

            ErrorThread = new Thread(delegate ()
            {
                DisplayError(ex);
            });

            ErrorThread.Start();

            return;
        }

        public Error(string Message, string _Title = "Error")
        {
            Title = "Logger " + _Title;

            ErrorThread = new Thread(delegate ()
            {
                DisplayError(Message);
            });

            ErrorThread.Start();

            return;
        }

        /// <summary>
        /// Display an error, based off an Exception
        /// </summary>
        /// <param name="ex"></param>
        private static void DisplayError(Exception ex)
        {
            string message = ex.Message;
            string stack_trace = ex.StackTrace;

            string error =  $"ERROR: {message}\n" +
                            $"STACK: {stack_trace}";

            MessageBox.Show(error, Title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// Display an error string
        /// </summary>
        /// <param name="Message"></param>
        private static void DisplayError(string Message)
        {
            string error = $"ERROR: {Message}\n";

            MessageBox.Show(error, Title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }
    }
}

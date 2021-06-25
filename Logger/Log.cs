using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Log : ILog
    {
        private static int counter = 0;

        private static readonly Lazy<Log> instance = new Lazy<Log>(() => new Log());
        public static Log GetInstance
        {
            get
            { 
                return instance.Value;
            }
        }
        private Log() 
        {
            counter++;
            Console.WriteLine("Counter Value is" + counter.ToString());
        }

        public void LogException(string message)
        {
            string date = DateTime.Now.ToShortDateString();
            string correcteddate = date.Replace('/', '-');
            string fileName = string.Format("{0}_{1}.log", "Exception", correcteddate);
            string basedir = @"C:\Users\Public\Downloads";
            string logFilePath = string.Format(@"{0}\{1}", basedir, fileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            using (StreamWriter writer = new StreamWriter(logFilePath,true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
    }
}

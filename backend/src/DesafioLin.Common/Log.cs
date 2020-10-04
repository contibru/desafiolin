using System;
using System.IO;
using System.Text;

namespace DesafioLin.Common
{
    public static class Log
    {
        public static string applicationFolder;

        private static bool WriteInLog(string text, string path, bool hours = true, bool append = true)
        {
            try
            {
                // Se o diretório não existe o cria.
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                FileInfo fi = new FileInfo(path);
                // Se o arquivo possuir mais de 10MB.
                if (fi.Exists && fi.Length > 1024000)
                {
                    File.Move(path, path + '_' + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute);
                }

                TextWriter tw = new StreamWriter(path, append, Encoding.UTF8);

                tw.WriteLine("[ " + DateTime.Now + " ] " + text);
                tw.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool WriteBugLog(string text, bool hours = true, bool append = true)
        {
            return WriteInLog(text, Path.Combine(applicationFolder, "logs", "buglog.txt"), hours, append);
        }

        public static bool WriteAnyLog(string text, string logName, bool hours = true, bool append = true)
        {
            return WriteInLog(text, Path.Combine(applicationFolder, "logs", logName), hours, append);
        }
    }
}
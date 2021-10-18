using System;
using System.IO;
using System.Reflection;


namespace ParserHtml
{
    /// <summary>
    /// Класс предназначен для записи сообщений в лог файл
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Путь к файлу с логом
        /// </summary>
        public static string FilePath { get; }

        static Logger()
        {
            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\log.txt";
        }
        /// <summary>
        /// Метод создаёт файл log.txt в директории с программой
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLog(string message)
        {
            try
            {
                using var sw = new StreamWriter(FilePath, true);

                sw.WriteLine("{0,-23} {1}", DateTime.Now.ToString() + ":", message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

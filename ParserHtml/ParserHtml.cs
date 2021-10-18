using System;
using System.IO;
using System.Net;
using System.Reflection;


namespace ParserHtml
{
    public enum State { RUN, EXIT };
    class ParserHtml : IWorkingWithHtml
    {
        private State _state;
        public Uri Url { get; private set; }

        public void LoadWeb(string url)
        {
            Url = new Uri(url);
        }

        /// <summary>
        /// Метод загружает скачанную страницу в файл по указаному пути
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        private void uploadToFile(string filePath)
        {
            try
            {
                var webClient = new WebClient();
                webClient.DownloadFile(Url, filePath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void Run()
        {
            _state = State.RUN;

            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Test.txt";

            while (_state != State.EXIT)
            {
                try
                {
                    Console.Write("Введите Url(exit для выхода): ");

                    var url = Console.ReadLine();

                    if (url == "exit")
                    {
                        _state = State.EXIT;
                        continue;
                    }

                    LoadWeb(url);
                    uploadToFile(filePath);

                    Console.WriteLine("Загрузка началась...");

                    var cleanedText = TextCleaner.ClearText(filePath);

                    var dictionaryText = new TextAnalyzer();
                    dictionaryText.FillingDictionary(cleanedText);
                    dictionaryText.Show();

                    Console.WriteLine("Сохранение в базу данных...");
                    var db = new DataBase();
                    db.UploadToSql(dictionaryText.Dictionary);
                    Console.WriteLine("Словарь успешно сохранён в базу данных. ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Logger.WriteLog(ex.Message);
                    Console.WriteLine($"Лог сохранён: {Logger.FilePath}");
                }
            }
        }
    }
}

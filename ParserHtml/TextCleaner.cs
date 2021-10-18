using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ParserHtml
{
    /// <summary>
    /// Класс для очистки текстового файла от различных лишних символов
    /// </summary>
    public static class TextCleaner
    {
        /// <summary>
        /// Метод очищает текст от тэгов в указаннном по пути файле
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns>Возвращает строку, очищенную от тэгов</returns>
        public static string ClearText(string filePath)
        {
            try
            {
                var cleanedText = File.ReadAllText(filePath);

                cleanedText = cleaningWithRegex(cleanedText);

                return cleanedText;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Сетод для очистки текста с помощью регулярных выражений
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string cleaningWithRegex(string input)
        {
            input = Regex.Replace(input, @"[^А-Яа-я]", " ").Trim();
            input = Regex.Replace(input, @"\s{2,}", " ");
            return input;
        }


        /// <summary>
        /// Метод очищает от тэгов
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Строка очищеннаяот тэгов Html</returns>
        private static string clearningTags(string input)
        {
            Dictionary<string, string> tagsHtml = new Dictionary<string, string>
            {
                ["<script"] = "</script>",
                ["<noscript"] = "</noscript>",
                ["<body"] = ">",
                ["<input"] = ">",
                ["<style"] = "/>",
                ["<link"] = ">",
                ["<button"] = ">",
                ["<path"] = "/>",
                ["<img"] = ">"
            };

            foreach (var item in tagsHtml)
            {
                input = deleteBetweenTags(input, item.Key, item.Value);
            }
            return input;
        }


        /// <summary>
        /// Метод для удаления строк между указанными тэгами
        /// </summary>
        /// <param name="input">Входная строка</param>
        /// <param name="openingTag">Открывающий тэг</param>
        /// <param name="closingTag">Закрывающий тэг</param>
        /// <returns>Новая строка без тэгов</returns>
        private static string deleteBetweenTags(string input, string openingTag, string closingTag)
        {
            while (true)
            {
                var indexOpeningTag = input.IndexOf(openingTag);
                var indexClosingTag = input.IndexOf(closingTag);

                if (indexOpeningTag != -1 && (indexClosingTag - indexOpeningTag) > 0)
                {
                    input = input.Remove(indexOpeningTag, 
                                         indexClosingTag - indexOpeningTag + closingTag.Length);
                }
                else
                {
                    break;
                }
            }
            return input;
        }
    }

}


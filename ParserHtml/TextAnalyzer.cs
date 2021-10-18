using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserHtml
{
    /// <summary>
    /// Класс содержит словарь уникальных слов
    /// </summary>
    public class TextAnalyzer
    {
        private Dictionary<string, int> _dictionaryOfUniqueWords;

        public Dictionary<string, int> Dictionary { get => _dictionaryOfUniqueWords; }

        public TextAnalyzer()
        {
            _dictionaryOfUniqueWords = new Dictionary<string, int>();
        }

        /// <summary>
        /// Вывод на экран словаря
        /// </summary>
        public void Show()
        {
            foreach (var pair in _dictionaryOfUniqueWords.OrderBy(pair => pair.Value))
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Метод разбивает текст на отдельные слова и заполняет словарь
        /// </summary>
        /// <param name="text">Входной текст</param>
        public void FillingDictionary(string text)
        {
            char[] charSeparators =
            {
             ' ', ',', '.', '!', '?', '"', ';', ':',
             '[', ']', '(', ')', '\n', '\r', '\t'
           };

            text = text.ToUpper();

            try
            {

                var wordArray = text.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in wordArray)
                {
                    if (_dictionaryOfUniqueWords.ContainsKey(word) == false)
                    {
                        int count = countingRepetitions(word, wordArray);

                        _dictionaryOfUniqueWords.Add(word, count);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Метод для подсчета количества повторений слова в массиве слов
        /// </summary>
        /// <param name="word">Проверяеме слово</param>
        /// <param name="wordArray">Массив слов, в котором необходимо посчитать количество повторений</param>
        /// <returns>Количество повторений слова</returns>
        private int countingRepetitions(string word, string[] wordArray)
        {
            int count = 0;

            foreach (var item in wordArray)
            {
                if (item == word)
                {
                    count++;
                }
            }
            return count;
        }

    }
}

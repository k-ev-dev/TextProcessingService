using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibProcessing {
    class MultiThreading {

        public StringBuilder text;
        public MultiThreading(StringBuilder text) {
            this.text = text;
        }


        //СОЗДАНИЕ МЕТОДА, ДЛЯ МНОГОПОТОЧНОГО ВЫБОРА СИМВОЛОВ, КОТОРЫЕ НЕОБХОДИМО УБРАТЬ
        public List<char> exeptionChars = new List<char>();
        public void ParallelReading(int i) {
            char ch = Convert.ToChar(text[i]);
            if ((!exeptionChars.Contains(ch)) &
               (char.IsControl(ch) | char.IsWhiteSpace(ch) | (char.IsPunctuation(ch) & (ch != '-') & (ch != '\'')))) {
                exeptionChars.Add(ch);
            }
        }

        //СОЗДАНИЕ МЕТОДА, ДЛЯ МНОГОПОТОЧНОЙ ЗАМЕНЫ НЕНУЖНЫХ СИМВОЛОВ ДЛЯ ДАЛЬНЕЙШЕГО ФОРМАТИРОВАНИЯ
        public void RemoveChar(char c) {
            text.Replace(c, '#');
        }

        //СОЗДАНИЕ МЕТОДА, ДЛЯ МНОГОПОТОЧНОЙ ОБРАБОТКИ МАССИВА СТРОК И СОЗДАНИЯ СЛОВАРЯ С УНИКАЛЬНЫМИ СЛОВАМИ
        public ConcurrentDictionary<string, int> forOutPut = new ConcurrentDictionary<string, int>();
        public void WordsChoise(string item) {
            forOutPut.AddOrUpdate(item, 1, (k, v) => v + 1);
        }

        //МЕТОД, ИСПОЛЬЗУЮЩИЙ МНОГОПОТОЧНОСТЬ
        public IEnumerable<KeyValuePair<string, int>> AllNewMethods() {

            Parallel.For(0, text.Length, ParallelReading);
            text.Replace("--", "#");
            text.Replace("- ", "#");
            Parallel.ForEach<char>(exeptionChars, RemoveChar);
            string[] wordsArray = text.ToString().ToUpper().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            Parallel.ForEach<string>(wordsArray, WordsChoise);
            Dictionary<string, int> finaly = forOutPut.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            return finaly;
        }
    }
}

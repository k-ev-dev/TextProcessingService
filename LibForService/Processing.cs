using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibProcessing {
    public class Processing {

        private Dictionary<string, int> getResult(StringBuilder text) {

            //ВЫБОР СИМВОЛОВ, КОТОРЫЕ НЕОБХОДИМО УБРАТЬ
            List<char> exeptionChars = new List<char>();                        
                for (int i = 0; i < text.Length; i++) {
                    char ch = text[i];
                    if ((!exeptionChars.Contains(ch)) &
                       (char.IsControl(ch) | char.IsWhiteSpace(ch) | (char.IsPunctuation(ch) & (ch != '-') & (ch != '\'')))) {
                        exeptionChars.Add(ch);
                    }
                }
            

            //ЗАМЕНА НЕНУЖНЫХ СИМОВОЛОВ ДЛЯ ДАЛЬНЕЙШЕГО ФОРМАТИРОВАНИЯ
            text.Replace("--", "#");
            text.Replace("- ", "#");
            foreach (char item in exeptionChars) {
                text.Replace(item, '#');
            }
            //ПОЛУЧЕНИЕ МАССИВА ВСЕХ СЛОВ
            string[] wordsArray = text.ToString().ToUpper().Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            //ПОЛУЧЕНИЕ СЛОВАРЯ С УНИКАЛЬНЫМИ СЛОВАМИ
            Dictionary<string, int> forOutPut = new Dictionary<string, int>();
            foreach (string item in wordsArray) {
                if (!forOutPut.ContainsKey(item)) {
                    forOutPut.Add(item, 1);
                }
                else {
                    forOutPut[item] += 1;
                }
            }
            //СОРТИРОВКА ЭЛЕМЕНТОВ ПО ВСТРЕЧАЕМОСТИ СЛОВ
            return forOutPut = forOutPut.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

    }
}

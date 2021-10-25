using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ServiceClient {
    class Program {
        static void Main(string[] args) {
            Dictionary<string, int> forOutPut = new Dictionary<string, int>();

            //СОЗДАНИЕ StringBuilder НА ОСНОВЕ ФАЙЛА
            StringBuilder book;
            using (StreamReader reading = new StreamReader(@"..\..\война и мир.txt")) {
                book = new StringBuilder(reading.ReadToEnd());
            }

            Console.WriteLine("Какой метод хотите использовать:" +
                "\nВведите \"1\", если метод без многопоточности." +
                "\nВведите \"2\", если многопоточный метод.");
            string Answear = Console.ReadLine();

            if (Answear == "1") {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (var client = new ServiceReference.Service1Client()) {
                    forOutPut = client.GetData(book);
                }
                stopwatch.Stop();
                TimeSpan timeResult = stopwatch.Elapsed;
                Console.WriteLine($"Время выполнения метода без многопоточности: {timeResult.TotalSeconds} секунд.");
            }

            if (Answear == "2"){
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using (var client = new ServiceReference.Service1Client()) {
                    forOutPut = client.GetDataFast(book);
                }
                stopwatch.Stop();
                TimeSpan timeResult = stopwatch.Elapsed;
                Console.WriteLine($"Время выполнения метода, использующего многопоточность: {timeResult.TotalSeconds} секунд.");
            }

            //ЗАПИСЬ В ФАЙЛ
            using (StreamWriter writing = new StreamWriter(@"..\..\myoutput.txt", false)) {
                foreach (KeyValuePair<string, int> word in forOutPut) {
                    writing.WriteLine(($"{word.Key}  {word.Value}").PadLeft(20));
                }
            }

            Console.WriteLine(@"Работа завершена. Файл-вывод => \ServiceClient\myoutput.txt.");
            Console.ReadLine();

        }
    }
}


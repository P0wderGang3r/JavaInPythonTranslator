using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInPythonTranslator
{
    internal class Miscelaneous
    {

        public static List<String> getInputText(String pathToFile)
        {
            List<String> inputText = new();

            try
            {
                StreamReader lexClasses = new(pathToFile);
                while (!lexClasses.EndOfStream)
                {
                    inputText.Add(lexClasses.ReadLine());
                }
            }
            catch
            {
                inputText.Add("Некорректный путь до файла");

                Console.WriteLine(inputText);

                return inputText;
            }

            return inputText;
        }
    }
}

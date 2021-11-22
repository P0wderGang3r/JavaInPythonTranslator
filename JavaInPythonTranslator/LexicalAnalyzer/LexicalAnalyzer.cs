using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JavaInPythonTranslator
{
    internal static class LexicalAnalyzer
    {
        private readonly static String defaultPath = "./LexicalClasses.xml";
        private static bool isCorrectlyInitialized = false;
        private static XDocument lexicalClasses = new();

        public static bool getIsCorrectlyInitialized()
        {
            return isCorrectlyInitialized;
        }

        public static void initLexicalAnalyzer()
        {
            //Я не знаю, как в дебаг/релиз папки с соответствующими билдами автоматически закидывать LexicalClasses.xml,
            //оттого при первой компиляции нужно закинуть этот файл в соответствующую папку самостоятельно
            Console.WriteLine("Введите путь до xml файла с лексическими классами.\nВ случае ошибки при чтении данного файла будет использован файл ./LexicalClasses.xml");

            try
            {
                StreamReader lexClasses = new(Console.ReadLine());
                lexicalClasses = XDocument.Parse(lexClasses.ReadToEnd());
                isCorrectlyInitialized = true;
                Console.WriteLine(lexicalClasses);
            } 
            catch
            {
                try
                {
                    StreamReader lexClasses = new(defaultPath);
                    lexicalClasses = XDocument.Parse(lexClasses.ReadToEnd());
                    isCorrectlyInitialized = true;
                    Console.WriteLine(lexicalClasses);
                }
                catch
                {
                    Console.WriteLine("Классы лексического анализатора не были найдены.");
                }
            }
        }

        public static void runLexicalAnalyze(String inputFile)
        {

        }
    }
}

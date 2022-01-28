
namespace JavaInPythonTranslator
{
    /// <summary>
    /// <br>type - тип лексемы</br>
    /// <br>value - содержимое лексемы</br>
    /// </summary>
    struct LexList
    {
        public string type;
        public string value;

        public LexList(string type, string value) : this()
        {
            this.type = type;
            this.value = value;
        }
    }

    class Program
    {
        public static int Main(String[] args)
        {
            //-----------------------------Чтение из файла-------------------------------


            Console.WriteLine("Введите путь до текстового файла с кодом для трансляции");

            List<String> inputText = Miscelaneous.getInputText(Console.ReadLine());

            if (String.Equals("Некорректный путь до файла", inputText[0]))
            {
                return 1;
            }

            if (Globals.logVerboseLevel >= 1)
                for (int i = 0; i < inputText.Count; i++)
                    Console.WriteLine(inputText[i]);


            //---------------------------Лексический анализ------------------------------

            
            List<LexList> lexList = new();

            if (!LexicalAnalyzer.initLexAnalyzer())
            {
                Console.WriteLine("Ошибка при заполнении лексических классов");
                return 2;
            }

            if (!LexicalAnalyzer.runLexScan(lexList, inputText))
            {
                Console.WriteLine("Ошибка при анализе входного текста");
                return 3;
            }


            if (Globals.logVerboseLevel >= 1)
                for (int i = 0; i < lexList.Count; i++)
                    Console.WriteLine(lexList[i].type + " " + lexList[i].value);


            //--------------------------Синтаксический анализ----------------------------


            SyntaxAnalyzer analyzer = new SyntaxAnalyzer();

            //Так, для теста
            Console.WriteLine(analyzer.startRule(lexList));


            //--------------------------Семантический анализ-----------------------------





            //-----------------------------Генератор кода--------------------------------






            return 0;
        }
    }
}
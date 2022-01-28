
namespace JavaInPythonTranslator
{
    /// <summary>
    /// <br>type - тип лексемы</br>
    /// <br>text - содержимое лексемы</br>
    /// </summary>
    struct LexList
    {
        public string type;
        public string text;

        public LexList(string type, string text) : this()
        {
            this.type = type;
            this.text = text;
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
                    Console.WriteLine(lexList[i].type + " " + lexList[i].text);


            //--------------------------Синтаксический анализ----------------------------




            return 0;
        }
    }
}
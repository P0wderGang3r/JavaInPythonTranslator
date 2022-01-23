
namespace JavaInPythonTranslator
{
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

            String pathToFile = Console.ReadLine();

            List<String> inputText = Miscelaneous.getInputText(pathToFile);

            if (String.Equals("Некорректный путь до файла", inputText[0]))
            {
                return 1;
            }

            if (Globals.logVerboseLevel >= 1)
                for (int i = 0; i < inputText.Count; i++)
                    Console.WriteLine(inputText[i]);

            //---------------------------Лексический анализ------------------------------

            
            List<LexList> lexList = new();

            LexicalAnalyzer.initLexAnalyzer();

            if (!LexicalAnalyzer.getIsCorrectlyInitialized())
            {
                return 2;
            }

            if (!LexicalAnalyzer.runLexScan(lexList, inputText))
                return 3;


            if (Globals.logVerboseLevel >= 1)
                for (int i = 0; i < lexList.Count; i++)
                    Console.WriteLine(lexList[i].type + " " + lexList[i].text);


            //--------------------------Синтаксический анализ----------------------------




            return 0;
        }
    }
}
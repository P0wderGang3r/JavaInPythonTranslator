namespace JavaInPythonTranslator
{
    class Program
    {

        static String getInputText(String pathToFile)
        {
            String inputText = "Некорректный путь до файла";

            try
            {
                StreamReader lexClasses = new(pathToFile);
                inputText = lexClasses.ReadToEnd();
            }
            catch
            {
                Console.WriteLine(inputText);

                return inputText;
            }

            return inputText;
        }

        public static int Main(String[] args)
        {
            /*
            //-----------------------------Чтение из файла-------------------------------

            Console.WriteLine("Введите путь до текстового файла с кодом для трансляции");

            String pathToFile = Console.ReadLine();

            String inputText = getInputText(pathToFile);

            if (String.Equals(GlobalErrorMessages.errorMessage1, inputText))
            {
                return 1;
            }

            //---------------------------Лексический анализ------------------------------
            */
            LexicalAnalyzer.initLexicalAnalyzer();

            if (!LexicalAnalyzer.getIsCorrectlyInitialized())
            {
                return 2;
            }

            //LexicalAnalyzer.runLexicalAnalyze(inputText);

            //--------------------------Синтаксический анализ----------------------------

            return 0;
        }
    }
}
namespace JavaInPythonTranslator
{
    internal static class LexicalAnalyzer
    {
        private readonly static String defaultPath = "./LexicalClasses.txt";
        private static bool isCorrectlyInitialized = false;
        private static List<LexicalClasses> lexicalClasses = new List<LexicalClasses>();

        public static bool getIsCorrectlyInitialized()
        {
            return isCorrectlyInitialized;
        }

        /**Короче, файл LexicalClasses.txt содержит список регулярных выражений, определяющих класс объекта. Единственное - нужно уточнить, что присутствует на выходе лексического анализатора.*/
        public static void initLexicalAnalyzer()
        {
            //Я не знаю, как в дебаг/релиз папки с соответствующими билдами автоматически закидывать LexicalClasses.txt,
            //оттого при первой компиляции нужно закинуть этот файл в соответствующую папку самостоятельно
            Console.WriteLine("Введите путь до xml файла с лексическими классами.\nВ случае ошибки при чтении данного файла будет использован файл ./LexicalClasses.xml");

            try
            {
                StreamReader lexClasses = new(Console.ReadLine());
                String[]? lexicalClassesLinear = lexClasses.ReadToEnd().Replace("\n", "~").Split("~");

                for (int i = 0; i < lexicalClassesLinear.Length; i+=2)
                {
                    lexicalClasses.Add(new LexicalClasses(lexicalClassesLinear[i], lexicalClassesLinear[i + 1]));
                }

                for (int i = 0; i < lexicalClasses.Count; i++)
                {
                    Console.WriteLine(lexicalClasses[i].getLexClass() + " " + lexicalClasses[i].getRegEx());
                }

                isCorrectlyInitialized = true;
            } 
            catch
            {
                try
                {
                    StreamReader lexClasses = new(defaultPath);
                    String[]? lexicalClassesLinear = lexClasses.ReadToEnd().Replace("\n", "~").Split("~");

                    for (int i = 0; i < lexicalClassesLinear.Length; i += 2)
                    {
                        lexicalClasses.Add(new LexicalClasses(lexicalClassesLinear[i], lexicalClassesLinear[i + 1]));
                    }

                    for (int i = 0; i < lexicalClasses.Count; i++)
                    {
                        Console.WriteLine(i + " " + lexicalClasses[i].getLexClass() + " " + lexicalClasses[i].getRegEx());
                    }

                    isCorrectlyInitialized = true;
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

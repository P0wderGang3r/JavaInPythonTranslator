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
                inputText.Add(" ");

                inputText[0] = ("Некорректный путь до файла");

                Console.WriteLine(inputText[0]);

                return inputText;
            }

            return inputText;
        }
    }
}

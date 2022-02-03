using static JavaInPythonTranslator.Globals;

namespace JavaInPythonTranslator
{
    // [V] Анализ повторяющихся объявлений
    // [-] Анализ совпадения объявленных и используемых типов
    // [V] Анализ совпадения подключенных библиотек и используемых
    // [V] Анализ использования переменной, заранее не объявленной

    /// <summary>
    /// Точка входа в семантический анализатор
    /// </summary>
    internal class SemanticAnalyzer
    {
        public static bool runSemanticScan(List<TreeNode> treeNodes)
        {
            if (RepeatedInitializationsCheck.repeatedInitializations(treeNodes))
            {
                Console.WriteLine("Ошибка: повторное объявление переменной");
                return true;
            }

            if (IsInitializedIdentificatorsUsedCheck.checkMain(treeNodes))
            {
                Console.WriteLine("Ошибка: не объявленная переменная");
                return true;
            }
            return false;
        }
    }
}

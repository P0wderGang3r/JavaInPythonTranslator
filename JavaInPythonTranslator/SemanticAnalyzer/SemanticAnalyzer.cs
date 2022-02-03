using static JavaInPythonTranslator.Globals;

namespace JavaInPythonTranslator
{
    // [V] Анализ повторяющихся объявлений
    //Анализ совпадения объявленных и используемых типов
    //Анализ совпадения подключенных библиотек и используемых
    //Анализ использования переменной, заранее не объявленной

    /// <summary>
    /// Точка входа в семантический анализатор
    /// </summary>
    internal class SemanticAnalyzer
    {
        public static bool runSemanticScan(List<TreeNode> treeNodes)
        {
            if (RepeatedInitializationsCheck.repeatedInitializations(treeNodes))
                return true;

            return false;
        }
    }
}

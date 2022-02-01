using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.EndPoints;

namespace JavaInPythonTranslator
{
    static internal class AssignmentRules
    {
        #region <присваивание> → <начало идентификатора> <оператор присваивания> <выражение>

        #endregion

        #region Правило <выражение> → <арифметическое выражение> | <логическое выражение> | <унарная арифметическая операция> | <значение> | <побитовое выражение>
        static string expressionCheck(List<LexList> lexems)
        {
            return "success";
        }
        #endregion

        #region <арифметическое выражение> → <арифметический операнд> <арифметический оператор> <арифметический операнд> 

        #endregion

        #region <логическое выражение> → !<логический операнд> | <логический операнд> <логический бинарный оператор> <логический операнд> | <логический операнд>

        #endregion

        #region <унарная арифметическая операция> → <начало идентификатора> <унарный арифметический оператор> | <унарный арифметический оператор> <начало идентификатора> | <знак числа> <начало идентификатора> | <знак числа> <число>

        #endregion

    }
}

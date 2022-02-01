using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;

namespace JavaInPythonTranslator
{
    internal class EndPoints
    {
        #region Правило <тип данных> → boolean | byte | short | char | int | float | double 
        public static string dataTypeCheck(List<LexList> lexems)
        {
            if ((lexems[pos].type != "R1") ||
               (lexems[pos].type != "R2") ||
               (lexems[pos].type != "R3") ||
               (lexems[pos].type != "R4") ||
               (lexems[pos].type != "R5") ||
               (lexems[pos].type != "R6") ||
               (lexems[pos].type != "R7"))
            {
                return "Ошибка: \"Ожидался тип данных\"";
            }
            else
            {
                return "success";
            }
        }
        #endregion
    }
}

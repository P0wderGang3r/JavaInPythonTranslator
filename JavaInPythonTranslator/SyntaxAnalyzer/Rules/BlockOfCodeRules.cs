using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.NewVariableRules;
using static JavaInPythonTranslator.FunctionRules;
using static JavaInPythonTranslator.AssignmentRules;
using static JavaInPythonTranslator.IfLoopRules;

namespace JavaInPythonTranslator
{
    internal class BlockOfCodeRules
    {
        #region <блок кода> → <инструкция> <блок кода> | <инструкция>
        public static string blockOfCodeCheck(List<LexList> lexems)
        {
            string check = compare(lexems[pos].type, D5);
            if (String.Equals(check, successMessage))
            {
                pos--;
                return check;
            }

            check = instructionCheck(lexems);
            if (!String.Equals(check, successMessage))
            {
                return check;
            }

            return blockOfCodeCheck(lexems);
        }
        #endregion

        #region <инструкция> → <объявление переменной> | <вызов функции> | <присваивание> | <цикл> | <ветвление>
        static string instructionCheck(List<LexList> lexems)
        {
            string check;

            check = compare(lexems[pos].type, D5);
            if (String.Equals(check, successMessage))
            {
                pos--;
                return check;
            }

            check = variableDeclarationCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            check = callFunctionCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            pos++;

            check = assignmentCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            check = callFunctionCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            check = loopCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            check = ifElseCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            return "Ошибка";
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;

namespace JavaInPythonTranslator
{
    internal class NewVariableRules
    {
        #region <объявление переменной> → <тип данных переменной> <имя или инициализация>
        public static string variableDeclarationCheck(List<LexList> lexems)
        {
            string check;

            check = EndPoints.dataTypeCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = nameAndRealizationCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;

            return successMessage;
        }
        #endregion

        #region <имя или инициализация> → <начало идентификатора> | <начало идентификатора> = <значение>;
        static string nameAndRealizationCheck(List<LexList> lexems)
        {
            string check;

            check = EndPoints.IdentificatorCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = compare(lexems[pos].type, D3);
            if (String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, P1);
            if (!String.Equals(check, successMessage))
                return check;

            check = EndPoints.ValueCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = compare(lexems[pos].type, D3);
            if (!String.Equals(check, successMessage))
                return check;

            return successMessage;
        }
        #endregion
    }
}

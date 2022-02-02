using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.FunctionRules;

namespace JavaInPythonTranslator
{
    internal class ExpressionRules
    {
        public static string expressionCheck(List<LexList> lexems)
        {
            string check;

            check = EndPoints.ValueCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                pos++;
                return check;
            }

            int startPos = pos;
            check = arithmeticalCheck(lexems);
            if (String.Equals(check, successMessage))
                return check;

            pos = startPos;
            check = logicalCheck(lexems);
            if (String.Equals(check, successMessage))
                return check;

            return "Ошибка";
        }

        static string arithmeticalCheck(List<LexList> lexems)
        {
            string check;
            
            check = arithmeticalOperandCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = EndPoints.ArithmeticalOperatorCheck(lexems);
            if (!String.Equals(check, successMessage))
            {
                return successMessage;
            }
            pos++;

            return arithmeticalCheck(lexems);
        }

        static string arithmeticalOperandCheck(List<LexList> lexems)
        {
            string check;

            check = EndPoints.NumberValueCheck(lexems);
            if (String.Equals(check, successMessage))
                return successMessage;

            check = EndPoints.IdentificatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                check = callFunctionCheck(lexems);
                if (String.Equals(check, successMessage))
                    return successMessage;
                pos--;
            }

            check = unaryCheck(lexems);
            if (String.Equals(check, successMessage))
                return successMessage;

            check = EndPoints.IdentificatorCheck(lexems);
            if (String.Equals(check, successMessage))
                return successMessage;

            return "Ошибка: ожидался операнд";
        }

        static string unaryCheck(List<LexList> lexems)
        {
            string check;

            check = EndPoints.UnaryOperatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                pos++;
                check = EndPoints.IdentificatorCheck(lexems);
                if (String.Equals(check, successMessage))
                    return successMessage;
                pos--;
            }

            check = EndPoints.IdentificatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                pos++;
                check = EndPoints.UnaryOperatorCheck(lexems);
                if (String.Equals(check, successMessage))
                    return successMessage;
                pos--;
            }

            check = EndPoints.SignOperatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                pos++;
                check = EndPoints.IdentificatorCheck(lexems);
                if (String.Equals(check, successMessage))
                    return successMessage;

                check = EndPoints.NumberValueCheck(lexems);
                if (String.Equals(check, successMessage))
                    return successMessage;
                pos--;
            }

            return "Ошибка: ожидался унарный операнд";
        }

        static string logicalCheck(List<LexList> lexems)
        {

            return successMessage;
        }

        static string logicalOperandCheck(List<LexList> lexems)
        {

            return successMessage;
        }

        static string comparisonOperandCheck(List<LexList> lexems)
        {

            return successMessage;
        }
    }
}

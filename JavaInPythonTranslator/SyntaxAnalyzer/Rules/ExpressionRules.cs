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
        #region <выражение> → <арифметическое выражение> | <логическое выражение> | <значение> | <идентификатор>
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
        #endregion

        #region <арифметическое выражение> → <операнд> <арифметический оператор> <арифметическое выражение> | <операнд> 
        static string arithmeticalCheck(List<LexList> lexems)
        {
            string check;
            
            check = operandCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = EndPoints.ArithmeticalOperatorCheck(lexems);
            if (!String.Equals(check, successMessage))
            {
                return check;
            }
            pos++;

            return arithmeticalCheck(lexems);
        }
        #endregion

        #region <унарная арифметическая операция> → <идентификатор> <унарный арифметический оператор> | <унарный арифметический оператор> <идентификатор> | <знак числа> <идентификатор> | <знак числа> <число>
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
        #endregion

        #region <логическое выражение> → <логический операнд> | <логический операнд> <логический бинарный оператор> <логическое выражение> | !<логическое выражение>
        static string logicalCheck(List<LexList> lexems)
        {
            string check;

            if (String.Equals(lexems[pos].value, "!"))
            {
                pos++;
                logicalCheck(lexems);
            }

            check = logicalOperandCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = EndPoints.LogicalBinaryOperatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return check;
            }
            pos++;

            return logicalCheck(lexems);
        }
        #endregion

        #region <логический операнд> → <операнд> | <выражение сравнения>
        static string logicalOperandCheck(List<LexList> lexems)
        {
            string check;
            int startPos = pos;

            check = comparisonCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            pos = startPos;
            check = operandCheck(lexems);
            if (String.Equals(check, successMessage))
                return successMessage;

            return "Ошибка: ожидался логический операнд";
        }
        #endregion

        #region <выражение сравнения> → <операнд> <оператор сравнения> <операнд> 
        static string comparisonCheck(List<LexList> lexems)
        {
            string check;

            check = operandCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = EndPoints.ComparisonOperatorCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = operandCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            return successMessage;
        }
        #endregion

        #region <операнд> → <число> | <идентификатор> | <вызов функции> | <унарная арифметическая операция> | <символьное значение>
        static string operandCheck(List<LexList> lexems)
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
        #endregion
    }
}

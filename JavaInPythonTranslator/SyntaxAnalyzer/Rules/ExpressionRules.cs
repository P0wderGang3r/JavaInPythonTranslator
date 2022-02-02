using static JavaInPythonTranslator.FunctionRules;
using static JavaInPythonTranslator.SyntaxGlobals;

namespace JavaInPythonTranslator
{
    internal class ExpressionRules
    {
        #region <выражение> → <арифметическое выражение> | <логическое выражение> | <значение> | <идентификатор>
        public static string expressionCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;
            
            treeNodes.Clear();
            int startPos = pos;
            //Проверка начала арифметического выражения
            check = arithmeticalCheck(lexems, treeNodes);
            if (String.Equals(check, successMessage))
                return check;

            treeNodes.Clear();
            pos = startPos;
            //Проверка начала логического выражения
            check = logicalCheck(lexems, treeNodes);
            if (String.Equals(check, successMessage))
                return check;

            return successMessage;
        }
        #endregion

        #region <арифметическое выражение> → <операнд> <арифметический оператор> <арифметическое выражение> | <операнд> 
        static string arithmeticalCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;

            check = operandCheck(lexems, treeNodes);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = EndPoints.ArithmeticalOperatorCheck(lexems);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos], null));
            if (!String.Equals(check, successMessage))
            {
                return successMessage;
            }
            pos++;

            return arithmeticalCheck(lexems, treeNodes);
        }
        #endregion

        #region <унарная арифметическая операция> → <идентификатор> <унарный арифметический оператор> | <унарный арифметический оператор> <идентификатор> | <знак числа> <идентификатор> | <знак числа> <число>
        static string unaryCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;

            check = EndPoints.UnaryOperatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                pos++;
                check = EndPoints.IdentificatorCheck(lexems);
                if (String.Equals(check, successMessage))
                {
                    treeNodes.Add(new TreeNode(lexems[pos - 1], null));
                    treeNodes.Add(new TreeNode(lexems[pos], null));
                    return successMessage;
                }
                pos--;
            }

            check = EndPoints.IdentificatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                pos++;
                check = EndPoints.UnaryOperatorCheck(lexems);
                if (String.Equals(check, successMessage))
                {
                    treeNodes.Add(new TreeNode(lexems[pos - 1], null));
                    treeNodes.Add(new TreeNode(lexems[pos], null));
                    return successMessage;
                }
                pos--;
            }

            check = EndPoints.SignOperatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                pos++;
                check = EndPoints.IdentificatorCheck(lexems);
                if (String.Equals(check, successMessage))
                {
                    treeNodes.Add(new TreeNode(lexems[pos - 1], null));
                    treeNodes.Add(new TreeNode(lexems[pos], null));
                    return successMessage;
                }

                check = EndPoints.NumberValueCheck(lexems);
                if (String.Equals(check, successMessage))
                {
                    treeNodes.Add(new TreeNode(lexems[pos - 1], null));
                    treeNodes.Add(new TreeNode(lexems[pos], null));
                    return successMessage;
                }
                pos--;
            }

            return "Ошибка: ожидался унарный операнд";
        }
        #endregion

        #region <логическое выражение> → <логический операнд> | <логический операнд> <логический бинарный оператор> <логическое выражение> | !<логическое выражение>
        static string logicalCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;

            if (String.Equals(lexems[pos].value, "!"))
            {
                pos++;
                logicalCheck(lexems, treeNodes);
            }

            check = logicalOperandCheck(lexems, treeNodes);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = EndPoints.LogicalBinaryOperatorCheck(lexems);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos], null));
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }
            pos++;

            return logicalCheck(lexems, treeNodes);
        }
        #endregion

        #region <логический операнд> → <операнд> | <выражение сравнения>
        static string logicalOperandCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;
            int startPos = pos;

            check = comparisonCheck(lexems, treeNodes);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            pos = startPos;
            check = operandCheck(lexems, treeNodes);
            if (String.Equals(check, successMessage))
                return successMessage;

            return "Ошибка: ожидался логический операнд";
        }
        #endregion

        #region <выражение сравнения> → <операнд> <оператор сравнения> <операнд> 
        static string comparisonCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;

            check = operandCheck(lexems, treeNodes);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = EndPoints.ComparisonOperatorCheck(lexems);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos], null));
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = operandCheck(lexems, treeNodes);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            return successMessage;
        }
        #endregion

        #region <операнд> → <число> | <идентификатор> | <вызов функции> | <унарная арифметическая операция> | <символьное значение>
        static string operandCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;

            check = EndPoints.NumberValueCheck(lexems);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos], null));
            if (String.Equals(check, successMessage))
                return successMessage;

            check = EndPoints.IdentificatorCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                check = callFunctionCheck(lexems, treeNodes);
                if (String.Equals(check, successMessage))
                    return successMessage;
                treeNodes.RemoveAt(treeNodes.Count - 1);
                pos--;
            }

            check = unaryCheck(lexems, treeNodes);
            if (String.Equals(check, successMessage))
                return successMessage;

            check = EndPoints.IdentificatorCheck(lexems);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos], null));
            if (String.Equals(check, successMessage))
                return successMessage;

            return "Ошибка: ожидался операнд";
        }
        #endregion
    }
}

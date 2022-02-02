using static JavaInPythonTranslator.AssignmentRules;
using static JavaInPythonTranslator.FunctionRules;
using static JavaInPythonTranslator.IfLoopRules;
using static JavaInPythonTranslator.NewVariableRules;
using static JavaInPythonTranslator.SyntaxGlobals;

namespace JavaInPythonTranslator
{
    internal class BlockOfCodeRules
    {
        #region <блок кода> → <инструкция> <блок кода> | <инструкция>
        public static string blockOfCodeCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            //Проверка на } - если встретили, выходим
            string check = compare(lexems[pos].type, D5);
            if (String.Equals(check, successMessage))
            {
                pos--;
                return check;
            }

            //Проверка инструкции
            check = instructionCheck(lexems, treeNodes);
            if (!String.Equals(check, successMessage))
            {
                return check;
            }

            //Переход к анализу следующего блока кода
            List<TreeNode> treeNode1 = new();
            treeNodes.Add(new TreeNode(new LexList(NewTree, NewTree), treeNode1));
            return blockOfCodeCheck(lexems, treeNode1);
        }
        #endregion

        #region <инструкция> → <объявление переменной> | <вызов функции> | <присваивание> | <цикл> | <ветвление>
        static string instructionCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;

            //Проверка на } - если встретили, выходим
            check = compare(lexems[pos].type, D5);
            if (String.Equals(check, successMessage))
            {
                pos--;
                return check;
            }

            treeNodes.Clear();
            int startPos = pos;
            //Проверка на "объявление переменной"
            check = variableDeclarationCheck(lexems, treeNodes);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }


            treeNodes.Clear();
            pos = startPos;
            //Проверка на "вызов функции"
            check = callFunctionCheck(lexems, treeNodes);
            if (String.Equals(check, successMessage))
            {
                check = compare(lexems[pos].type, D3);
                if (!String.Equals(check, successMessage))
                    return check;

                return successMessage;
            }

            //Костыль
            return "Ошибка";

            treeNodes.Clear();
            pos = startPos;
            //Проверка на операцию присваивания
            check = assignmentCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            treeNodes.Clear();
            pos = startPos;
            //Проверка на вход в цикл
            check = loopCheck(lexems);
            if (String.Equals(check, successMessage))
            {
                return successMessage;
            }

            treeNodes.Clear();
            pos = startPos;
            //Проверка на вход в ветвление
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

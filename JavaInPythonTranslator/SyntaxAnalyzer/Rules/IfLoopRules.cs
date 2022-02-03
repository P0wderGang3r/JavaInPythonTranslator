using static JavaInPythonTranslator.SyntaxGlobals;

namespace JavaInPythonTranslator
{
    static internal class IfLoopRules
    {
        #region <цикл> → while (<логическое выражение>) {<тело цикла>} | do {<тело цикла>} while (<логическое выражение>); | for (<инструкция>; <логическое выражение>; <инструкция>) {<тело цикла>}
        public static string loopCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {

            return "success";
        }
        #endregion

        #region <тело цикла> → <блок кода> <оператор цикла> <блок кода> | <оператор цикла> <блок кода> | <блок кода> <оператор цикла> | <блок кода> | <оператор цикла>
        static string bodyLoopCheck(List<LexList> lexems)
        {

            return "success";
        }
        #endregion

        #region <ветвление> → if (<логическое выражение>) {<блок кода>} | if (<логическое выражение>) {<блок кода>} <иначе-ветвление>
        public static string ifCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;

            //Проверяем, что начинаем с if
            check = compare(lexems[pos].type, ifClass);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos - 1], null));
            if (!String.Equals(check, successMessage))
                return check;

            //Проверяем открывающую скобку (
            check = compare(lexems[pos].type, D6);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos - 1], null));
            if (!String.Equals(check, successMessage))
                return check;

            //Проверка на наличие логического условия
            List<TreeNode> treeNode1 = new List<TreeNode>();
            treeNodes.Add(new TreeNode(new LexList(NewTree, NewTree), treeNode1));
            check = ExpressionRules.logicalCheck(lexems, treeNode1);
            if (!String.Equals(check, successMessage))
                return check;

            //Проверяем закрывающую скобку )
            check = compare(lexems[pos].type, D7);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos - 1], null));
            if (!String.Equals(check, successMessage))
                return check;

            //Проверяем открывающую скобку {
            check = compare(lexems[pos].type, D4);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos - 1], null));
            if (!String.Equals(check, successMessage))
                return check;

            //Проверка на наличие блока кода
            List<TreeNode> treeNode2 = new List<TreeNode>();
            treeNodes.Add(new TreeNode(new LexList(NewTree, NewTree), treeNode2));
            check = BlockOfCodeRules.blockOfCodeCheck(lexems, treeNode2);
            if (!String.Equals(check, successMessage))
                return check;

            //Проверяем закрывающую скобку }
            check = compare(lexems[pos].type, D5);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos - 1], null));
            if (!String.Equals(check, successMessage))
                return check;

            int startPos = pos;
            List<TreeNode> treeNode3 = new List<TreeNode>();
            treeNodes.Add(new TreeNode(new LexList(NewTree, NewTree), treeNode3));
            check = elseCheck(lexems, treeNode3);
            if (!String.Equals(check, successMessage))
            {
                pos = startPos;
                treeNodes.RemoveAt(treeNodes.Count - 1);
                return successMessage;
            }

            return successMessage;

            //Проверяем else-часть ветвления

        }
        #endregion

        #region <иначе-ветвление> → else {<блок кода>} | if (<логическое выражение>) {<блок кода>} else <ветвление>
        public static string elseCheck(List<LexList> lexems, List<TreeNode> treeNodes)
        {
            string check;

            //Проверяем наличие else
            check = compare(lexems[pos].type, elseClass);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos - 1], null));
            if (!String.Equals(check, successMessage))
                return check;

            //Проверяем открывающую скобку {
            check = compare(lexems[pos].type, D4);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos - 1], null));
            if (!String.Equals(check, successMessage))
                return check;

            //Проверка на наличие блока кода
            List<TreeNode> treeNode1 = new List<TreeNode>();
            treeNodes.Add(new TreeNode(new LexList(NewTree, NewTree), treeNode1));
            check = BlockOfCodeRules.blockOfCodeCheck(lexems, treeNode1);
            if (!String.Equals(check, successMessage))
                return check;

            //Проверяем закрывающую скобку }
            check = compare(lexems[pos].type, D5);
            if (String.Equals(check, successMessage))
                treeNodes.Add(new TreeNode(lexems[pos - 1], null));
            if (!String.Equals(check, successMessage))
                return check;

            return successMessage;
        }
        #endregion
    }
}

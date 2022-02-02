namespace JavaInPythonTranslator
{
    /*
         * ----------------------------------------------------------------------------------------
         * ----------------------------------Генератор кода----------------------------------------
         * 
         *      Генератор кода переводит синтаксически корректную программу на Java в программу на 
         * Python. Дерево, построенное синтаксическим анализатором используется, чтобы перевести
         * входную программу на Java в программу на Python при помощи таблицы соотвествия.
         * 
         * ----------------------------------------------------------------------------------------
         * 
         * Есть нода дерева TreeNode в Globals
         * И есть корень дерева List<TreeNode>
         * Каждый из этих листов либо имеет NULL потомок и содержит лексему, либо имеет потомок List<TreeNode> и не имеет лексему
         * Задача прохода - идти по каждому из списков слева направо, и если находишь потомка, то спускаться вниз, 
         * а потом возвращаться наверх, когда кончатся все элементы
         *
         * ----------------------------------------------------------------------------------------
         * 
         * Допустим,
         * Для public static void main() {
         * int a = 5 + 5 * 5;
         * int b = 6 + 6 * 6;
         * System.out.println(a + b);
         * b = 0;
         * }
         * Дерево будет выглядеть как (здесь :N - номер потомка, на самом деле они непосредственно связаны)
         * :0->public static void main() { :1 }
         * :1->int a = 5 + 5 * 5; :2
         * :2->int b = 6 + 6 * 6;
         * :3->System.out.println( :4 ); :5
         * :4->a + b
         * :5->b = 0 
         * 
         * 
         * */

    internal static class CodeGenerator
    {
        // Главная функция генератора кода
        public static void Generate1(StreamWriter file, List<TreeNode> treeNodes) {

            foreach (TreeNode treeNode in treeNodes)
            {
                if (treeNode.nextLevelNodes != null)
                {
                    file.WriteLine();
                    Generate1(file, treeNode.nextLevelNodes);
                }
                file.Write(String.Equals(treeNode.lexem.value, "NewTree") ? "" : Translate(treeNode.lexem) + " ");
            }
        }

        // Тест, потом удалю
        public static void Generate2(StreamWriter file, List<TreeNode> treeNodes)
        {
            foreach (TreeNode treeNode in treeNodes)
            {
                file.Write(String.Equals(treeNode.lexem.value, "NewTree")? "": treeNode.lexem.value + " ");
            }

            foreach (TreeNode treeNode in treeNodes)
            {
                if (treeNode.nextLevelNodes != null)
                {
                    file.WriteLine();
                    Generate2(file, treeNode.nextLevelNodes);
                }
            }
        }

        // Трансляция Java в Python
        public static string Translate(LexList lexem) {
            
            return lexem.value;

        }
    }
}

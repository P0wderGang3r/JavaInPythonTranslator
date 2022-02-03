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
        // стек для работы с "(" и ")"
        // если прямо сейчас обрабатываются параметры функции, то в стек спушится "("
        // если скобка ")" закрылась (закончили обрабатываться параметры), то из стека удаляется "("
        static Stack<string> inParametersStack = new Stack<string>(); 

        // функция проверяет, есть ли лексема с "(" и ")", то есть проверка на то, что
        // текущие лексемы принадлежат параметрам функции/команды.
        static void inParametersCheck(TreeNode treeNode) { 

            switch (treeNode.lexem.type) {

                // "("
                case "D6":
                    inParametersStack.Push("(");
                    break;

                // ")"
                case "D7":
                    inParametersStack.Pop();
                    break;
            }
        }

        // стек для работы с "{" и "}"
        static Stack<string> inBodyStack = new Stack<string>();

        // Отступ строки (для функций)
        static int intOffset = 0;

        static string stringOffset() {
            string offset = "";
            for (int i = 1; i <= intOffset; i++) offset += "\t";
            return offset;
        }

        // Главная функция генератора кода
        public static void Generate1(StreamWriter file, List<TreeNode> treeNodes) {
            
            for (int i = 0; i < treeNodes.Count; i++)
            {
                inParametersCheck(treeNodes[i]);
                if (treeNodes[i].nextLevelNodes != null)
                {
                    // новая строка
                    // если сейчас в лексемах - параметры функции, то отмена
                    if (!inParametersStack.Contains("("))
                    {
                        file.WriteLine();
                        file.Write(stringOffset());
                    }

                    Generate1(file, treeNodes[i].nextLevelNodes);
                }
                file.Write(String.Equals(treeNodes[i].lexem.value, "NewTree") ? "" : Translate(treeNodes, ref i, treeNodes.Count));
            }
        }

        // Трансляция Java в Python
        // В параметрах - список нод и индекс обрабатываемой ноды, т.к. возможно потребуется доступ к содержимому других нод
        // Поэтому работать с объектом treeNode не вариант
        public static string Translate(List<TreeNode> treeNodes, ref int i, int size) {
           
            switch (treeNodes[i].lexem.type)
            {
                // import
                case "K1":
                    if ((i + 1 < size) && (treeNodes[i + 1].lexem.value == "java.lang.Math"))
                    {
                        //если import math, то отбросить
                        i++;
                        return "";
                    }
                    return treeNodes[i].lexem.value + " ";

                // class
                case "K14":
                    if ((i + 1 < size) && (treeNodes[i + 1].lexem.type == "K6"))
                    {
                        // если это class Main, то отбросить
                        // в стек "{" не добавляем
                        i++;
                        return "";
                    }
                    
                    // если класс транслируется, то запушить в стек "{"
                    inBodyStack.Push("{");
                    return treeNodes[i].lexem.value + " ";

                // ; 
                case "D3":
                    // отбрасываем
                    return "";

                // {
                case "D4":
                    if (!inBodyStack.Contains("{"))
                        // если скобка "{" принадлежит функции ,не нужной для трансляции, то отбрасывается
                        return "";

                    // если скобка "{" указывает на тело транслируемой функции, то ":"
                    return ":";

                // }
                case "D5":
                    if (!inBodyStack.Contains("{"))
                        // если скобка "}" принадлежит функции ,не нужной для трансляции, то отбрасывается
                        return "";

                    // если скобка "}" указывает на тело транслируемой функции, то убрать скобку из стека
                    intOffset--;
                    inBodyStack.Pop();
                    return "";
                

                default:
                    return treeNodes[i].lexem.value + " ";
            }
            

        }
    }
}

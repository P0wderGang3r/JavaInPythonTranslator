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

        // множество операторов, для которых в правой части могут быть ссылки на поддеревья синт. дерева
        // обычно для новых поддеревьев пишется новая строка в выходном файле, но здесь это не требуется
        // поэтому выделили след. мн-во исключений для перевода строки
        static string[] operators = { "P1", "P2", "P3", "P4", "P5", "P6", "S1", "S2", "S3", "S4",
         "S5", "S6", "L1", "L2", "U1", "U2","B1", "B2", "B3", "B4", "B5"};

        // Главная функция генератора кода
        public static void Generate(StreamWriter file, List<TreeNode> treeNodes) {
            
            for (int i = 0; i < treeNodes.Count; i++)
            {
                inParametersCheck(treeNodes[i]);
                if (treeNodes[i].nextLevelNodes != null)
                {
                    // новая строка
                    // если сейчас в лексемах - параметры функции, то отмена
                    // если лексемы принадлежат множеству исключений operators. то отмена
                    if (!inParametersStack.Contains("(") && !(operators.Contains(treeNodes[i-1].lexem.type)))
                    {
                        file.WriteLine();
                        file.Write(stringOffset());
                    }

                    Generate(file, treeNodes[i].nextLevelNodes);
                }
                file.Write(String.Equals(treeNodes[i].lexem.value, "NewTree") ? "" : Translate(treeNodes, ref i, treeNodes.Count));
            }
        }

        // добавление в файл 'if __name__=="__main__": main()'
        public static void addMainFunctionCall(StreamWriter file) {
            file.WriteLine();
            file.WriteLine("if __name__ == '__main__':");
            file.WriteLine("\tmain()");
        }

        // Трансляция Java в Python
        // В параметрах - список нод и индекс обрабатываемой ноды, т.к. возможно потребуется доступ к содержимому других нод
        // Поэтому работать с объектом treeNode не вариант
        static string Translate(List<TreeNode> treeNodes, ref int i, int size) {
           
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

                // public
                case "K2":
                    // проверка на главную функцию public static void main(String [] args)
                    if ((i + 3 < size) && (treeNodes[i + 3].lexem.type == "K7"))
                    {
                        intOffset++;
                        inBodyStack.Push("{");
                        i += 9;
                        return "def main()";
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

                // boolean
                case "T1":
                    return typeTranslate(treeNodes, ref i, size, "bool");

                // byte
                case "T2":
                    return typeTranslate(treeNodes, ref i, size, "bytes");

                // short
                case "T3":
                    return typeTranslate(treeNodes, ref i, size, "int");

                // int
                case "T4":
                    return typeTranslate(treeNodes, ref i, size, "int");

                // float
                case "T5":
                    return typeTranslate(treeNodes, ref i, size, "float");

                // double
                case "T6":
                    return typeTranslate(treeNodes, ref i, size, "double");

                // char
                case "T7":
                    return typeTranslate(treeNodes, ref i, size, "str");

                // string
                case "T8":
                    return typeTranslate(treeNodes, ref i, size, "str");

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

                // (
                case "D6":
                    if (!inParametersStack.Contains("("))
                        // если скобка "(" принадлежит функции ,не нужной для трансляции, то отбрасывается
                        return "";
                    return treeNodes[i].lexem.value;

                // (
                case "D7":
                    if (!inParametersStack.Contains("("))
                        // если скобка ")" принадлежит функции ,не нужной для трансляции, то отбрасывается
                        return "";
                    inParametersStack.Pop();
                    return treeNodes[i].lexem.value;

                // ++
                case "U1":
                    //В Python нет "++", поэтому заменим на скобку - ++a -> (a + 1)
                    i++;
                    return "(" + treeNodes[i].lexem.value + " + 1)";
                // --
                case "U2":
                    i++;
                    return "(" + treeNodes[i].lexem.value + " - 1)";

                // ID
                case "ID":
                    // проверка на функцию вывода в консоль
                    if ((String.Equals(treeNodes[i].lexem.value, "System.out.println")) ||
                     (String.Equals(treeNodes[i].lexem.value, "System.out.print")))
                    {
                        inParametersStack.Push("(");
                        return "print";
                    }
                    // проверка на операторы "<ID> ++" и "<ID> --"
                    if (i + 1 < size)
                    {
                        if (treeNodes[i + 1].lexem.type == "U1")
                        {
                            i++;
                            return "(" + treeNodes[i-1].lexem.value + " + 1)";
                        }
                        if (treeNodes[i + 1].lexem.type == "U2")
                        {
                            i++;
                            return "(" + treeNodes[i - 1].lexem.value + " - 1)";
                        }
                    }
                    return treeNodes[i].lexem.value + " ";

                default:
                    return treeNodes[i].lexem.value + " ";
            }
        }

        // Трансляция типов
        static string typeTranslate(List<TreeNode> treeNodes, ref int i, int size, String type) {
            // проверка на функцию
            // например, boolean func (...){...}
            if ((i + 2 < size) && (treeNodes[i + 2].lexem.type == "D6")) // "("
            {
                intOffset++;
                inBodyStack.Push("{");
                
                i += 2;
                return "def " + treeNodes[i + 1].lexem.value;
            }
            // проверка на параметры func(boolean a, ...)
            if (inParametersStack.Contains("("))
                return type + " ";
            // объявление переменной boolean a = 5
            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.FunctionRules;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.Globals;


namespace JavaInPythonTranslator
{
    static class SyntaxAnalyzer
    {
        #region <программа> → <подключение пакетов> | <объявление класса> 
        static public string startRule(List<LexList> lexems)
        {
            if (String.Equals(lexems[pos].type, importClass))
            {
                return importCheck(lexems);
            }
            else
            {
                return classCheck(lexems);
            }
        }
        #endregion

        #region <подключение пакетов> → K1 <идентификатор> <подключение пакетов> | <объявление класса> 
        static string importCheck(List<LexList> lexems)
        {
            if (lexems[pos].type == importClass)
            { 
                pos++;
            }
            else
                return "Ошибка";

            if (lexems[pos].type == identificator)
            {
                pos++;
            }
            else
                return "Ошибка";

            if (lexems[pos].type == D3)
            {
                pos++;
            }
            else
                return "Ошибка";

            if (String.Equals(lexems[pos].type, importClass))
            {
                return importCheck(lexems);
            }
            else
            {
                return classCheck(lexems);
            }
        }
        #endregion

        #region Правило <объявление класса> → class Main {<главная функция> <тело класса>} | class Main {<главная функция>}
        static string classCheck(List<LexList> lexems)
        {
            string check;

            check = compare(lexems[pos].type, classClass);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, classMainClass);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, D4);
            if (!String.Equals(check, successMessage))
                return check;

            check = voidmainCheck(lexems);
            if (!String.Equals(check, successMessage))
            {
                return "Ошибка: \"Ожидалась главная функция\"";
            }

            check = bodyclassCheck(lexems);
             if (!String.Equals(check, "NULL") && !String.Equals(check, successMessage))
            {
                return "Ошибка: \"Ожидалось тело класса\"";
            }

            check = compare(lexems[pos].type, D5);
            if (!String.Equals(check, successMessage))
                return check;

            return successMessage;
        }
        #endregion

        #region <главная функция> → public static void main (String[] args) { <блок кода> }
        static string voidmainCheck(List<LexList> lexems)
        {
            string check;

            check = compare(lexems[pos].type, publicClass);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, staticClass);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, voidClass);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, funcMainClass);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, D6);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, stringClass);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, D8);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, D6);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, D9);
            if (!String.Equals(check, successMessage))
                return check;

            check = lexems[pos].value;
            if (String.Equals(check, "args"))
            {
                pos++;
            }
            else
                return "Ошибка: \"Ожидалось \"args\"\"";

            check = compare(lexems[pos].type, D7);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, D4);
            if (!String.Equals(check, successMessage))
                return check;

            check = blockOfCodeCheck(lexems);
            if (!String.Equals(check, successMessage) && !String.Equals(check, "NULL"))
            {
                return "Ошибка: \"Ожидалось тело класса\"";
            }

            check = compare(lexems[pos].type, D5);
            if (!String.Equals(check, successMessage))
                return check;

            return successMessage;
        }
        #endregion

        #region Правило <тело класса> → <объявление переменной> <тело класса> | <объявление переменной> | <объявление функции> <тело класса> | <объявление функции> | <объявление константы> <тело класса> | <объявление константы> 
        static string bodyclassCheck(List<LexList> lexems)
        {
            return successMessage;
        }
        #endregion

    }
}

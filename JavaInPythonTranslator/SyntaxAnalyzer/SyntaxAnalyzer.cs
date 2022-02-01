using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.EndPoints;


namespace JavaInPythonTranslator
{
    static class SyntaxAnalyzer
    {

        #region Правило <программа> → <подключение пакетов> | <объявление класса> 
        static public string startRule(List<LexList> lexems)
        {
            if (lexems[pos].type == "K9")
            {
                pos++;
                return importCheck(lexems);
            }
            else if (lexems[pos].type == "R1")
            {
                pos++;
                return classCheck(lexems);
            }
            else
            {
                pos++;
                if ((lexems[pos].type != "R1"))
                {
                    return "Ошибка: \"Ожидалось ключевое слово \"class\"\"";
                }
                else
                {
                    pos++;
                    return classCheck(lexems);
                }
            }
        }
        #endregion

        #region Правило <подключение пакетов> → <подключение пакета> <подключение пакетов> | <объявление класса>
        static string importCheck(List<LexList> lexems)
        {
            //!!!КОСТЫЛЬ!!!
            pos++;
            pos++;
            if ((lexems[pos].type == "K10") || (lexems[pos].type == "K11") || (lexems[pos].type == "K12") || (lexems[pos].type == "K13"))
            {
                pos++;
                if (lexems[pos].type != "R1")
                {
                    return "Ошибка: \"Ожидалось ключевое слово \"class\"\"";
                }
                else
                {
                    pos++;
                    return classCheck(lexems);
                }
            }
            else if (lexems[pos].type == "R1")
            {
                pos++;
                return classCheck(lexems);
            }
            else if (lexems[pos].type == "K9")
            {
                pos++;
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
            if(lexems[pos].type != "K14")
            {
                return "Ошибка: \"Ожидалось \"Main\"\"";
            }
            else
            {
                pos++;
                if(lexems[pos].type != "D4")
                {
                    return "Ошибка: \"Ожидалось \'{\'\"";
                }
                else
                {
                    pos++;
                    int i = pos;
                    if (voidmainCheck(lexems) != "success")
                    {
                        pos = i;
                        return voidmainCheck(lexems);
                    }
                    if (bodyclassCheck(lexems) != "success")
                    {
                        pos = i;
                        return bodyclassCheck(lexems);
                    }
                    if (lexems[pos].type != "D5")
                    {
                        return "Ошибка: \"Ожидалось \'}\'\"";
                    }
                    else
                    {
                        return "success";
                    }
                }
            }
        }
        #endregion

        #region Правило <тело класса> → <объявление переменной> <тело класса> | <объявление переменной> | <объявление функции> <тело класса> | <объявление функции> | <объявление константы> <тело класса> | <объявление константы> 
        static string bodyclassCheck(List<LexList> lexems)
        {
            //НЕ СДЕЛАНО
            return "success";
        }
        #endregion

        #region <главная функция> → public static void main (String[] args) { <блок кода> }
        static string voidmainCheck(List<LexList> lexems)
        {
            if (lexems[pos].type != "K10")
            {
                return "Ошибка: \"Ожидалось \"public\"\"";
            }
            else
            {
                pos++;
                if (lexems[pos].type != "K13")
                {
                    return "Ошибка: \"Ожидалось \"static\"\"";
                }
                else
                {
                    pos++;
                    if (lexems[pos].type != "R9")
                    {
                        return "Ошибка: \"Ожидалось \"void\"\"";
                    }
                    else
                    {
                        pos++;
                        if (lexems[pos].type != "K15")
                        {
                            return "Ошибка: \"Ожидалось \"main\"\"";
                        }
                        else
                        {
                            pos++;
                            if (lexems[pos].type != "D6")
                            {
                                return "Ошибка: \"Ожидалось \'(\'\"";
                            }
                            else
                            {
                                pos++;
                                if (lexems[pos].type != "R10")
                                {
                                    return "Ошибка: \"Ожидалось \"String\"\"";
                                }
                                else
                                {
                                    pos++;
                                    if (lexems[pos].type != "D8")
                                    {
                                        return "Ошибка: \"Ожидалось \'[\'\"";
                                    }
                                    else
                                    {
                                        pos++;
                                        if (lexems[pos].type != "D9")
                                        {
                                            return "Ошибка: \"Ожидалось \']\'\"";
                                        }
                                        else
                                        {
                                            pos++;
                                            if (lexems[pos].value != "args")
                                            {
                                                return "Ошибка: \"Ожидалось \"args\"\"";
                                            }
                                            else
                                            {
                                                pos++;
                                                if (lexems[pos].type != "D7")
                                                {
                                                    return "Ошибка: \"Ожидалось \')\'\"";
                                                }
                                                else
                                                {
                                                    pos++;
                                                    if (lexems[pos].type != "D4")
                                                    {
                                                        return "Ошибка: \"Ожидалось \'{\'\"";
                                                    }
                                                    else
                                                    {
                                                        pos++;
                                                        int i = pos;
                                                        if (mainbodyCheck(lexems) != "success")
                                                        {
                                                            pos = i;
                                                            return mainbodyCheck(lexems);
                                                        }
                                                        if (lexems[pos].type != "D5")
                                                        {
                                                            return "Ошибка: \"Ожидалось \'}\'\"";
                                                        }
                                                        else
                                                        {
                                                            return "success";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Правило <блок кода> → <инструкция> <блок кода> | <инструкция>

        static string mainbodyCheck(List<LexList> lexems)
        {

            int i = pos;
            /*
            if(instructionCheck(lexems) != "success")
            {
                pos = i;
                return instructionCheck(lexems);
            }
            */
            pos++;
            if (lexems[pos].type != "D5")
            {
                return mainbodyCheck(lexems);
            }
            return "success";
        }
        #endregion

        #region <инструкция> → <объявление переменной> | <вызов функции> | <присваивание> | <цикл> | <ветвление>

        #endregion

    }
}

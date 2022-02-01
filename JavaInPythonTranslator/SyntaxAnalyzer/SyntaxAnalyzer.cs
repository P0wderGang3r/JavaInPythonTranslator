using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JavaInPythonTranslator
{
    class SyntaxAnalyzer
    {
        //Теперь вполне себе нормально передаётся список лексем
        public List<LexList> lexems = new List<LexList>();


        //Определение для текущей позиции в списке    
        public int pos = 0;

        #region Правило <программа> → package <имя текущего пакета> <подключение пакетов> | package <имя текущего пакета> <объявление класса> 
        public string startRule(List<LexList> lexems)
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
        #region Правило <подключение пакетов> → <подключение пакета>
        string importCheck(List<LexList> lexems)
        {
            //!!!КОСТЫЛЬ!!!
            pos++; pos++;
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
        #region Правило package -> <имя текущего пакета>
        string packageCheck(List<LexList> lexems)
        {
            if (lexems[pos].type != "S2")
            {
                return "Ошибка: \"Ожидалось название пакета\"";
            }
            else
            {
                pos++;
                if (lexems[pos].type != "D3")
                {
                    return "Ошибка: \"Ожидалось \';\'\"";
                }
                else
                {
                    pos++;
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
                    else if ((lexems[pos].type == "K10") || (lexems[pos].type == "K11") || (lexems[pos].type == "K12") || (lexems[pos].type == "K13"))
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
                    else
                    {
                        return "Ошибка: \"Ожидалось объявление класса или подключение библиотеки\"";
                    }
                }
            }
        }
        #endregion
        #region Правило <объявление класса> → class Main {<главная функция> <тело класса>} | class Main {<главная функция>}
        string classCheck(List<LexList> lexems)
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
        #region <главная функция> → public static void main (String[] args) { <блок кода> }
        string voidmainCheck(List<LexList> lexems)
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
        #region Правило <блок кода> → <инструкция> <блок кода> | <инструкция>;

        string mainbodyCheck(List<LexList> lexems)
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
        #region Правила объявления переменных
        #region Правило <тип данных> → boolean | byte | short | char | int | float | double 
        string dataTypeCheck(List<LexList> lexems)
        {
            if ((lexems[pos].type != "R1") ||
               (lexems[pos].type != "R2") ||
               (lexems[pos].type != "R3") ||
               (lexems[pos].type != "R4") ||
               (lexems[pos].type != "R5") ||
               (lexems[pos].type != "R6") ||
               (lexems[pos].type != "R7"))
            {
                return "Ошибка: \"Ожидался тип данных\"";
            }
            else
            {
                return "success";
            }
        }
        #endregion
        #region Правило <тело класса> → <объявление переменной> <тело класса> | <объявление переменной> | <объявление функции> <тело класса> | <объявление функции> | <объявление константы> <тело класса> | <объявление константы> 
        string bodyclassCheck(List<LexList> lexems)
        {
            return "success";
        }
        #endregion
        #region Правило <объявление переменной> → <тип данных переменной> <имя или инициализация>
        string variableDeclarationCheck(List<LexList> lexems)
        {
            int i = pos;
            if (dataTypeCheck(lexems) != "success")
            {
                pos = i;
                return dataTypeCheck(lexems);
            }
            else
            {
                pos++;
                i = pos;
                if (nameAndRealizationCheck(lexems) != "success")
                {
                    pos = i;
                    return nameAndRealizationCheck(lexems);
                }
                else
                {
                    return "success";
                }
            }
        }
        #endregion
        #region Правило <объявление константы> → const <тип данных переменной> <следующая константа> 
        string constDeclarationCheck(List<LexList> lexems)
        {
            if (lexems[pos].type != "K2")
            {
                return "Ошибка: \"Ожидалось \"const\"\"";
            }
            else
            {
                pos++;
                int i = pos;
                if (dataTypeCheck(lexems) != "success")
                {
                    pos = i;
                    return dataTypeCheck(lexems);
                }
                else 
                {
                    pos++;
                    return nextConstCheck(lexems);
                }
            }
        }
        #endregion
        #region Правило <следующая константа> → <имя переменной> = <значение>, <следующая константа> | <имя переменной> = <значение>;
        string nextConstCheck(List<LexList> lexems)
        {
            if(lexems[pos].type != "I3")
            {
                return "Ошибка: \"Ожидалось имя переменной\"";
            }
            else
            {
                pos++;
                if (lexems[pos].type != "O23")
                {
                    return "Ошибка: \"Ожидалось \'=\'\"";
                }
                else
                {
                    pos++;
                    if (lexems[pos].type != "NN")
                    {
                        return "Ошибка: \"Ожидалось значение\"";
                    }
                    else
                    {
                        pos++; 
                        if ((lexems[pos].type != "D2") || (lexems[pos].type) != "D3")
                        {
                            return "Ошибка \"Ожидалось \',\' или \';\'\"";
                        }
                        else if (lexems[pos].type == "D2")
                        {
                            pos++; 
                            return nextConstCheck(lexems);
                        }
                        else
                        {
                            return "success";
                        }
                    }
                }
            }
        }
        #endregion
        #region Правило <объявление функции> → <тип данных функции> <имя функции> (<параметры функции>) {<тело функции>}
        string functionDeclarationCheck(List<LexList> lexems)
        {
            int i = pos;
            if ((dataTypeCheck(lexems) != "success") || (lexems[pos].type != "R9"))
            {
                pos = i;
                return dataTypeCheck(lexems);
            }
            else
            {
                pos++;
                if (lexems[pos].type != "I3")
                {
                    return "Ошибка: \"Ожидалось имя функции\"";
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
                        i = pos;
                        if (functionParamsCheck(lexems) != "success")
                        {
                            pos = i;
                            return functionParamsCheck(lexems);
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
                                    i = pos;
                                    if (functionBodyCheck(lexems) != "success")
                                    {
                                        pos = i;
                                        return functionBodyCheck(lexems);
                                    }
                                    else
                                    {
                                        pos++;
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
        #endregion
        #region Правило <параметры функции> → <тип данных переменной> <идентификатор> | <тип данных переменной> <идентификатор>, <параметры функции> 
        string functionParamsCheck(List<LexList> lexems)
        {
            int i = pos;
            if(dataTypeCheck(lexems) != "success")
            {
                pos = i;
                return dataTypeCheck(lexems);
            }
            else
            {
                pos++;
                if (lexems[pos].type != "I3")
                {
                    return "Ошибка: \"Ожидалось имя переменной\"";
                }
                else
                {
                    pos++;
                    if (lexems[pos].type == "D7")
                    {
                        return "success";
                    }
                    else
                    {
                        return functionParamsCheck(lexems);
                    }
                }
            }
        }
        #endregion
        #region Правило <тело функции> → <блок кода> <возврат значения> | <блок кода>
        string functionBodyCheck(List<LexList> lexems)
        {
            int i = pos;
            if (mainbodyCheck(lexems) != "success")
            {
                pos = i;
                return mainbodyCheck(lexems);
            }
            else
            {
                pos++;
                if (lexems[pos].value == "return")
                {
                    return returnCheck(lexems);
                }
                else
                {
                    pos--;
                    return "success";
                }

            }
        }
        #endregion
        #region Правило <возврат значения> → return <выражение>;| return <имя переменной>; | return <имя константы>;
        string returnCheck(List<LexList> lexems)
        {
            if ((lexems[pos].type != "I3") || (expressionCheck(lexems) != "success"))
            {
                return "Ошибка: \"Ожидалось возвращаемое значение или выражение\"";
            }
            else
            {
                pos++;
                if (lexems[pos].type != "D3")
                {
                    return "Ошибка: \"Ожидалось \';\'\"";
                }
                else
                {
                    return "success";
                }
            }
        }
        #endregion
        #region Правило <имя или инициализация> → <имя переменной>, <имя или инициализация> | <имя переменной> = <значение>, <имя или инициализация> | <имя переменной>; | <имя переменной> = <значение>;

        string nameAndRealizationCheck(List<LexList> lexems)
        {
            if (lexems[pos].type != "I3")
            {
                return "Ошибка: \"Ожидалось имя переменной\"";
            }
            else
            {
                pos++;
                if ((lexems[pos].type != "D2") || (lexems[pos].type != "O23") || (lexems[pos].type != "D3"))
                {
                    return "Ошибка: \"Ожидалось\',\' или \'=\' или \';\'\"";
                }
                else if (lexems[pos].type == "D2")
                {
                    pos++;
                    return nameAndRealizationCheck(lexems);
                }
                else if (lexems[pos].type == "O23")
                {
                    pos++;
                    if (lexems[pos].type != "NN")
                    {
                        return "Ошибка: \"Ожидалось значение\"";
                    }
                    else
                    {
                        pos++;
                        if ((lexems[pos].type != "D2") || (lexems[pos].type) != "D3")
                        {
                            return "Ошибка: \"Ожидалось \',\' или \';\'\"";
                        }
                        else if (lexems[pos].type == "D2")
                        {
                            pos++;
                            return nameAndRealizationCheck(lexems);
                        }
                        else
                        {
                            return "success";
                        }
                    }
                }
                else
                {
                    return "success";
                }

            }
        }
        #endregion
        #endregion

        #region Правила выражений
        #region Правило <выражение> → <арифметическое выражение> | <логическое выражение> | <унарная арифметическая операция> | <значение> | <побитовое выражение>
        string expressionCheck(List<LexList> lexems)
        {
            return "success";
        }
        #endregion
        #endregion

        }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInPythonTranslator
{
    class SyntaxAnalyzer
    {
        //Необходимо передать лист структур данных "Лексема" Пока что так :(
        public List<lexem> lexems = new List<lexem>();

        //Определение для текущей позиции в списке    
        public int pos = 0;

        #region Правило <программа> → package <имя текущего пакета> <подключение пакетов> | package <имя текущего пакета> <объявление класса> 
        public string startRule(List<lexem> lexems)
        {
            if ((lexems[pos].value != "package") && (lexems[pos].id != "K9") && (lexems[pos].id != "R1") && (lexems[pos].id != "K10") && (lexems[pos].id != "K11") && (lexems[pos].id != "K12") && (lexems[pos].id != "K13"))
            {
                return "Ошибка: \"Не встречено ключевых слов начала программы\"";
            }
            else if (lexems[pos].value == "package")
            {
                pos++;
                return packageCheck(lexems);
            }
            else if (lexems[pos].id == "K9")
            {
                pos++;
                return importCheck(lexems);
            }
            else if (lexems[pos].id == "R1")
            {
                pos++;
                return classCheck(lexems);
            }
            else
            {
                pos++;
                if ((lexems[pos].id != "R1"))
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
        string importCheck(List<lexem> lexems)
        {
            if (lexems[pos].id != "S2")
            {
                return "Ошибка: \"Ожидалось название пакета\"";
            }
            else
            {
                pos++;
                if (lexems[pos].id != "D3")
                {
                    return "Ошибка: \"Ожидалось \';\'\"";
                }
                else 
                {
                    pos++;
                    if ((lexems[pos].id == "K10") || (lexems[pos].id == "K11") || (lexems[pos].id == "K12") || (lexems[pos].id == "K13"))
                    {
                        pos++;
                        if (lexems[pos].id != "R1")
                        {
                            return "Ошибка: \"Ожидалось ключевое слово \"class\"\"";
                        }
                        else
                        {
                            pos++;
                            return classCheck(lexems);
                        }
                    }
                    else if (lexems[pos].id == "R1")
                    {
                        pos++;
                        return classCheck(lexems);
                    }
                    else if (lexems[pos].id == "K9")
                    {
                        pos++;
                        return importCheck(lexems);    
                    }
                    else
                    {
                        return classCheck(lexems);
                    }
                }
            }
        }
        #endregion
        #region Правило package -> <имя текущего пакета>
        string packageCheck(List<lexem> lexems)
        {
            if (lexems[pos].id != "S2")
            {
                return "Ошибка: \"Ожидалось название пакета\"";
            }
            else
            {
                pos++;
                if (lexems[pos].id != "D3")
                {
                    return "Ошибка: \"Ожидалось \';\'\"";
                }
                else
                {
                    pos++;
                    if (lexems[pos].id == "K9")
                    {
                        pos++;
                        return importCheck(lexems);
                    }
                    else if (lexems[pos].id == "R1")
                    {
                        pos++;
                        return classCheck(lexems);
                    }
                    else if ((lexems[pos].id == "K10") || (lexems[pos].id == "K11") || (lexems[pos].id == "K12") || (lexems[pos].id == "K13"))
                    {
                        pos++;
                        if (lexems[pos].id != "R1")
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
        string classCheck(List<lexem> lexems)
        {
            if(lexems[pos].id != "K14")
            {
                return "Ошибка: \"Ожидалось \"Main\"\"";
            }
            else
            {
                pos++;
                if(lexems[pos].id != "D4")
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
                    pos++;
                    if (lexems[pos].id != "D5")
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
        #region Правило <главная функция> → public static void main (String[] args) { <блок кода> }
        string voidmainCheck(List<lexem> lexems)
        {
            if (lexems[pos].id != "K10")
            {
                return "Ошибка: \"Ожидалось \"public\"\"";
            }
            else
            {
                pos++;
                if (lexems[pos].id != "K13")
                {
                    return "Ошибка: \"Ожидалось \"static\"\"";
                }
                else
                {
                    pos++;
                    if (lexems[pos].id != "R9")
                    {
                        return "Ошибка: \"Ожидалось \"void\"\"";
                    }
                    else
                    {
                        pos++;
                        if (lexems[pos].id != "K15")
                        {
                            return "Ошибка: \"Ожидалось \"main\"\"";
                        }
                        else 
                        {
                            pos++;
                            if (lexems[pos].id != "D6")
                            {
                                return "Ошибка: \"Ожидалось \'(\'\"";
                            }
                            else
                            {
                                pos++;
                                if (lexems[pos].id != "R10")
                                {
                                    return "Ошибка: \"Ожидалось \"String\"\"";
                                }
                                else
                                {
                                    pos++;
                                    if (lexems[pos].id != "D8")
                                    {
                                        return "Ошибка: \"Ожидалось \'[\'\"";
                                    }
                                    else
                                    {
                                        pos++;
                                        if (lexems[pos].id != "D9")
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
                                                if (lexems[pos].id != "D7")
                                                {
                                                    return "Ошибка: \"Ожидалось \')\'\"";
                                                }
                                                else
                                                {
                                                    pos++;
                                                    if (lexems[pos].id != "D4")
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
                                                        if (lexems[pos].id != "D5")
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
        string mainbodyCheck(List<lexem> lexems)
        {
            int i = pos;
            if(instructionCheck(lexems) != "success")
            {
                pos = i;
                return instructionCheck(lexems);
            }
            pos++;
            if (lexems[pos].id != "D5")
            {
                return mainbodyCheck(lexems);
            }
            return "success";
        }
        #endregion
        #region Правила объявления переменных
        #region Правило <тело класса> → <объявление переменной> <тело класса> | <объявление переменной> | <объявление функции> <тело класса> | <объявление функции> | <объявление константы> <тело класса> | <объявление константы> 
        string bodyclassCheck(List<lexem> lexems)
        {
            pos++;

            return "success";
        }
        #endregion
        #region Правило <объявление переменной> → <тип данных переменной> <имя или инициализация>
        string variableDeclarationCheck(List<lexem> lexems)
        {
            if ((lexems[pos].id != "R1") ||
                (lexems[pos].id != "R2") ||
                (lexems[pos].id != "R3") ||
                (lexems[pos].id != "R4") ||
                (lexems[pos].id != "R5") ||
                (lexems[pos].id != "R6") ||
                (lexems[pos].id != "R7"))
            {
                return "Ошибка: \"Ожидался тип данных\"";
            }
            else
            {
                pos++;
                int i = pos;
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
        #region Правило <имя или инициализация> → <имя переменной>, <имя или инициализация> | <имя переменной> = <значение>, <имя или инициализация> | <имя переменной>; | <имя переменной> = <значение>;

        string nameAndRealizationCheck(List<lexem> lexems)
        {
            if (lexems[pos].id != "I3")
            {
                return "Ошибка: \"Ожидалось имя переменной\"";
            }
            else
            {
                pos++;
                if ((lexems[pos].id != "D2") || (lexems[pos].id != "O23") || (lexems[pos].id != "D3"))
                {
                    return "Ошибка: \"Ожидалось\',\' или \'=\' или \';\'\"";
                }
                else if (lexems[pos].id == "D2")
                {
                    pos++;
                    return nameAndRealizationCheck(lexems);
                }
                else if (lexems[pos].id == "O23")
                {
                    pos++;
                    if (lexems[pos].id != "NN")
                    {
                        return "Ошибка: \"Ожидалось значение\"";
                    }
                    else
                    {
                        pos++;
                        if ((lexems[pos].id != "D2") || (lexems[pos].id) != "D3")
                        {
                            return "Ошибка: \"Ожидалось \',\' или \';\'\"";
                        }
                        else if (lexems[pos].id == "D2")
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


        #region Правило <инструкция> → <присваивание>; | <объявление переменной> | <объявление константы> | <вызов функции>; | <выражение>; | <цикл> | <ветвление> | <вывод в консоль>
        string instructionCheck(List<lexem> lexems)
        {
            return "success";
        }
        #endregion
    }


    //Структура для лексемы, где id - ключ(K1, K2, K3, etc...), value - ключевое слово(break, const, continue, etc...)
    public struct lexem
    {
        public string id;
        public string value;
        public lexem(string id, string value)
        {
            this.id = id;
            this.value = value;
        }
    }
}

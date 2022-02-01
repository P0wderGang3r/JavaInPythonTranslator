using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.EndPoints;

namespace JavaInPythonTranslator
{
    internal class FunctionRules
    {
        #region Правило <блок кода> → <инструкция> <блок кода> | <инструкция>
        public static string blockOfCodeCheck(List<LexList> lexems)
        {
            //Нужна проверка на первое вхождение
            if (!String.Equals(instructionCheck(lexems), successMessage))
            {
                return "Ожидалась \"Инструкция\"";
            }
            
            return blockOfCodeCheck(lexems);
        }
        #endregion

        #region <инструкция> → <объявление переменной> | <вызов функции> | <присваивание> | <цикл> | <ветвление>
        static string instructionCheck(List<LexList> lexems)
        {

            return successMessage;
        }
        #endregion


        #region Правило <объявление функции> → <тип данных функции> <имя функции> (<параметры функции>) {<тело функции>}
        static string functionDeclarationCheck(List<LexList> lexems)
        {
            int i = pos;
            if ((dataTypeCheck(lexems) != successMessage) || (lexems[pos].type != "R9"))
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
                        if (functionParamsCheck(lexems) != successMessage)
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
                                    if (functionBodyCheck(lexems) != successMessage)
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
                                            return successMessage;
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
        static string functionParamsCheck(List<LexList> lexems)
        {
            int i = pos;
            if (dataTypeCheck(lexems) != successMessage)
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
                        return successMessage;
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
        public static string functionBodyCheck(List<LexList> lexems)
        {
            int i = pos;
            if (blockOfCodeCheck(lexems) != successMessage)
            {
                pos = i;
                return blockOfCodeCheck(lexems);
            }
            else
            {
                pos++;
                if (lexems[pos].value == "return")
                {
                    return successMessage;//returnCheck(lexems);
                }
                else
                {
                    pos--;
                    return successMessage;
                }

            }
        }
        #endregion

        /*
        #region Правило <возврат значения> → return <выражение>;| return <имя переменной>; | return <имя константы>;
        static string returnCheck(List<LexList> lexems)
        {
            if ((lexems[pos].type != "I3") || (expressionCheck(lexems) != successMessage))
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
                    return successMessage;
                }
            }
        }
        #endregion
        */
        #region <вызов функции> → <начало идентификатора> (<параметры вызова функции>)

        #endregion
        #region <параметры вызова функции> → <выражение>| <выражение>, <параметры вызова функции> | λ

        #endregion

    }
}

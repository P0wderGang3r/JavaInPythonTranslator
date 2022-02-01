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

        #region Правило <объявление функции> → <тип данных функции> <имя функции> (<параметры функции>) {<тело функции>}
        static string functionDeclarationCheck(List<LexList> lexems)
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
        static string functionParamsCheck(List<LexList> lexems)
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
        static string functionBodyCheck(List<LexList> lexems)
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
        static string returnCheck(List<LexList> lexems)
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

        #region <вызов функции> → <начало идентификатора> (<параметры вызова функции>)

        #endregion
        #region <параметры вызова функции> → <выражение>| <выражение>, <параметры вызова функции> | λ

        #endregion

    }
}

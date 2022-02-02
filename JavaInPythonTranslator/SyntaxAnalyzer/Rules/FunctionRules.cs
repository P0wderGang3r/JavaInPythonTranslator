using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.BlockOfCodeRules;
using static JavaInPythonTranslator.ExpressionRules;

namespace JavaInPythonTranslator
{
    internal class FunctionRules
    {
        #region <вызов функции> → <начало идентификатора> (<параметры вызова функции>)
        public static string callFunctionCheck(List<LexList> lexems)
        {
            string check;

            check = EndPoints.IdentificatorCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;
            pos++;

            check = compare(lexems[pos].type, D6);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, D7);
            if (String.Equals(check, successMessage))
            {
                pos--;
                return check;
            }

            check = callFunctionParamsCheck(lexems);
            if (!String.Equals(check, successMessage))
                return check;

            check = compare(lexems[pos].type, D7);
            if (!String.Equals(check, successMessage))
                return check;

            return successMessage;
        }
        #endregion

        #region <параметры вызова функции> → <выражение>, <параметры вызова функции> | λ
        static string callFunctionParamsCheck(List<LexList> lexems)
        {
            string check;
            bool trigger = false;

            //Проверка на запятую
            check = compare(lexems[pos].type, D2);
            if (String.Equals(check, successMessage)) {
                trigger = true;
            }

            //Если найдена скобка после запятой, то ошибка
            check = compare(lexems[pos].type, D7);
            if (String.Equals(check, successMessage) && trigger)
            {
                return "Ошибка: встречена \")\" вместо выражения";
            }
            if (String.Equals(check, successMessage) && !trigger)
                pos--;

            //Если найдена скобка, то возвращаем успех проверки
            check = compare(lexems[pos].type, D7);
            if (String.Equals(check, successMessage))
            {
                pos--;
                return check;
            }
            
            //Проверка на принадлежность классу выражение следующей после запятой лексемы
            check = expressionCheck(lexems);
            if (!String.Equals(check, successMessage))
            {
                return check;
            }

            return callFunctionParamsCheck(lexems);
        }
        #endregion

        #region <объявление функции> → <тип данных функции> <имя функции> (<параметры функции>) {<тело функции>}
        public static string functionDeclarationCheck(List<LexList> lexems)
        {
            int i = pos;
            if ((EndPoints.dataTypeCheck(lexems) != successMessage) || (lexems[pos].type != "R9"))
            {
                pos = i;
                return EndPoints.dataTypeCheck(lexems);
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

        #region <параметры функции> → <тип данных переменной> <идентификатор> | <тип данных переменной> <идентификатор>, <параметры функции> 
        static string functionParamsCheck(List<LexList> lexems)
        {
            int i = pos;
            if (EndPoints.dataTypeCheck(lexems) != successMessage)
            {
                pos = i;
                return EndPoints.dataTypeCheck(lexems);
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

        #region <тело функции> → <блок кода> <возврат значения> | <блок кода>
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

    }
}

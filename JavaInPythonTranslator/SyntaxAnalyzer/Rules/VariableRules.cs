using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.EndPoints;

namespace JavaInPythonTranslator
{
    static internal class VariableRules
    {
        #region Правило <объявление переменной> → <тип данных переменной> <имя или инициализация>
        static string variableDeclarationCheck(List<LexList> lexems)
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

        #region <имя или инициализация> → <начало идентификатора> | <начало идентификатора> = <значение>;

        static string nameAndRealizationCheck(List<LexList> lexems)
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

        #region <значение> → <число> | <строковое значение> | <символьное значение> | <логическое значение>

        #endregion
    }
}

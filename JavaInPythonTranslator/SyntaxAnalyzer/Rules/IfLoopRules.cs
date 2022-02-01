using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.SyntaxGlobals;
using static JavaInPythonTranslator.EndPoints;

namespace JavaInPythonTranslator
{
    static internal class IfLoopRules
    {
        #region <цикл> → while (<логическое выражение>) {<тело цикла>} | do {<тело цикла>} while (<логическое выражение>); | for (<инструкция>; <логическое выражение>; <инструкция>) {<тело цикла>}

        #endregion

        #region <тело цикла> → <блок кода> <оператор цикла> <блок кода> | <оператор цикла> <блок кода> | <блок кода> <оператор цикла> | <блок кода> | <оператор цикла>

        #endregion

        #region <ветвление> → if (<логическое выражение>) {<блок кода>} | if (<логическое выражение>) {<блок кода>} else {<блок кода>} | if (<логическое выражение>) {<блок кода>} else <ветвление>

        #endregion

    }
}

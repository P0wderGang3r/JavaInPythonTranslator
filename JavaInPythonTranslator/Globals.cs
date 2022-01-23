using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInPythonTranslator
{
    internal class Globals
    {
        /// <summary>
        /// <br>Если = 0, то логи не должны выводиться в принципе</br>
        /// <br>Если = 1, то должны выводиться только отчёты о работе блоков</br>
        /// <br>Если >= 2, то может выводиться любая информация, которая нужна для отладки</br>
        /// <br>Блок if должен выглядеть так: if (Globals.logVerboseLevel {== или >=} ?) ...</br>
        /// </summary>
        public static int logVerboseLevel = 1;

        public static bool lexSpaces = false;
    }
}

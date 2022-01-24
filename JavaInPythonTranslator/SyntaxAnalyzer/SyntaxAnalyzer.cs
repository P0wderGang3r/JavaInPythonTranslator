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
        string StartRule(List<lexem> lexems)
        {
            if((lexems[pos].value != "package") && (lexems[pos].id != "K9") && (lexems[pos].id != "R1") && (lexems[pos].id != "K10"))
            {
                return "Ошибка: \"Не встречено ключевых слов начала программы\"";
            }
            else if(lexems[pos].value == "package")
            {
                pos++;
                //Метод на подключение пакета
            }
            else if(lexems[pos].id == "K9")
            {
                pos++;
                return Rule1(lexems);
            }
            else if (lexems[pos].id == "R1")
            {
                pos++;
                //Метод на создание класса
            }
            else 
            {
                pos++;
                if (lexems[pos].id != "K10")
                {
                    return "Ошибка: \"Ожидалось ключевое слово \"class\"\"";
                }
                else
                {
                    pos++;
                    //Метод на создание класса
                }
            }
        }
        #endregion
        #region Правило <подключение пакетов> → <подключение пакета> <подключение пакетов> | <объявление класса> 
        string Rule1(List<lexem> lexems)
        {
            if (lexems[pos].value != "java.lang.Math")
            {
                return "Ошибка: \"Ожидалось \"java.lang.Math\"\"";
            }
            else
            {
                pos++;
                //какой-то блять метод ебаный нихуя сука непонятно блять в пизду нахуй в пизду блять
            }
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

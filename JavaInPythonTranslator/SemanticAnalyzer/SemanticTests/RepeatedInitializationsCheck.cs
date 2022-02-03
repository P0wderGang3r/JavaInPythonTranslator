using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.Globals;

namespace JavaInPythonTranslator
{
    internal class RepeatedInitializationsCheck
    {
        struct RInits {
            public string type;
            public string value;

            public RInits(string type, string value)
            {
                this.type = type;
                this.value = value;
            }
        }

        static List<RInits> repeatedInits = new();

        public static bool repeatedInitializations(List<TreeNode> treeNodes)
        {
            //Проход по строке
            for (int pos = 0; pos < treeNodes.Count; pos++)
            {
                //Если находим лексему-тип, то смотрим дальше
                if (treeNodes[pos].lexem.type[0] == 'T')
                {
                    //Проходимся по типам переменных в надежде найти существующий
                    foreach (LexicalClasses lexClass in dividerClasses)
                    {
                        //Если находим такой тип, то смотрим дальше
                        if (treeNodes[pos].lexem.type == lexClass.getLexClass())
                        {
                            RInits newElem = new RInits(treeNodes[pos].lexem.value, treeNodes[pos + 1].lexem.value);

                            //Проверяем переменную на предмет её наличия в списке переменных
                            foreach (RInits repeatedInit in repeatedInits)
                            {
                                if (repeatedInit.value == newElem.value)
                                {
                                    return true;
                                }
                            }
                            //Добавляем элемент в рамках данного "Скоупа"
                            repeatedInits.Add(newElem);

                            foreach (TreeNode treeNode in treeNodes)
                            {
                                if (treeNode.nextLevelNodes != null)
                                {
                                    repeatedInitializations(treeNode.nextLevelNodes);
                                }
                            }
                            //Удаляем элемент, чтобы не проверять его наличие за пределами данного "Скоупа"
                            repeatedInits.Remove(newElem);

                        }
                    }
                }
            }

            foreach (TreeNode treeNode in treeNodes)
            {
                if (treeNode.nextLevelNodes != null)
                {
                    repeatedInitializations(treeNode.nextLevelNodes);
                }
            }
            return false;
        }

    }
}

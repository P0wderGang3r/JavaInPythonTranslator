using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JavaInPythonTranslator.Globals;

namespace JavaInPythonTranslator
{
    internal class IsInitializedIdentificatorsUsedCheck
    {
        struct RInits
        {
            public string type;
            public string value;
            public List<TreeNode> node;

            public RInits(string type, string value, List<TreeNode> node)
            {
                this.type = type;
                this.value = value;
                this.node = node;
            }
        }

        static List<RInits> repeatedInits = new();

        public static bool makeBibliary(List<TreeNode> treeNodes)
        {
            //Проход по строке
            for (int pos = 0; pos < treeNodes.Count; pos++)
            {
                //Если находим лексему-import, то смотрим дальше
                if (treeNodes[pos].lexem.type == "K1")
                {
                    RInits newElem = new RInits(treeNodes[pos].lexem.value, treeNodes[pos + 1].lexem.value, treeNodes);
                    repeatedInits.Add(newElem);
                }

                    //Если находим лексему-тип, то смотрим дальше
                if (treeNodes[pos].lexem.type[0] == 'T')
                {
                    //Проходимся по типам переменных в надежде найти существующий
                    foreach (LexicalClasses lexClass in dividerClasses)
                    {
                        //Если находим такой тип, то смотрим дальше
                        if (treeNodes[pos].lexem.type == lexClass.getLexClass())
                        {
                            RInits newElem = new RInits(treeNodes[pos].lexem.value, treeNodes[pos + 1].lexem.value, treeNodes);

                            //Создаём глоссарий переменных
                            foreach (RInits repeatedInit in repeatedInits)
                            {
                                if (!(repeatedInit.value == newElem.value))
                                {
                                    repeatedInits.Add(newElem);
                                }
                            }

                        }
                    }
                }
            }

            foreach (TreeNode treeNode in treeNodes)
            {
                if (treeNode.nextLevelNodes != null)
                {
                    makeBibliary(treeNode.nextLevelNodes);
                }
            }
            return false;
        }

        public static bool isInitializedUsedMain(List<TreeNode> treeNodes)
        {
            makeBibliary(treeNodes);

            return false;
        }
    }
}

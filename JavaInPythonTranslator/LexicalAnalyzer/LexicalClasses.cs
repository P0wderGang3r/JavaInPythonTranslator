using System.Text.RegularExpressions;

namespace JavaInPythonTranslator
{
    class LexicalClasses
    {
        private String lexClass = "";
        private Regex regEx;

        public LexicalClasses(String lexClass, String regEx)
        {
            this.lexClass = lexClass;
            this.regEx = new Regex(regEx);
        }

        public String getLexClass()
        {
            return this.lexClass;
        }

        public Regex getRegEx()
        {
            return this.regEx;
        }
    }
}

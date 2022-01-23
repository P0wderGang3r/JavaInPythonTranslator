namespace JavaInPythonTranslator
{
    class LexicalClasses
    {
        private String lexClass = "";
        private String regEx;

        public LexicalClasses(String lexClass, String regEx)
        {
            this.lexClass = lexClass;
            this.regEx = new String(regEx);
        }

        public String getLexClass()
        {
            return this.lexClass;
        }

        public String getRegEx()
        {
            return this.regEx;
        }
    }
}

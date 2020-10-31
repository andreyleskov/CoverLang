using Sprache;

namespace CoverLang
{
    public static class FormulaGrammar
    {
        public static class Parts
        {
            public const string FormulaKeyword = "'formula' keyword";
            public const string Returning = "'returning' keyword";
            public const string DataTypePart = "formula return data type";
            public const string AsPart = "'as' keyword";
                
            public const string Name = "formula name";
            public static string Body = "formula body";
            public static string BodyIdent = "formula body ident";
        }

        public static readonly Parser<FormulaSignature> FormulaDefinition =
            from hasKeyWord in CoverLangGrammar.KeyWord("formula").Named(Parts.FormulaKeyword)
            from name in CoverLangGrammar.Identifier.Named(Parts.Name)
            from returning in CoverLangGrammar.KeyWord("returning").Named(Parts.Returning)
            from dataType in CoverLangGrammar.DataType.Named(Parts.DataTypePart)
            from aspart in CoverLangGrammar.KeyWord("as").Named(Parts.AsPart)
            select new FormulaSignature {Name = name, ReturnDataType = dataType};

        public static readonly Parser<Formula> Formula =
            from definition in FormulaDefinition
            from bodyBegin in Parse.WhiteSpace.Repeat(4)//.Named(Parts.BodyIdent)
            from body in Parse.AnyChar.Many().Text().Named(Parts.Body)
            select new Formula {Signature = definition, Body = body};
    }
}
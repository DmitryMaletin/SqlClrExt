using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

public partial class RegularExpressionFunctions
    {
    public static readonly System.Text.RegularExpressions.RegexOptions Options =  
    
        System.Text.RegularExpressions.RegexOptions.IgnoreCase |
        System.Text.RegularExpressions.RegexOptions.Singleline |
        System.Text.RegularExpressions.RegexOptions.CultureInvariant |
        System.Text.RegularExpressions.RegexOptions.Compiled
        ;

        [SqlFunction
            (IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None, SystemDataAccess = SystemDataAccessKind.None)]
        public static SqlBoolean RegExIsMatch([SqlFacet(MaxSize = -1)]SqlString StringToParse, [SqlFacet(MaxSize = -1)]SqlString RegEx)
        {
            return new SqlBoolean(System.Text.RegularExpressions.Regex.IsMatch(StringToParse.Value, RegEx.Value, Options));
        }

        [return: SqlFacet(MaxSize = -1)]
        [SqlFunction 
        (IsDeterministic = true, IsPrecise = true,  DataAccess = DataAccessKind.None, SystemDataAccess = SystemDataAccessKind.None)]
        public static SqlString RegExReplace([SqlFacet(MaxSize = -1)]SqlString StringToParse, [SqlFacet(MaxSize = -1)]SqlString RegEx, [SqlFacet(MaxSize = -1)]SqlString ReplacementText)
        {
            return new SqlString(System.Text.RegularExpressions.Regex.Replace(StringToParse.Value, RegEx.Value, ReplacementText.Value, Options));
        }

        // 
        [SqlFunction
        (
        FillRowMethodName = "FillValidatedRow",
        TableDefinition = "isMatched bit",
        IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None, SystemDataAccess = SystemDataAccessKind.None)]

        public static System.Collections.IEnumerable tvf_RegExIsMatch([SqlFacet(MaxSize = -1)]SqlString StringToParse, [SqlFacet(MaxSize = -1)]SqlString RegEx)
        {

            //ReturnValues rows = new ReturnValues();
            //rows.Value = new SqlString(System.Text.RegularExpressions.Regex.Replace(StringToParse.Value, RegEx.Value, ReplacementText.Value, Options));
            yield return new SqlBoolean(System.Text.RegularExpressions.Regex.IsMatch(StringToParse.Value, RegEx.Value, Options));

        }
        public static void FillValidatedRow(object obj, ref SqlBoolean isMatched)
        {
            SqlBoolean val = (SqlBoolean)obj;
            isMatched = val.Value;
        }

        //private struct ReturnValues
        //{
        //    public SqlString Value;
        //}   
        [SqlFunction
        (
        FillRowMethodName = "FillRow",
        TableDefinition = "ParsedString nvarchar(max)",
        IsDeterministic = true, IsPrecise = true, DataAccess = DataAccessKind.None, SystemDataAccess = SystemDataAccessKind.None)]

        public static System.Collections.IEnumerable tvf_RegExReplace([SqlFacet(MaxSize = -1)]SqlString StringToParse, [SqlFacet(MaxSize = -1)]SqlString RegEx, [SqlFacet(MaxSize = -1)]SqlString ReplacementText)
        {

            //ReturnValues rows = new ReturnValues();
            //rows.Value = new SqlString(System.Text.RegularExpressions.Regex.Replace(StringToParse.Value, RegEx.Value, ReplacementText.Value, Options));
            yield return new SqlString(System.Text.RegularExpressions.Regex.Replace(StringToParse.Value, RegEx.Value, ReplacementText.Value, Options));

        }
        public static void FillRow(object obj, ref SqlString ParsedString)
        {
            SqlString str = (SqlString)obj;
            ParsedString = str.Value;
        }
}


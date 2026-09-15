using System.Drawing;

namespace DynamoDBUI.Utils
{
    /// <summary>
    /// Palet warna terpusat untuk seluruh aplikasi (dark purple theme).
    /// </summary>
    public static class DarkTheme
    {
        // Header & Sidebar
        public static readonly Color HeaderPurple = ColorTranslator.FromHtml("#2D1B4E");
        public static readonly Color HeaderPurpleLight = ColorTranslator.FromHtml("#5B3A99");
        public static readonly Color AccentPurple = ColorTranslator.FromHtml("#7C4DFF");

        // Query editor
        public static readonly Color EditorBackground = ColorTranslator.FromHtml("#1E1E2A");
        public static readonly Color EditorForeground = ColorTranslator.FromHtml("#D4D4D4");

        // Result grid
        public static readonly Color ResultsBackground = ColorTranslator.FromHtml("#242435");
        public static readonly Color ResultsAltRow = ColorTranslator.FromHtml("#2C2C40");
        public static readonly Color GridLines = ColorTranslator.FromHtml("#3A3A50");
        public static readonly Color ResultsText = Color.Gainsboro;

        // Syntax highlight
        public static readonly Color KeywordStatement = ColorTranslator.FromHtml("#FF79C6"); // VIEW, CREATE, INSERT, DROP
        public static readonly Color KeywordClause = ColorTranslator.FromHtml("#569CD6");     // TABLE, ORDER, BY, FINDBY, dst
        public static readonly Color TableName = ColorTranslator.FromHtml("#4EC9B0");         // nama table
        public static readonly Color StringLiteral = ColorTranslator.FromHtml("#CE9178");     // 'text'
        public static readonly Color NumberLiteral = ColorTranslator.FromHtml("#B5CEA8");     // 123
    }
}
using System.Drawing;

namespace DynamoDBUI.Utils
{
    /// <summary>
    /// Palet warna terpusat untuk seluruh aplikasi (dark charcoal + cyan/teal accent theme).
    /// </summary>
    public static class DarkTheme
    {
        // Header & Sidebar
        public static readonly Color PanelBackground = ColorTranslator.FromHtml("#20242B");
        public static readonly Color PanelHighlight = ColorTranslator.FromHtml("#2D333B");
        public static readonly Color AccentPrimary = ColorTranslator.FromHtml("#2DD4BF");
        public static readonly Color AccentPrimaryDark = ColorTranslator.FromHtml("#159C8E");

        // Query tabs
        public static readonly Color TabInactive = ColorTranslator.FromHtml("#181B20");
        public static readonly Color TabActiveAccent = ColorTranslator.FromHtml("#67E8F9");

        // Query editor
        public static readonly Color EditorBackground = ColorTranslator.FromHtml("#1B1E23");
        public static readonly Color EditorForeground = ColorTranslator.FromHtml("#D4D4D4");

        // Result grid
        public static readonly Color ResultsBackground = ColorTranslator.FromHtml("#1F2329");
        public static readonly Color ResultsAltRow = ColorTranslator.FromHtml("#262B32");
        public static readonly Color GridLines = ColorTranslator.FromHtml("#333941");
        public static readonly Color ResultsText = ColorTranslator.FromHtml("#D8DEE4");

        // Toolbar icon buttons (Run / Commit / Un Commit)
        public static readonly Color RunAccent = ColorTranslator.FromHtml("#FF8A3D");
        public static readonly Color CommitAccent = ColorTranslator.FromHtml("#2DD4BF");
        public static readonly Color UndoAccent = ColorTranslator.FromHtml("#C9D1D9");

        // Syntax highlight
        public static readonly Color KeywordStatement = ColorTranslator.FromHtml("#FF79C6"); // VIEW, CREATE, INSERT, DROP
        public static readonly Color KeywordClause = ColorTranslator.FromHtml("#569CD6");     // TABLE, ORDER, BY, FINDBY, dst
        public static readonly Color TableName = ColorTranslator.FromHtml("#4EC9B0");         // nama table
        public static readonly Color StringLiteral = ColorTranslator.FromHtml("#CE9178");     // 'text'
        public static readonly Color NumberLiteral = ColorTranslator.FromHtml("#B5CEA8");     // 123
    }
}
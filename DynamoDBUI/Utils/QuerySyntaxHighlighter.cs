using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DynamoDBUI.Utils
{
    /// <summary>
    /// Pewarnaan sintaks sederhana untuk mini query language DynamoDBUI.
    /// </summary>
    public static class QuerySyntaxHighlighter
    {
        private static readonly Regex StatementKeywords = new Regex(
            @"\b(VIEW|CREATE|INSERT|DROP)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex ClauseKeywords = new Regex(
            @"\b(TABLE|INTO|VALUES|FINDBY|ORDER|BY|ASC|DESC|PK|SK|STRING|NUMBER|BOOL)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex StringLiteral = new Regex(
            @"'[^']*'|""[^""]*""", RegexOptions.Compiled);

        private static readonly Regex NumberLiteral = new Regex(
            @"(?<![\w.])\d+(\.\d+)?\b", RegexOptions.Compiled);

        // Nama table = kata setelah VIEW / TABLE / INTO
        private static readonly Regex TableIdentifier = new Regex(
            @"(?<=\b(VIEW|TABLE|INTO)\s+)\w+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static bool _isHighlighting;

        public static void Highlight(RichTextBox rtb)
        {
            if (_isHighlighting || string.IsNullOrEmpty(rtb.Text)) return;
            _isHighlighting = true;

            int selStart = rtb.SelectionStart;
            int selLength = rtb.SelectionLength;

            NativeMethods.SuspendDrawing(rtb);

            rtb.SelectAll();
            rtb.SelectionColor = DarkTheme.EditorForeground;

            ApplyColor(rtb, StringLiteral, DarkTheme.StringLiteral);
            ApplyColor(rtb, NumberLiteral, DarkTheme.NumberLiteral);
            ApplyColor(rtb, StatementKeywords, DarkTheme.KeywordStatement);
            ApplyColor(rtb, ClauseKeywords, DarkTheme.KeywordClause);
            ApplyColor(rtb, TableIdentifier, DarkTheme.TableName);

            rtb.SelectionStart = selStart;
            rtb.SelectionLength = selLength;
            rtb.SelectionColor = DarkTheme.EditorForeground;

            NativeMethods.ResumeDrawing(rtb);

            _isHighlighting = false;
        }

        private static void ApplyColor(RichTextBox rtb, Regex pattern, System.Drawing.Color color)
        {
            foreach (Match m in pattern.Matches(rtb.Text))
            {
                rtb.Select(m.Index, m.Length);
                rtb.SelectionColor = color;
            }
        }
    }
}
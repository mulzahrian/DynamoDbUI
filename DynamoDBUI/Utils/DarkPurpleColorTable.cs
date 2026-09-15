using System.Drawing;
using System.Windows.Forms;

namespace DynamoDBUI.Utils
{
    public class DarkPurpleColorTable : ProfessionalColorTable
    {
        private readonly Color _base = DarkTheme.HeaderPurple;
        private readonly Color _hover = DarkTheme.HeaderPurpleLight;

        public override Color MenuStripGradientBegin => _base;
        public override Color MenuStripGradientEnd => _base;
        public override Color ToolStripDropDownBackground => _base;
        public override Color ImageMarginGradientBegin => _base;
        public override Color ImageMarginGradientMiddle => _base;
        public override Color ImageMarginGradientEnd => _base;
        public override Color MenuItemSelected => _hover;
        public override Color MenuItemSelectedGradientBegin => _hover;
        public override Color MenuItemSelectedGradientEnd => _hover;
        public override Color MenuItemBorder => _hover;
        public override Color MenuBorder => _base;
        public override Color ButtonSelectedHighlight => _hover;
        public override Color ButtonSelectedGradientBegin => _hover;
        public override Color ButtonSelectedGradientEnd => _hover;
        public override Color ButtonPressedGradientBegin => _hover;
        public override Color ButtonPressedGradientEnd => _hover;
        public override Color StatusStripGradientBegin => _base;
        public override Color StatusStripGradientEnd => _base;
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DynamoDBUI.Forms
{
    /// <summary>
    /// Pengganti MessageBox.Show bawaan Windows supaya tampil dengan tema dark aplikasi.
    /// </summary>
    public static class ThemedMessageBox
    {
        public static DialogResult Show(IWin32Window owner, string message, string title,
            ThemedMessageIcon icon = ThemedMessageIcon.Info,
            ThemedMessageButtons buttons = ThemedMessageButtons.OK)
        {
            using (var form = new ThemedMessageForm(message, title, icon, buttons))
            {
                return form.ShowDialog(owner);
            }
        }
    }
}

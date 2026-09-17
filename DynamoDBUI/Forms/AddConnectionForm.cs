using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynamoDBUI.Models;
using DynamoDBUI.Services;
using DynamoDBUI.Utils;

namespace DynamoDBUI.Forms
{
    public partial class AddConnectionForm : Form
    {
        private readonly HashSet<string> _existingNames;
        private bool _isTesting;

        public ConnectionProfile Result { get; private set; }

        public AddConnectionForm(IEnumerable<string> existingNames)
        {
            InitializeComponent();
            ApplyTheme();

            _existingNames = new HashSet<string>(existingNames ?? Enumerable.Empty<string>(),
                StringComparer.OrdinalIgnoreCase);
        }

        private void ApplyTheme()
        {
            BackColor = DarkTheme.DialogBackground;

            foreach (var lbl in new[] { lblName, lblAccessKey, lblSecretKey, lblSessionToken, lblRegion, lblServiceUrl })
                lbl.ForeColor = Color.Gainsboro;

            foreach (var txt in new[] { txtName, txtAccessKey, txtSecretKey, txtSessionToken, txtRegion, txtServiceUrl })
            {
                txt.BackColor = DarkTheme.InputBackground;
                txt.ForeColor = Color.White;
                txt.BorderStyle = BorderStyle.FixedSingle;
            }

            lblStatus.ForeColor = Color.Gainsboro;

            btnTest.BackColor = DarkTheme.DialogBackground;
            btnTest.ForeColor = DarkTheme.AccentPrimary;
            btnTest.FlatAppearance.BorderSize = 1;
            btnTest.FlatAppearance.BorderColor = DarkTheme.AccentPrimary;
            btnTest.FlatAppearance.MouseOverBackColor = DarkTheme.PanelHighlight;

            btnOk.BackColor = DarkTheme.AccentPrimary;
            btnOk.ForeColor = Color.Black;
            btnOk.FlatAppearance.MouseOverBackColor = ControlPaint.Light(DarkTheme.AccentPrimary, 0.25f);
            btnOk.FlatAppearance.MouseDownBackColor = DarkTheme.AccentPrimaryDark;

            btnCancel.BackColor = DarkTheme.PanelHighlight;
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatAppearance.MouseOverBackColor = ControlPaint.Light(DarkTheme.PanelHighlight, 0.3f);
        }

        private ConnectionProfile BuildProfile()
        {
            return new ConnectionProfile
            {
                Name = txtName.Text.Trim(),
                AccessKey = txtAccessKey.Text.Trim(),
                SecretKey = txtSecretKey.Text.Trim(),
                SessionToken = txtSessionToken.Text.Trim(),
                Region = txtRegion.Text.Trim(),
                ServiceUrl = txtServiceUrl.Text.Trim()
            };
        }

        private bool ValidateFields(ConnectionProfile profile)
        {
            if (string.IsNullOrEmpty(profile.Name) || string.IsNullOrEmpty(profile.ServiceUrl))
            {
                SetStatus("Connection Name dan Service URL wajib diisi.", isError: true);
                return false;
            }

            if (_existingNames.Contains(profile.Name))
            {
                SetStatus("Nama connection sudah dipakai, pilih nama lain.", isError: true);
                return false;
            }

            return true;
        }

        private async Task<(bool Success, string Message)> TestConnectionAsync(ConnectionProfile profile)
        {
            try
            {
                var service = new DynamoDbService(profile);
                service.Connect();
                await service.ListTablesAsync();
                return (true, "Koneksi berhasil! Server DynamoDB merespons dengan baik.");
            }
            catch (Exception ex)
            {
                return (false, "Koneksi gagal: " + ex.Message);
            }
        }

        private void SetStatus(string message, bool isError = false, bool isSuccess = false)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? DarkTheme.DangerAccent
                : isSuccess ? DarkTheme.SuccessAccent
                : Color.Gainsboro;
        }

        private void SetBusy(bool busy)
        {
            _isTesting = busy;
            btnTest.Enabled = !busy;
            btnOk.Enabled = !busy;
            foreach (var txt in new[] { txtName, txtAccessKey, txtSecretKey, txtSessionToken, txtRegion, txtServiceUrl })
                txt.Enabled = !busy;
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            if (_isTesting) return;

            var profile = BuildProfile();
            if (string.IsNullOrEmpty(profile.ServiceUrl))
            {
                SetStatus("Isi Service URL dulu sebelum test connection.", isError: true);
                return;
            }

            SetBusy(true);
            SetStatus("Menguji koneksi...");

            var (success, message) = await TestConnectionAsync(profile);

            SetBusy(false);
            SetStatus(message, isError: !success, isSuccess: success);
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (_isTesting) return;

            var profile = BuildProfile();
            if (!ValidateFields(profile)) return;

            SetBusy(true);
            SetStatus("Menguji koneksi...");

            var (success, message) = await TestConnectionAsync(profile);

            SetBusy(false);

            if (!success)
            {
                SetStatus(message + "  Connection tidak bisa disimpan sebelum berhasil terhubung.", isError: true);
                return;
            }

            Result = profile;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

namespace RecordBook.UI.View
{
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Visuals;
    using System.Globalization;

    public partial class Popup_Progress : Form
    {
        public Popup_Progress()
        {
            InitializeComponent();
            lblStatus.Text = string.Empty;
            progress.Percent = 0f;
            ThemeChanged(PluginMain.GetApplication().VisualTheme);
        }

        /// <summary>
        /// UpdateProgress will update the progress bar to the supplied percentage
        /// </summary>
        /// <param name="percent">The percentage to show on the progress bar</param>
        /// <param name="text">The text to display along side the progress bar</param>
        public void UpdateProgress(float percent, string text)
        {
            progress.Percent = percent;
            lblStatus.Text = text;
            this.Update();
            Application.DoEvents();
        }

        /// <summary>
        /// ThemeChanged will change the color theme to match the user's selection
        /// </summary>
        /// <param name="visualTheme">ITheme to apply to the progress bar</param>
        public void ThemeChanged(ITheme visualTheme)
        {
            progress.ThemeChanged(visualTheme);
            lblStatus.ForeColor = visualTheme.ControlText;
        }

        public void UICultureChanged(CultureInfo culture)
        {
            this.Text = string.Format(Resources.Strings.Label_CalculatingRecords, string.Empty);
        }
    }
}

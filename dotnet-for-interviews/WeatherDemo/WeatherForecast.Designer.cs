namespace WeatherDemo
{
    partial class WeatherForecast
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblAppTitle = new Label();
            lblCity = new Label();
            txtBoxCity = new TextBox();
            btnSearch = new Button();
            lblStatus = new Label();
            pbxWeatherIcon = new PictureBox();
            weatherPanel = new Panel();
            lblWeatherDetails = new Label();
            ((System.ComponentModel.ISupportInitialize)pbxWeatherIcon).BeginInit();
            weatherPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblAppTitle
            // 
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAppTitle.Location = new Point(33, 28);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(702, 74);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "Current Weather Forecast";
            lblAppTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCity.Location = new Point(51, 125);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(175, 45);
            lblCity.TabIndex = 1;
            lblCity.Text = "City Name";
            // 
            // txtBoxCity
            // 
            txtBoxCity.Location = new Point(232, 135);
            txtBoxCity.Name = "txtBoxCity";
            txtBoxCity.Size = new Size(680, 31);
            txtBoxCity.TabIndex = 2;
            txtBoxCity.TextChanged += txtBoxCity_TextChanged;
            txtBoxCity.KeyDown += txtBoxCity_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Orange;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(933, 132);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(229, 34);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Get Weather Details";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = SystemColors.ControlDark;
            lblStatus.Location = new Point(232, 170);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(363, 25);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Type city name and get current weather data";
            // 
            // pbxWeatherIcon
            // 
            pbxWeatherIcon.Location = new Point(29, 29);
            pbxWeatherIcon.Name = "pbxWeatherIcon";
            pbxWeatherIcon.Size = new Size(83, 75);
            pbxWeatherIcon.TabIndex = 6;
            pbxWeatherIcon.TabStop = false;
            // 
            // weatherPanel
            // 
            weatherPanel.BackColor = SystemColors.ButtonHighlight;
            weatherPanel.Controls.Add(lblWeatherDetails);
            weatherPanel.Controls.Add(pbxWeatherIcon);
            weatherPanel.Location = new Point(232, 227);
            weatherPanel.Name = "weatherPanel";
            weatherPanel.Size = new Size(834, 284);
            weatherPanel.TabIndex = 7;
            weatherPanel.Visible = false;
            // 
            // lblWeatherDetails
            // 
            lblWeatherDetails.AutoSize = true;
            lblWeatherDetails.Location = new Point(148, 29);
            lblWeatherDetails.Name = "lblWeatherDetails";
            lblWeatherDetails.Size = new Size(0, 25);
            lblWeatherDetails.TabIndex = 7;
            // 
            // WeatherForecast
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(1216, 676);
            Controls.Add(weatherPanel);
            Controls.Add(lblStatus);
            Controls.Add(btnSearch);
            Controls.Add(txtBoxCity);
            Controls.Add(lblCity);
            Controls.Add(lblAppTitle);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WeatherForecast";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Weather Forecast";
            Load += WeatherForecast_Load;
            ((System.ComponentModel.ISupportInitialize)pbxWeatherIcon).EndInit();
            weatherPanel.ResumeLayout(false);
            weatherPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAppTitle;
        private Label lblCity;
        private TextBox txtBoxCity;
        private Button btnSearch;
        private Label lblStatus;
        private PictureBox pbxWeatherIcon;
        private Panel weatherPanel;
        private Label lblWeatherDetails;
    }
}

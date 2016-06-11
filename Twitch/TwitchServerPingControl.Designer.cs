namespace Twitch
{
    partial class TwitchServerPingControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServerNameLabel = new System.Windows.Forms.Label();
            this.ServerPingLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ServerNameLabel
            // 
            this.ServerNameLabel.AutoSize = true;
            this.ServerNameLabel.Location = new System.Drawing.Point(24, -1);
            this.ServerNameLabel.Name = "ServerNameLabel";
            this.ServerNameLabel.Size = new System.Drawing.Size(82, 13);
            this.ServerNameLabel.TabIndex = 0;
            this.ServerNameLabel.Text = "SERVERNAME";
            // 
            // ServerPingLabel
            // 
            this.ServerPingLabel.AutoSize = true;
            this.ServerPingLabel.Location = new System.Drawing.Point(229, -1);
            this.ServerPingLabel.Name = "ServerPingLabel";
            this.ServerPingLabel.Size = new System.Drawing.Size(33, 13);
            this.ServerPingLabel.TabIndex = 0;
            this.ServerPingLabel.Text = "PING";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.checkBox1.Location = new System.Drawing.Point(5, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(13, 12);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // TwitchServerPingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.ServerPingLabel);
            this.Controls.Add(this.ServerNameLabel);
            this.Name = "TwitchServerPingControl";
            this.Size = new System.Drawing.Size(266, 15);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ServerNameLabel;
        private System.Windows.Forms.Label ServerPingLabel;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

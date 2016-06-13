namespace Twitch
{
    partial class MainForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStartParallelEvents = new System.Windows.Forms.Button();
            this.buttonStartParallelAwait = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 41);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(3, 0);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // buttonStartParallelEvents
            // 
            this.buttonStartParallelEvents.Location = new System.Drawing.Point(93, 12);
            this.buttonStartParallelEvents.Name = "buttonStartParallelEvents";
            this.buttonStartParallelEvents.Size = new System.Drawing.Size(75, 23);
            this.buttonStartParallelEvents.TabIndex = 1;
            this.buttonStartParallelEvents.Text = "Start events";
            this.buttonStartParallelEvents.UseVisualStyleBackColor = true;
            // 
            // buttonStartParallelAwait
            // 
            this.buttonStartParallelAwait.Location = new System.Drawing.Point(174, 12);
            this.buttonStartParallelAwait.Name = "buttonStartParallelAwait";
            this.buttonStartParallelAwait.Size = new System.Drawing.Size(75, 23);
            this.buttonStartParallelAwait.TabIndex = 1;
            this.buttonStartParallelAwait.Text = "Start await";
            this.buttonStartParallelAwait.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(261, 43);
            this.Controls.Add(this.buttonStartParallelAwait);
            this.Controls.Add(this.buttonStartParallelEvents);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStartParallelEvents;
        private System.Windows.Forms.Button buttonStartParallelAwait;
    }
}


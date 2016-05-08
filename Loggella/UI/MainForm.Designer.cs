namespace Loggella.UI
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.uiComponents = new System.Windows.Forms.GroupBox();
      this.uiPlayPause = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // uiComponents
      // 
      this.uiComponents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.uiComponents.Location = new System.Drawing.Point(12, 41);
      this.uiComponents.Name = "uiComponents";
      this.uiComponents.Size = new System.Drawing.Size(778, 578);
      this.uiComponents.TabIndex = 1;
      this.uiComponents.TabStop = false;
      // 
      // uiPlayPause
      // 
      this.uiPlayPause.Location = new System.Drawing.Point(12, 12);
      this.uiPlayPause.Name = "uiPlayPause";
      this.uiPlayPause.Size = new System.Drawing.Size(75, 23);
      this.uiPlayPause.TabIndex = 2;
      this.uiPlayPause.Text = "Play";
      this.uiPlayPause.UseVisualStyleBackColor = true;
      this.uiPlayPause.Click += new System.EventHandler(this.uiPlayPause_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(802, 631);
      this.Controls.Add(this.uiPlayPause);
      this.Controls.Add(this.uiComponents);
      this.Name = "MainForm";
      this.Text = "Loggella";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.GroupBox uiComponents;
    private System.Windows.Forms.Button uiPlayPause;
  }
}


namespace Loggella
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
      this.uiCircuitValues = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // uiCircuitValues
      // 
      this.uiCircuitValues.Location = new System.Drawing.Point(28, 33);
      this.uiCircuitValues.Multiline = true;
      this.uiCircuitValues.Name = "uiCircuitValues";
      this.uiCircuitValues.Size = new System.Drawing.Size(225, 151);
      this.uiCircuitValues.TabIndex = 0;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(284, 261);
      this.Controls.Add(this.uiCircuitValues);
      this.Name = "MainForm";
      this.Text = "Loggella";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox uiCircuitValues;
  }
}


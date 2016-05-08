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
      this.uiCircuitValues = new System.Windows.Forms.TextBox();
      this.uiComponents = new System.Windows.Forms.GroupBox();
      this.uiComponentsLayout = new System.Windows.Forms.FlowLayoutPanel();
      this.uiComponents.SuspendLayout();
      this.SuspendLayout();
      // 
      // uiCircuitValues
      // 
      this.uiCircuitValues.Location = new System.Drawing.Point(12, 12);
      this.uiCircuitValues.Multiline = true;
      this.uiCircuitValues.Name = "uiCircuitValues";
      this.uiCircuitValues.Size = new System.Drawing.Size(225, 151);
      this.uiCircuitValues.TabIndex = 0;
      // 
      // uiComponents
      // 
      this.uiComponents.Controls.Add(this.uiComponentsLayout);
      this.uiComponents.Location = new System.Drawing.Point(243, 12);
      this.uiComponents.Name = "uiComponents";
      this.uiComponents.Size = new System.Drawing.Size(547, 432);
      this.uiComponents.TabIndex = 1;
      this.uiComponents.TabStop = false;
      // 
      // uiComponentsLayout
      // 
      this.uiComponentsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
      this.uiComponentsLayout.Location = new System.Drawing.Point(3, 16);
      this.uiComponentsLayout.Name = "uiComponentsLayout";
      this.uiComponentsLayout.Size = new System.Drawing.Size(541, 413);
      this.uiComponentsLayout.TabIndex = 0;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(802, 456);
      this.Controls.Add(this.uiComponents);
      this.Controls.Add(this.uiCircuitValues);
      this.Name = "MainForm";
      this.Text = "Loggella";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
      this.uiComponents.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox uiCircuitValues;
    private System.Windows.Forms.GroupBox uiComponents;
    private System.Windows.Forms.FlowLayoutPanel uiComponentsLayout;
  }
}


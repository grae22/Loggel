namespace Loggella.UI
{
  partial class ComponentControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.uiName = new System.Windows.Forms.Label();
      this.uiType = new System.Windows.Forms.Label();
      this.uiValueInput = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // uiName
      // 
      this.uiName.AutoSize = true;
      this.uiName.Location = new System.Drawing.Point(3, 0);
      this.uiName.Name = "uiName";
      this.uiName.Size = new System.Drawing.Size(35, 13);
      this.uiName.TabIndex = 0;
      this.uiName.Text = "Name";
      // 
      // uiType
      // 
      this.uiType.AutoSize = true;
      this.uiType.Location = new System.Drawing.Point(3, 23);
      this.uiType.Name = "uiType";
      this.uiType.Size = new System.Drawing.Size(31, 13);
      this.uiType.TabIndex = 1;
      this.uiType.Text = "Type";
      // 
      // uiValueInput
      // 
      this.uiValueInput.Location = new System.Drawing.Point(98, 21);
      this.uiValueInput.Name = "uiValueInput";
      this.uiValueInput.Size = new System.Drawing.Size(38, 20);
      this.uiValueInput.TabIndex = 2;
      this.uiValueInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.uiValueInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.uiValueInput_KeyPress);
      // 
      // ComponentControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.uiValueInput);
      this.Controls.Add(this.uiType);
      this.Controls.Add(this.uiName);
      this.Name = "ComponentControl";
      this.Size = new System.Drawing.Size(134, 39);
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.ComponentControl_Paint);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label uiName;
    private System.Windows.Forms.Label uiType;
    private System.Windows.Forms.TextBox uiValueInput;
  }
}

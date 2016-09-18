namespace Loggella.UI
{
  partial class StoryControl
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
      this.label1 = new System.Windows.Forms.Label();
      this.uiName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.uiType = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.uiInitialValue = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(38, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Name:";
      // 
      // uiName
      // 
      this.uiName.Location = new System.Drawing.Point(44, 6);
      this.uiName.Name = "uiName";
      this.uiName.Size = new System.Drawing.Size(193, 20);
      this.uiName.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(253, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(34, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Type:";
      // 
      // uiType
      // 
      this.uiType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.uiType.FormattingEnabled = true;
      this.uiType.Items.AddRange(new object[] {
            "Value",
            "State"});
      this.uiType.Location = new System.Drawing.Point(293, 6);
      this.uiType.Name = "uiType";
      this.uiType.Size = new System.Drawing.Size(70, 21);
      this.uiType.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(380, 9);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(63, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Initial value:";
      // 
      // uiInitialValue
      // 
      this.uiInitialValue.FormattingEnabled = true;
      this.uiInitialValue.Items.AddRange(new object[] {
            "Value",
            "State"});
      this.uiInitialValue.Location = new System.Drawing.Point(449, 6);
      this.uiInitialValue.Name = "uiInitialValue";
      this.uiInitialValue.Size = new System.Drawing.Size(81, 21);
      this.uiInitialValue.TabIndex = 5;
      // 
      // StoryControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.uiInitialValue);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.uiType);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.uiName);
      this.Controls.Add(this.label1);
      this.Name = "StoryControl";
      this.Size = new System.Drawing.Size(542, 36);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox uiName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox uiType;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox uiInitialValue;
  }
}

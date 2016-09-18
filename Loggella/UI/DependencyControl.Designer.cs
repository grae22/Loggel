﻿namespace Loggella.UI
{
  partial class DependencyControl
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
      this.uiStories = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.uiCondition = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.uiCompisonValue = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 11);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "While";
      // 
      // uiStories
      // 
      this.uiStories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.uiStories.FormattingEnabled = true;
      this.uiStories.Location = new System.Drawing.Point(43, 8);
      this.uiStories.Name = "uiStories";
      this.uiStories.Size = new System.Drawing.Size(172, 21);
      this.uiStories.TabIndex = 1;
      this.uiStories.DropDown += new System.EventHandler(this.uiStories_DropDown);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(221, 11);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(14, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "is";
      // 
      // uiCondition
      // 
      this.uiCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.uiCondition.FormattingEnabled = true;
      this.uiCondition.Items.AddRange(new object[] {
            "=",
            "!=",
            ">",
            ">=",
            "<",
            "<="});
      this.uiCondition.Location = new System.Drawing.Point(241, 8);
      this.uiCondition.Name = "uiCondition";
      this.uiCondition.Size = new System.Drawing.Size(44, 21);
      this.uiCondition.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(291, 11);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(16, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "to";
      // 
      // uiCompisonValue
      // 
      this.uiCompisonValue.FormattingEnabled = true;
      this.uiCompisonValue.Items.AddRange(new object[] {
            "=",
            "!=",
            ">",
            ">=",
            "<",
            "<="});
      this.uiCompisonValue.Location = new System.Drawing.Point(313, 8);
      this.uiCompisonValue.Name = "uiCompisonValue";
      this.uiCompisonValue.Size = new System.Drawing.Size(69, 21);
      this.uiCompisonValue.TabIndex = 5;
      // 
      // DependencyControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.uiCompisonValue);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.uiCondition);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.uiStories);
      this.Controls.Add(this.label1);
      this.Name = "DependencyControl";
      this.Size = new System.Drawing.Size(596, 37);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox uiStories;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox uiCondition;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox uiCompisonValue;
  }
}

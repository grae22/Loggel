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
      this.uiPlayPause = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.uiComponents = new System.Windows.Forms.GroupBox();
      this.uiAddStory = new System.Windows.Forms.Button();
      this.uiStories = new System.Windows.Forms.FlowLayoutPanel();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
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
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(12, 41);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(778, 578);
      this.tabControl1.TabIndex = 3;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.uiStories);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(770, 552);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Stories";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.uiComponents);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(770, 552);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Circuit View";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // uiComponents
      // 
      this.uiComponents.Dock = System.Windows.Forms.DockStyle.Fill;
      this.uiComponents.Location = new System.Drawing.Point(3, 3);
      this.uiComponents.Name = "uiComponents";
      this.uiComponents.Size = new System.Drawing.Size(764, 546);
      this.uiComponents.TabIndex = 2;
      this.uiComponents.TabStop = false;
      // 
      // uiAddStory
      // 
      this.uiAddStory.Location = new System.Drawing.Point(93, 12);
      this.uiAddStory.Name = "uiAddStory";
      this.uiAddStory.Size = new System.Drawing.Size(75, 23);
      this.uiAddStory.TabIndex = 4;
      this.uiAddStory.Text = "Add Story";
      this.uiAddStory.UseVisualStyleBackColor = true;
      this.uiAddStory.Click += new System.EventHandler(this.uiAddStory_Click);
      // 
      // uiStories
      // 
      this.uiStories.AutoScroll = true;
      this.uiStories.Dock = System.Windows.Forms.DockStyle.Fill;
      this.uiStories.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.uiStories.Location = new System.Drawing.Point(3, 3);
      this.uiStories.Name = "uiStories";
      this.uiStories.Size = new System.Drawing.Size(764, 546);
      this.uiStories.TabIndex = 0;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(802, 631);
      this.Controls.Add(this.uiAddStory);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.uiPlayPause);
      this.Name = "MainForm";
      this.Text = "Loggella";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button uiPlayPause;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.GroupBox uiComponents;
    private System.Windows.Forms.Button uiAddStory;
    private System.Windows.Forms.FlowLayoutPanel uiStories;
  }
}


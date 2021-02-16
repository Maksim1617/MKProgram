
namespace CyclicCodes
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button_Back = new System.Windows.Forms.Button();
            this.button_Next = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Back
            // 
            this.button_Back.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button_Back.BackgroundImage = global::CyclicCodes.Properties.Resources.iconfinder_arrow_left_227602;
            this.button_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Back.Location = new System.Drawing.Point(12, 490);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(41, 47);
            this.button_Back.TabIndex = 0;
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // button_Next
            // 
            this.button_Next.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button_Next.BackgroundImage = global::CyclicCodes.Properties.Resources.iconfinder_arrow_right_227601;
            this.button_Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_Next.Location = new System.Drawing.Point(1847, 490);
            this.button_Next.Name = "button_Next";
            this.button_Next.Size = new System.Drawing.Size(41, 47);
            this.button_Next.TabIndex = 1;
            this.button_Next.UseVisualStyleBackColor = true;
            this.button_Next.Click += new System.EventHandler(this.button_Next_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1900, 1043);
            this.Controls.Add(this.button_Next);
            this.Controls.Add(this.button_Back);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1916, 1082);
            this.MinimumSize = new System.Drawing.Size(1916, 1082);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Принцип кодування та пошуку помилок кратності 1 та 2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Button button_Next;
    }
}
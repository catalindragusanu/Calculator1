namespace Calculator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Controls are built in Form1.cs (keypad grid), so the designer only sets the form itself.
        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 32, 32);
            ClientSize = new Size(340, 480);
            KeyPreview = true;
            MinimumSize = new Size(300, 420);
            Name = "Form1";
            Text = "Calculator";
            ResumeLayout(false);
        }
    }
}

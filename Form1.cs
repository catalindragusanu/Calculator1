using Calculator.Core;

namespace Calculator
{
    public partial class Form1 : Form
    {
        readonly CalculatorEngine engine = new();
        readonly Label display = new() { Name = "display" };
        readonly Label expression = new() { Name = "expression" };
        readonly Font displayFont = new("Segoe UI", 30F);
        readonly Font smallDisplayFont = new("Segoe UI", 16F);
        readonly Font expressionFont = new("Segoe UI", 11F);
        readonly Font buttonFont = new("Segoe UI", 16F);

        public Form1()
        {
            InitializeComponent();
            BuildKeypad();
            KeyPress += Form1_KeyPress;
            Disposed += (_, _) =>
            {
                displayFont.Dispose(); smallDisplayFont.Dispose();
                expressionFont.Dispose(); buttonFont.Dispose();
            };
        }

        void BuildKeypad()
        {
            expression.Dock = DockStyle.Top;
            expression.Height = 28;
            expression.Font = expressionFont;
            expression.ForeColor = Color.FromArgb(170, 170, 170);
            expression.TextAlign = ContentAlignment.BottomRight;
            expression.Padding = new Padding(0, 0, 12, 0);

            display.Dock = DockStyle.Top;
            display.Height = 80;
            display.Font = displayFont;
            display.ForeColor = Color.White;
            display.TextAlign = ContentAlignment.MiddleRight;
            display.Padding = new Padding(0, 0, 12, 0);
            display.AutoEllipsis = true;

            var grid = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(4) };
            for (int i = 0; i < 4; i++) grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            for (int i = 0; i < 5; i++) grid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            string[] keys =
            {
                "C", "⌫", "±", "/",
                "7", "8", "9", "*",
                "4", "5", "6", "-",
                "1", "2", "3", "+",
                "0", ".", "=",
            };
            for (int i = 0; i < keys.Length; i++)
            {
                var b = MakeButton(keys[i]);
                if (i < 16) grid.Controls.Add(b, i % 4, i / 4);
                else if (i == 16) { grid.Controls.Add(b, 0, 4); grid.SetColumnSpan(b, 2); }
                else grid.Controls.Add(b, i - 14, 4);
            }

            Controls.Add(grid);
            Controls.Add(display);
            Controls.Add(expression);
            Render();
        }

        Button MakeButton(string text)
        {
            bool isOp = "/*-+=".Contains(text);
            bool isDigit = char.IsDigit(text[0]) || text == ".";
            var b = new Button
            {
                Text = text == "/" ? "÷" : text == "*" ? "×" : text,
                Tag = text,
                AccessibleName = ButtonName(text),
                Dock = DockStyle.Fill,
                Margin = new Padding(3),
                Font = buttonFont,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                TabStop = false, // keeps Enter/Space for the calculator, not the focused button
                BackColor = text == "=" ? Color.FromArgb(0, 120, 212)
                          : isOp ? Color.FromArgb(70, 70, 70)
                          : isDigit ? Color.FromArgb(59, 59, 59) : Color.FromArgb(50, 50, 50),
            };
            b.FlatAppearance.BorderSize = 0;
            b.Click += (_, _) => Press((string)b.Tag!);
            return b;
        }

        static string ButtonName(string key) => key switch
        {
            "C" => "Clear",
            "⌫" => "Backspace",
            "±" => "Negate",
            "/" => "Divide",
            "*" => "Multiply",
            "-" => "Subtract",
            "+" => "Add",
            "=" => "Equals",
            "." => "Decimal point",
            _ => key,
        };

        void Form1_KeyPress(object? sender, KeyPressEventArgs e)
        {
            string? key = e.KeyChar switch
            {
                >= '0' and <= '9' => e.KeyChar.ToString(),
                '.' or ',' => ".",
                '+' or '-' or '*' or '/' => e.KeyChar.ToString(),
                '=' or '\r' => "=",
                '\b' => "⌫",
                (char)27 => "C",
                _ => null,
            };
            if (key != null) { Press(key); e.Handled = true; }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            string? key = keyData switch
            {
                Keys.Enter => "=",
                Keys.Escape or Keys.Delete => "C",
                Keys.Back => "⌫",
                _ => null,
            };
            if (key == null) return base.ProcessCmdKey(ref msg, keyData);
            Press(key);
            return true;
        }

        void Press(string key)
        {
            engine.Press(key);
            Render();
        }

        void Render()
        {
            display.Text = engine.Display;
            display.ForeColor = engine.HasError ? Color.FromArgb(255, 153, 153) : Color.White;
            display.Font = display.Text.Length > 12 ? smallDisplayFont : displayFont;
            expression.Text = engine.Expression;
        }
    }
}

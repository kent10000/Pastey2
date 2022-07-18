namespace Pastey2;

public partial class Form1 : Form
{
    //public
    private readonly TextBox _box; 
    private readonly Button _button;
    private bool _useTextBox;
    public Form1()
    {
        
        _box = new TextBox();
        _box.Multiline = true;
        _box.Dock = DockStyle.Fill;
        _box.Padding = new Padding(10);
        _box.ReadOnly = true;
        _box.Text = "Currently Pasting From Clipboard";
        
        _button = new Button();
        _button.MinimumSize = new Size(0, 40);
        _button.Dock = DockStyle.Bottom;
        _button.Click += button_Click;
        _button.Text = "Paste from textbox";
        _button.BackColor = Color.LightBlue;
        //_button.Padding = new Padding(10);
        
        Controls.AddRange(new Control[]{_box, _button});
        
        
        
        StartHook();
        InitializeComponent();
    }

    private void button_Click(object? sender, EventArgs e)
    {
        if (_useTextBox)
        {
            _useTextBox = false;
            _box.ReadOnly = true;
            _box.Text = "Currently Pasting From Clipboard";
            _button.Text = "Paste from textbox";
            _button.BackColor = Color.LightBlue;
        }
        else
        {
            _useTextBox = true;
            _box.ReadOnly = false;
            _box.Text = "";
            _box.PlaceholderText = "Enter text to paste";
            _button.Text = "Paste from clipboard";
            _button.BackColor = Color.LightGreen;
        }
    }

    private void smartPaste()
    {
        var text = Clipboard.GetText();
        SendKeys.Send(text);
    }
    
    private void smartPaste(string text)
    {
        
        SendKeys.Send(text);
    }
    
    private GlobalKeyboardHook? _globalKeyboardHook;
    
    private void StartHook()
    {
        // Hooks only into specified Keys (here "A" and "B").
        _globalKeyboardHook = new GlobalKeyboardHook(new[] { Keys.F8 });

        // Hooks into all keys.
        //_globalKeyboardHook = new GlobalKeyboardHook();
        _globalKeyboardHook.KeyboardPressed += OnKeyPressed!;
    }
    
    private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
    {
        if (e.KeyboardState != GlobalKeyboardHook.KeyboardState.KeyDown) return;
        if (_useTextBox)
        {
            smartPaste(_box.Text);
        }
        else
        {
            smartPaste();
        }
        e.Handled = true;
    }
}
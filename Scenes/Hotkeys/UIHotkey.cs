namespace GodotModules
{
    public class UIHotkey : Control
    {
        [Export] protected readonly NodePath NodePathLabel;
        [Export] protected readonly NodePath NodePathBtn;

        private Label _label;
        private Button _btn;
        private string _action;

        public override void _Ready()
        {
            _label = GetNode<Label>(NodePathLabel);
            _btn = GetNode<Button>(NodePathBtn);
        }

        public void SetAction(string v)
        {
            _action = v;
            _label.Text = v.Replace("_", " ").ToTitleCase().SmallWordsToUpper(2, (word) =>
            {
                var words = new string[] { "Up", "In" };
                return !words.Contains(word);
            });
        }

        public void SetBtnText(string v) => _btn.Text = v;

        private void _on_Btn_focus_exited()
        {
            
        }

        private void _on_Btn_pressed()
        {
            GD.Print(_label.Text);
        }
    }
}

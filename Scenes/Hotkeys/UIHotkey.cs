namespace GodotModules
{
    public class UIHotkey : Control
    {
        [Export] protected readonly NodePath NodePathLabel;
        [Export] protected readonly NodePath NodePathBtn;

        private Label _label;
        private Control _btnList;
        private Button _btn;

        public override void _Ready()
        {
            _label = GetNode<Label>(NodePathLabel);
            _btn = GetNode<Button>(NodePathBtn);
        }

        public void SetAction(string v) => _label.Text = v;
        public void SetBtnText(string v) => _btn.Text = v;

        private void _on_Btn_pressed()
        {
            GD.Print(_label.Text);
        }
    }
}

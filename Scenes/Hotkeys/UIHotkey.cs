namespace GodotModules
{
    public class UIHotkey : Control
    {
        [Export] protected readonly NodePath NodePathLabel;
        [Export] protected readonly NodePath NodePathBtnList;
        [Export] protected readonly NodePath NodePathBtnPlus;
        private Label _label;
        private Control _btnList;
        private Button _btnPlus;

        public override void _Ready()
        {
            _label = GetNode<Label>(NodePathLabel);
            _btnList = GetNode<Control>(NodePathBtnList);
            _btnPlus = GetNode<Button>(NodePathBtnPlus);
        }

        public void SetLabel(string v) => _label.Text = v;
        public void AddBtn(string v) 
        {
            var btn = new Button();
            btn.Text = v;
            _btnList.AddChild(btn);
        }
    }
}

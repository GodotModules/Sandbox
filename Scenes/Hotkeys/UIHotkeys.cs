namespace GodotModules
{
    public class UIHotkeys : Node
    {
        [Export] protected readonly NodePath NodePathHotkeyList;
        private Control _hotkeyList;

        public override void _Ready()
        {
            var hotkeyManager = new HotkeyManager();
            hotkeyManager.AddCategories("ui", "player", "camera");
            hotkeyManager.Init();

            _hotkeyList = GetNode<Control>(NodePathHotkeyList);

            foreach (var category in hotkeyManager.Categories)
            {
                var inputEventsCategory = hotkeyManager.DefaultInputEvents.Where(x => x.Value.Category == category).OrderBy(x => x.Key);

                foreach (var pair in inputEventsCategory)
                {
                    var uiHotkey = Prefabs.UIHotkey.Instance<UIHotkey>();
                    _hotkeyList.AddChild(uiHotkey);
                    uiHotkey.SetAction(pair.Key);
                    uiHotkey.SetBtnText(pair.Value.InputEventInfo[0].Display());
                }
            }
        }

        public override void _Input(InputEvent @event)
        {
            if (Input.IsActionJustPressed("player_move_left"))
            {
                GD.Print("moving left");
            }
        }
    }
}

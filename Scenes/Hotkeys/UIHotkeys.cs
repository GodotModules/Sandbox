namespace GodotModules
{
    public class UIHotkeys : Node
    {
        [Export] protected readonly NodePath NodePathHotkeyList;
        private Control _hotkeyList;
        private Dictionary<string, HotkeyInfo> _defaultInputEvents;

        public override void _Ready()
        {
            _defaultInputEvents = new Dictionary<string, HotkeyInfo>();
            _hotkeyList = GetNode<Control>(NodePathHotkeyList);

            // make sure all lists are defined
            foreach (string actionGroup in InputMap.GetActions())
            {
                var actions = InputMap.GetActionList(actionGroup);

                if (actions.Count == 0)
                    continue;

                _defaultInputEvents[actionGroup] = new HotkeyInfo {
                    Category = "ui",
                    InputEventInfo = new List<InputEventInfo>()
                };
            }

            LoadDefaults();
        }

        public void LoadDefaults()
        {
            foreach (string actionGroup in InputMap.GetActions())
            {
                var actions = InputMap.GetActionList(actionGroup);

                if (actions.Count == 0)
                    continue;

                var uiHotkey = Prefabs.UIHotkey.Instance<UIHotkey>();
                _hotkeyList.AddChild(uiHotkey);
                uiHotkey.SetLabel(actionGroup);

                var category = GetCategory(actionGroup);

                foreach (var action in actions)
                {
                    if (action is InputEvent inputEvent)
                    {
                        switch (inputEvent)
                        {
                            case InputEventKey k:
                                var e = _defaultInputEvents[actionGroup];
                                e.Category = category;
                                e.InputEventInfo.Add(ConvertToKeyInfo(k));
                                break;
                            case InputEventMouseButton m:
                                _defaultInputEvents[actionGroup].Add(ConvertToMouseButtonInfo(m));
                                break;
                            case InputEventJoypadButton j:
                                _defaultInputEvents[actionGroup].Add(ConvertToJoypadButtonInfo(j));
                                break;
                        }

                        uiHotkey.AddBtn(inputEvent.Display());
                    }
                }
            }
        }

        public string GetCategory(string actionGroup)
        {
            var categories = new string[] {"ui", "player", "camera" };
            foreach (var category in categories)
                if (actionGroup.ToLower() == category.ToLower())
                    return category.ToLower();

            return categories[0];
        }

        public InputEventKeyInfo ConvertToKeyInfo(InputEventKey e) =>
            new InputEventKeyInfo
            {
                InputEventInfoType = InputEventInfoType.Key,
                Alt = e.Alt,
                Command = e.Command,
                Control = e.Control,
                Device = e.Device,
                Echo = e.Echo,
                Meta = e.Meta,
                PhysicalScancode = e.PhysicalScancode,
                Pressed = e.Pressed,
                Scancode = e.Scancode,
                Shift = e.Shift,
                Unicode = e.Unicode
            };

        public InputEventMouseButtonInfo ConvertToMouseButtonInfo(InputEventMouseButton e) =>
            new InputEventMouseButtonInfo
            {
                InputEventInfoType = InputEventInfoType.MouseButton,
                Alt = e.Alt,
                Command = e.Command,
                Control = e.Control,
                Device = e.Device,
                Meta = e.Meta,
                Pressed = e.Pressed,
                Shift = e.Shift,
                ButtonIndex = e.ButtonIndex,
                ButtonMask = e.ButtonMask,
                DoubleClick = e.Doubleclick,
                Factor = e.Factor,
                GlobalPosition = e.GlobalPosition,
                Position = e.Position
            };

        public InputEventJoypadButtonInfo ConvertToJoypadButtonInfo(InputEventJoypadButton e) =>
            new InputEventJoypadButtonInfo
            {
                InputEventInfoType = InputEventInfoType.JoypadButton,
                Device = e.Device,
                Pressed = e.Pressed,
                ButtonIndex = e.ButtonIndex,
                Pressure = e.Pressure
            };
    }

    public class InputEventMouseButtonInfo : InputEventMouseInfo
    {
        public int ButtonIndex { get; set; }
        public bool DoubleClick { get; set; }
        public float Factor { get; set; }
        public bool Pressed { get; set; }
    }

    public class InputEventMouseInfo : InputEventWithModifiersInfo
    {
        public int ButtonMask { get; set; }
        public Vector2 GlobalPosition { get; set; }
        public Vector2 Position { get; set; }
    }

    public class InputEventKeyInfo : InputEventWithModifiersInfo
    {
        public bool Echo { get; set; }
        public uint PhysicalScancode { get; set; }
        public bool Pressed { get; set; }
        public uint Scancode { get; set; }
        public uint Unicode { get; set; }
    }

    public class InputEventWithModifiersInfo : InputEventInfo
    {
        public bool Alt { get; set; }
        public bool Command { get; set; }
        public bool Control { get; set; }
        public bool Meta { get; set; }
        public bool Shift { get; set; }
    }

    public class InputEventJoypadButtonInfo : InputEventInfo
    {
        public int ButtonIndex { get; set; }
        public bool Pressed { get; set; }
        public float Pressure { get; set; }
    }

    public class InputEventInfo
    {
        public InputEventInfoType InputEventInfoType { get; set; }
        public int Device { get; set; }
    }

    public class HotkeyInfo 
    {
        public string Category { get; set; }
        public List<InputEventInfo> InputEventInfo { get; set; }
    }

    public enum InputEventInfoType
    {
        Key,
        MouseButton,
        JoypadButton
    }
}

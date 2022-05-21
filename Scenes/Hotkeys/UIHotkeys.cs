namespace GodotModules
{
    public class UIHotkeys : Node
    {
        [Export] protected readonly NodePath NodePathHotkeyList;
        private Control _hotkeyList;
        private Dictionary<string, HotkeyInfo> _defaultInputEvents;
        private Dictionary<string, HotkeyInfo> _persistentInputEvents;

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

                _defaultInputEvents[actionGroup] = new HotkeyInfo
                {
                    Category = "ui",
                    InputEventInfo = new List<InputEventInfo>()
                };
            }

            LoadDefaults();

            _persistentInputEvents = DeepCopy(_defaultInputEvents);
        }

        public Dictionary<string, HotkeyInfo> DeepCopy(Dictionary<string, HotkeyInfo> data)
        {
            var dict = new Dictionary<string, HotkeyInfo>(data);
            foreach (var e in dict)
                e.Value.InputEventInfo = new List<InputEventInfo>(data[e.Key].InputEventInfo);
            return dict;
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
                _defaultInputEvents[actionGroup].Category = category;

                foreach (var action in actions)
                {
                    if (action is InputEvent inputEvent)
                    {
                        if (inputEvent is InputEventKey e)
                            _defaultInputEvents[actionGroup].InputEventInfo.Add(Convert(e));

                        if (inputEvent is InputEventMouseButton m)
                            _defaultInputEvents[actionGroup].InputEventInfo.Add(Convert(m));

                        if (inputEvent is InputEventJoypadButton j)
                            _defaultInputEvents[actionGroup].InputEventInfo.Add(Convert(j));

                        uiHotkey.AddBtn(inputEvent.Display());
                    }
                }
            }
        }

        public string GetCategory(string actionGroup)
        {
            var categories = new string[] { "ui", "player", "camera" };
            foreach (var category in categories)
                if (actionGroup.ToLower() == category.ToLower())
                    return category.ToLower();

            return categories[0];
        }

        public InputEventInfo Convert(InputEvent e)
        {
            switch (e)
            {
                case InputEventKey k:
                    return new InputEventInfo
                    {
                        InputEventInfoType = InputEventInfoType.Key,
                        Alt = k.Alt,
                        Command = k.Command,
                        Control = k.Control,
                        Device = k.Device,
                        Echo = k.Echo,
                        Meta = k.Meta,
                        PhysicalScancode = k.PhysicalScancode,
                        Pressed = k.Pressed,
                        Scancode = k.Scancode,
                        Shift = k.Shift,
                        Unicode = k.Unicode
                    };
                case InputEventMouseButton m:
                    return new InputEventInfo
                    {
                        InputEventInfoType = InputEventInfoType.MouseButton,
                        Alt = m.Alt,
                        Command = m.Command,
                        Control = m.Control,
                        Device = m.Device,
                        Meta = m.Meta,
                        Pressed = m.Pressed,
                        Shift = m.Shift,
                        ButtonIndex = m.ButtonIndex,
                        ButtonMask = m.ButtonMask,
                        DoubleClick = m.Doubleclick,
                        Factor = m.Factor,
                    };
                case InputEventJoypadButton j:
                    return new InputEventInfo
                    {
                        InputEventInfoType = InputEventInfoType.JoypadButton,
                        Device = j.Device,
                        Pressed = j.Pressed,
                        ButtonIndex = j.ButtonIndex,
                        Pressure = j.Pressure
                    };
            }

            throw new InvalidOperationException("This InputEventInfo is not a Key, MouseButton or JoypadButton event.");
        }
    }

    public record struct InputEventInfo
    {
        public InputEventInfoType InputEventInfoType { get; set; }
        public int Device { get; set; }
        public int ButtonIndex { get; set; }
        public bool Pressed { get; set; }
        public float Pressure { get; set; }
        public bool Alt { get; set; }
        public bool Command { get; set; }
        public bool Control { get; set; }
        public bool Meta { get; set; }
        public bool Shift { get; set; }
        public bool Echo { get; set; }
        public uint PhysicalScancode { get; set; }
        public uint Scancode { get; set; }
        public uint Unicode { get; set; }
        public int ButtonMask { get; set; }
        public bool DoubleClick { get; set; }
        public float Factor { get; set; }
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

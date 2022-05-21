namespace GodotModules
{
    public class UIHotkeys : Node
    {
        [Export] protected readonly NodePath NodePathHotkeyList;
        private Control _hotkeyList;
        private Dictionary<string, List<InputEventInfo>> _inputEvents;

        public override void _Ready()
        {
            _inputEvents = new Dictionary<string, List<InputEventInfo>>();
            _hotkeyList = GetNode<Control>(NodePathHotkeyList);
            
            // make sure all lists are defined
            foreach (string actionGroup in InputMap.GetActions()) 
            {
                var actions = InputMap.GetActionList(actionGroup);

                if (actions.Count == 0)
                    continue;

                _inputEvents[actionGroup] = new List<InputEventInfo>();
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

                foreach (var action in actions)
                {
                    if (action is InputEvent inputEvent)
                    {
                        if (inputEvent is InputEventKey k)
                        {
                            _inputEvents[actionGroup].Add(
                                new InputEventKeyInfo {
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
                                }
                            );
                        }

                        if (inputEvent is InputEventMouseButton m)
                        {
                            _inputEvents[actionGroup].Add(
                                new InputEventMouseButtonInfo {
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
                                    GlobalPosition = m.GlobalPosition,
                                    Position = m.Position
                                }
                            );
                        }

                        if (inputEvent is InputEventJoypadButton j)
                        {
                            _inputEvents[actionGroup].Add(
                                new InputEventJoypadButtonInfo {
                                    InputEventInfoType = InputEventInfoType.JoypadButton,
                                    Device = j.Device,
                                    Pressed = j.Pressed,
                                    ButtonIndex = j.ButtonIndex,
                                    Pressure = j.Pressure
                                }
                            );
                        }

                        uiHotkey.AddBtn(inputEvent.AsText());
                    }
                }
            }
        }
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

    public enum InputEventInfoType 
    {
        Key,
        MouseButton,
        JoypadButton
    }
}

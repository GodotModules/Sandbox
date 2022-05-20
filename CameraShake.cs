using Godot;
using System;

public class CameraShake : Camera2D
{
    private GTimer _timerDuration;
    private GTimer _timerFrequency;
    private float _amplitudeX = 5;
    private float _amplitudeY = 5;
    private int _duration = 500;
    private int _frequency = 50;

    public override void _Ready()
    {
        _timerDuration = new GTimer(this, nameof(StopProcess), _duration, false, false);
        _timerFrequency = new GTimer(this, nameof(Shake), _frequency, true, false);
        SetPhysicsProcess(false);
    }

    private void _on_Amplitude_X_text_changed(string text)
    {
        if (int.TryParse(text, out int result))
            _amplitudeX = result;
    }

    private void _on_Amplitude_Y_text_changed(string text)
    {
        if (int.TryParse(text, out int result))
            _amplitudeY = result;
    }

    private void _on_Duration_text_changed(string text)
    {
        if (int.TryParse(text, out int result))
            _duration = result;
    }

    private void _on_Frequency_text_changed(string text)
    {
        if (int.TryParse(text, out int result))
            _frequency = result;
    }

    private void Shake()
    {
        var randomX = GD.Randf() - 0.5f;
        var randomY = GD.Randf() - 0.5f;

        Offset = new Vector2(randomX * _amplitudeX, randomY * _amplitudeY);
    }

    public override void _PhysicsProcess(float delta)
    {
        
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("ui_accept")) 
        {
            _timerDuration.StartMs(_duration);
            _timerFrequency.StartMs(_frequency);
            SetPhysicsProcess(true);
        }
    }

    private void StopProcess()
    {
        SetPhysicsProcess(false);
        _timerFrequency.Stop();
        Offset = Vector2.Zero;
    }
}

public class GTimer
{
    private readonly Timer _timer = new Timer();

    public float Delay { get; set; }

    public GTimer(Node target, int delayMs = 1000, bool loop = false, bool autoStart = false) =>
        Init(target, delayMs, loop, autoStart);

    public GTimer(Node target, string methodName, int delayMs = 1000, bool loop = true, bool autoStart = true)
    {
        Init(target, delayMs, loop, autoStart);
        _timer.Connect("timeout", target, methodName);
    }

    private void Init(Node target, int delayMs, bool loop, bool autoStart)
    {
        _timer.WaitTime = delayMs / 1000f;
        Delay = _timer.WaitTime;
        _timer.OneShot = !loop;
        _timer.Autostart = autoStart;
        target.AddChild(_timer);
    }

    public bool IsActive() => _timer.TimeLeft != 0;
    public void SetDelay(float delay) => _timer.WaitTime = delay;
    public void SetDelayMs(int delayMs) => _timer.WaitTime = delayMs / 1000f;

    public void Start(float delay)
    {
        _timer.WaitTime = delay;
        Start();
    }
    public void StartMs(float delayMs)
    {
        _timer.WaitTime = delayMs / 1000;
        Start();
    }

    public void Start() => _timer.Start();
    public void Stop() => _timer.Stop();
    public void QueueFree() => _timer.QueueFree();
}

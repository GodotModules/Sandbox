global using Godot;
global using System;
global using System.Collections.Generic;

namespace GodotModules
{
    public class CameraShake : Camera2D
    {
        private GTimer _timerDuration;
        private GTimer _timerFrequency;
        private ShakeInfo _currentShake;
        private ShakeInfo _testShake;

        public override void _Ready()
        {
            _currentShake = new(){
                AmplitudeX = 7,
                AmplitudeY = 5,
                Duration = 200,
                Frequency = 40,
                Priority = 0
            };
            _testShake = _currentShake;
            _timerDuration = new GTimer(this, nameof(StopShake), _currentShake.Duration, false, false);
            _timerFrequency = new GTimer(this, nameof(SimulateShake), _currentShake.Frequency, true, false);
        }

        public override void _Input(InputEvent @event)
        {
            if (Input.IsActionJustPressed("ui_accept"))
            {
                StartShake(_testShake);
            }
        }

        private void _on_Amplitude_X_text_changed(string text)
        {
            if (int.TryParse(text, out int result))
                _testShake.AmplitudeX = result;
        }

        private void _on_Amplitude_Y_text_changed(string text)
        {
            if (int.TryParse(text, out int result))
                _testShake.AmplitudeY = result;
        }

        private void _on_Duration_text_changed(string text)
        {
            if (int.TryParse(text, out int result))
                _testShake.Duration = result;
        }

        private void _on_Frequency_text_changed(string text)
        {
            if (int.TryParse(text, out int result))
                _testShake.Frequency = result;
        }

        private void SimulateShake() =>
            Offset = new Vector2((GD.Randf() - 0.5f) * _currentShake.AmplitudeX, (GD.Randf() - 0.5f) * _currentShake.AmplitudeY);

        private void StartShake(ShakeInfo shakeInfo)
        {
            if (shakeInfo.Priority < _currentShake.Priority)
                return;

            _timerDuration.StartMs(shakeInfo.Duration);
            _timerFrequency.StartMs(shakeInfo.Frequency);

            _currentShake = shakeInfo;
        }

        private void StopShake()
        {
            _timerFrequency.Stop();
            _currentShake = default;
            Offset = Vector2.Zero;
        }
    }

    public struct ShakeInfo
    {
        public float AmplitudeX { get; set; }
        public float AmplitudeY { get; set; }
        public int Duration { get; set; }
        public int Frequency { get; set; }
        public int Priority { get; set; }
    }
}

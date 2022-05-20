global using Godot;
global using System;

namespace GodotModules
{
    public class CameraShake : Camera2D
    {
        private GTimer _timerDuration;
        private GTimer _timerFrequency;
        private float _amplitudeX = 7;
        private float _amplitudeY = 5;
        private int _duration = 200;
        private int _frequency = 40;

        public override void _Ready()
        {
            _timerDuration = new GTimer(this, nameof(StopShake), _duration, false, false);
            _timerFrequency = new GTimer(this, nameof(SimulateShake), _frequency, true, false);
        }

        public override void _Input(InputEvent @event)
        {
            if (Input.IsActionJustPressed("ui_accept"))
            {
                StartShake(_amplitudeX, _amplitudeY, _duration, _frequency);
            }
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

        private void SimulateShake()
        {
            var randomX = GD.Randf() - 0.5f;
            var randomY = GD.Randf() - 0.5f;

            Offset = new Vector2(randomX * _amplitudeX, randomY * _amplitudeY);
        }

        private void StartShake(float amplitudeX = 7, float amplitudeY = 5, int duration = 200, int frequency = 40)
        {
            _amplitudeX = amplitudeX;
            _amplitudeY = amplitudeY;
            _duration = duration;
            _frequency = frequency;

            _timerDuration.StartMs(_duration);
            _timerFrequency.StartMs(_frequency);
        }

        private void StopShake()
        {
            _timerFrequency.Stop();
            Offset = Vector2.Zero;
        }
    }
}

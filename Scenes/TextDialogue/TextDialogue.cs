using Godot;
using System;

namespace GodotModules
{
    public class TextDialogue : RichTextLabel
    {
        private GTimer _timer;
        private int _visibleChars;

        public override void _Ready()
        {
            _timer = new GTimer(this, nameof(RevealNextChar), 50, true, false);
            Text = "The red fox jumped over the fence. This was a great thing to see.";
            VisibleCharacters = 0;
            StartRevealing(50);
        }

        public void StartRevealing(int speed) => _timer.StartMs(speed);

        private void RevealNextChar()
        {
            VisibleCharacters = _visibleChars++;

            if (VisibleCharacters == Text.Length)
            {
                _timer.Stop();
                GD.Print("Finished");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ctris {
    public static class InputManager {

        private static ISet<Keys> lastPressedKeys = new HashSet<Keys>();

        public static void Update(Piece piece) {
            var state = Keyboard.GetState();
            var pressed = state.GetPressedKeys();
            foreach (var key in pressed) {
                if (!lastPressedKeys.Contains(key))
                    OnKeyPressed(key, piece);
            }
            lastPressedKeys.Clear();
            foreach (var key in pressed)
                lastPressedKeys.Add(key);
        }

        public static void OnKeyPressed(Keys key, Piece piece) {
            if (key == Keys.Left)
                piece.Move(new Point(-1, 0), -1);
            if(key == Keys.Right)
                piece.Move(new Point(1,0), 1);
            if(key == Keys.Down)
                piece.Move(new Point(0,1), 0);
        }

    }
}
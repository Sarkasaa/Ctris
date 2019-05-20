using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ctris {
    public static class InputManager {

        private static ISet<Keys> lastPressedKeys = new HashSet<Keys>();

        public static void Update(Board board) {
            var state = Keyboard.GetState();
            var pressed = state.GetPressedKeys();
            foreach (var key in pressed) {
                if (!lastPressedKeys.Contains(key))
                    OnKeyPressed(key, board);
            }
            lastPressedKeys.Clear();
            foreach (var key in pressed)
                lastPressedKeys.Add(key);
        }

        private static void OnKeyPressed(Keys key, Board board) {
            var piece = board.CurrPiece;
            if (!GameImpl.IsPaused) {
                if (key == Keys.Left)
                    piece.Move(new Point(-1, 0));
                if (key == Keys.Right)
                    piece.Move(new Point(1, 0));
                if (key == Keys.Down)
                    piece.Move(new Point(0, 1));
                if (key == Keys.Q) {
                    if (piece.CanRotate(false)) {
                        if (piece.PieceRot > 0)
                            piece.PieceRot--;
                        else piece.PieceRot = 3;
                        piece.RotatePieceCCW();
                        board.GhostPiece.RotatePieceCCW();
                    }
                }
                if (key == Keys.E) {
                    if (piece.CanRotate(true)) {
                        if (piece.PieceRot < 3)
                            piece.PieceRot++;
                        else piece.PieceRot = 0;
                        piece.RotatePieceCW();
                        board.GhostPiece.RotatePieceCW();
                    }
                }
                if (key == Keys.Space) {
                    while (piece.CanMove(new Point(0, 1)))
                        piece.Move(new Point(0, 1));
                    GameImpl.instance.Board.PieceToMap();
                }
            }
            if (key == Keys.Escape) {
                GameImpl.IsPaused = !GameImpl.IsPaused;
            }
            board.GhostPiece.PositionGhost();
        }

    }
}
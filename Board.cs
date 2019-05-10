using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ctris {
    public class Board {

        public const int Height = 21;
        public const int Width = 10;
        public const float Scale = 30;

        public Piece currPiece;
        private Piece nextPiece;

        private Color[,] map;

        public Board() {
            this.map = new Color[Width, Height];
            this.currPiece = new Piece(PieceType.I);
        }


        public void PieceToMap() {
            var offset = this.currPiece.Width == 3 ? new Point(1, 1) : new Point(2, 2);
            var zeroPos = this.currPiece.CurrPos - offset;
            var size = this.currPiece.Width;
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    if (this.currPiece.tiles[y, x] == 1) {
                        this.map[zeroPos.X + x, zeroPos.Y + y] = this.currPiece.Color;
                    }
                }
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch batch, Viewport screen) {
            var matrix = Matrix.CreateScale(Scale) * Matrix.CreateTranslation(screen.Width / 2 - Width * Scale / 2, 0, 0);
            batch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: matrix);
            this.currPiece.Draw(batch);
            for (var x = 0; x < Width; x++) {
                for (var y = 0; y < Height; y++) {
                    batch.FillRectangle(new Vector2(x, y), new Size2(1, 1), this.map[x, y]);
                    batch.DrawRectangle(new Vector2(x, y), new Size2(1, 1), Color.Black, 1 / 16F);
                }
            }
            batch.End();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collections;

namespace Ctris {
    public class Board {

        public const int Height = 21;
        public const int Width = 10;
        public const float Scale = 30;


        public Queue<PieceType> queue = new Queue<PieceType>();
        public Piece currPiece;
        private Piece nextPiece;

        public Color[,] Map;

        public Board() {
            this.Map = new Color[Width, Height];
            this.currPiece = new Piece(this.GetNextPiece());
        }


        private PieceType GetNextPiece() {
            if (this.queue.Count == 0) {
                var bag = new List<PieceType>(Enum.GetValues(typeof(PieceType)).Cast<PieceType>());
                bag.Shuffle(new Random());
                foreach (var type in bag) {
                    this.queue.Enqueue(type);
                }
            }

            return this.queue.Dequeue();
        }


        public void PieceToMap() {
            var offset = this.currPiece.Width == 3 ? new Point(1, 1) : new Point(2, 2);
            var zeroPos = this.currPiece.CurrPos - offset;
            var size = this.currPiece.Width;
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    if (this.currPiece.Tiles[y, x] == 1) {
                        this.Map[zeroPos.X + x, zeroPos.Y + y] = this.currPiece.Color;
                    }
                }
            }
            this.currPiece = new Piece(this.GetNextPiece());
        }


        public void Draw(GameTime gameTime, SpriteBatch batch, Viewport screen) {
            var matrix = Matrix.CreateScale(Scale) * Matrix.CreateTranslation(screen.Width / 2 - Width * Scale / 2, 0, 0);
            batch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: matrix);
            this.currPiece.Draw(batch);
            for (var x = 0; x < Width; x++) {
                for (var y = 0; y < Height; y++) {
                    batch.FillRectangle(new Vector2(x, y), new Size2(1, 1), this.Map[x, y]);
                    batch.DrawRectangle(new Vector2(x, y), new Size2(1, 1), Color.Black, 1 / 16F);
                }
            }
            batch.End();
        }

    }
}
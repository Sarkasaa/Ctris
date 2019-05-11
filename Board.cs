using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collections;

namespace Ctris {
    public class Board {

        public const int Height = 23;
        public const int Width = 10;
        public const float Scale = 30;


        public Queue<PieceType> queue = new Queue<PieceType>();
        public Piece CurrPiece;
        private Piece nextPiece;

        public Color[,] Map;

        public Board() {
            this.Map = new Color[Width, Height];
            this.CurrPiece = new Piece(this.GetNextPiece(), this);
            if (this.CurrPiece.CanMove(new Point(0, 1)))
                this.CurrPiece.Move(new Point(0, 1));
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
            var offset = this.CurrPiece.Width == 3 ? new Point(1, 1) : new Point(2, 2);
            var zeroPos = this.CurrPiece.CurrPos - offset;
            var size = this.CurrPiece.Width;
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    if (this.CurrPiece.Tiles[y, x] == 1) {
                        this.Map[zeroPos.X + x, zeroPos.Y + y] = this.CurrPiece.Color;
                    }
                }
            }
            this.ClearLines();
            this.CurrPiece = new Piece(this.GetNextPiece(), this);
            if (this.CurrPiece.CanMove(new Point(0, 1)))
                this.CurrPiece.Move(new Point(0, 1));
            else {
                GameImpl.instance.Board = new Board();
            }
        }

        public void ClearLines() {
            for (var y = 0; y < Height; y++) {
                for (var x = 0; x < Width; x++) {
                    if (this.Map[x, y] == Color.Transparent)
                        goto afterXCheck;
                }

                for (var moveY = y - 1; moveY >= 0; moveY--) {
                    for (var x = 0; x < Width; x++) {
                        this.Map[x, moveY + 1] = this.Map[x, moveY];
                    }
                }

                afterXCheck: ;
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch batch, Viewport screen) {
            var matrix = Matrix.CreateScale(Scale) * Matrix.CreateTranslation(screen.Width / 2 - Width * Scale / 2, 0, 0);
            batch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: matrix);
            this.CurrPiece.Draw(batch);
            for (var x = 0; x < Width; x++) {
                for (var y = 3; y < Height; y++) {
                    batch.FillRectangle(new Vector2(x, y - 3), new Size2(1, 1), this.Map[x, y]);
                    batch.DrawRectangle(new Vector2(x, y - 3), new Size2(1, 1), Color.Black, 1 / 32F);
                }
            }
            batch.End();
        }

    }
}
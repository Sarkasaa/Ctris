using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ctris {
    public class Piece {


        private int[,] tiles;
        private Color color;
        private int pieceRot;
        private PieceType pieceType;

        public Point CurrPos;
        public int Width => this.tiles.GetLength(1);
        public int Height => this.tiles.GetLength(0);

        public Piece(PieceType pieceType) {
            this.pieceType = pieceType;
            if (pieceType == PieceType.O) {
                this.CurrPos = new Point(Board.Width / 2, 0);
                this.color = Color.Yellow;
                this.tiles = new[,] {
                    {1, 1},
                    {1, 1}
                };
            } else if (pieceType == PieceType.I) {
                this.CurrPos = new Point(Board.Width / 2, 0);
                this.color = Color.Aqua;
                this.tiles = new[,] {
                    {0, 0, 0, 0},
                    {1, 1, 1, 1},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0}
                };
            } else if (pieceType == PieceType.T) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.color = Color.Purple;
                this.tiles = new[,] {
                    {0, 1, 0},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.J) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.color = Color.Blue;
                this.tiles = new[,] {
                    {1, 0, 0},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.L) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.color = Color.Orange;
                this.tiles = new[,] {
                    {0, 0, 1},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.S) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.color = Color.Green;
                this.tiles = new[,] {
                    {0, 1, 1},
                    {1, 1, 0},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.Z) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.color = Color.Red;
                this.tiles = new[,] {
                    {1, 1, 0},
                    {0, 1, 1},
                    {0, 0, 0}
                };
            }
        }

        public int PieceBias() {
            if (this.Width == 3) {
                if (this.pieceRot == 0 || this.pieceRot == 2) {
                    return 0;
                } else if (this.pieceRot == 1) {
                    return 1;
                } else if (this.pieceRot == 3)
                    return -1;
            } else if (this.pieceType == PieceType.O) {
                return 0;
            } else if (this.pieceType == PieceType.I) {
                if (this.pieceRot == 0 || this.pieceRot == 2) {
                    return 0;
                } else if (this.pieceRot == 1) {
                    return 1;
                } else if (this.pieceRot == 3)
                    return -1;
            }
            return 0;
        }

        public bool CanMove() {
            if (this.CurrPos.X > 10) {
                var mostRightX = this.CurrPos.X + this.PieceBias();
                if (mostRightX <= 2)
                    return true;
            } else if (this.CurrPos.X <= 10) {
                var mostLeftX = this.CurrPos.X 
            }
        }
        
        public void Move(Point offset) {
            var halfWidth = (int) Math.Ceiling(this.tiles.GetLength(1) / 2F);
            
            this.CurrPos += offset;
            Console.WriteLine(this.CurrPos);
        }
        
        

        public void Draw(SpriteBatch batch) {
            var renderPos = (this.CurrPos - new Point(this.Width / 2, this.Height / 2)).ToVector2();
            for (var y = 0; y < this.Height; y++) {
                for (var x = 0; x < this.Width; x++) {
                    if (this.tiles[y, x] == 1)
                        batch.FillRectangle(renderPos + new Vector2(x, y), new Size2(1, 1), this.color);
                }
            }
        }

    }

    public enum PieceType {

        O,
        I,
        T,
        L,
        J,
        S,
        Z

    }
}
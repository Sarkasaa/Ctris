using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ctris {
    public class Piece {

        private int counter;
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

        public int[,] RotatePiece(int[,] tiles) {
            var pieceRot = this.pieceRot;
            if (this.pieceRot == 0) {
                return tiles;
            }
            var result = new int[Width, Width];
            for (var rot = 0; rot < pieceRot; rot++) {
                for (var x = 0; x < Width; x++) {
                    for (var y = 0; y < Width; y++) {
                        var newX = y;
                        var newY = Width - (x + 1);
                        result[newX, newY] = tiles[y, x];
                    }
                }
            }
            return result;
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

        private bool CanMove(int direction) {
            var halfWidthLeft = (int) Math.Floor(this.Width / 2F);
            var halfWidthRight = (int) Math.Ceiling(this.Width / 2F);
            var pieceBias = this.PieceBias();

            if (pieceBias != 0)
                return this.CurrPos.X + pieceBias * halfWidthRight < Board.Width;
            if (this.CurrPos.X + halfWidthRight >= Board.Width && direction == 1)
                return false;
            if (this.CurrPos.X - halfWidthLeft < 1 && direction == -1)
                return false;
            return true;
        }


        public void Move(Point offset, int direction) {
            if (!this.CanMove(direction)) {
                /**counter++;                DEBUG
                Console.WriteLine("move prevented " + counter);**/
                return;
            }
            this.CurrPos += offset;
            //counter = 0;                    DEBUG
            Console.WriteLine(this.CurrPos);
        }


        public void Draw(SpriteBatch batch) {
            var renderPos = (this.CurrPos - new Point(this.Width / 2, this.Height / 2)).ToVector2();
            for (var y = 0; y < this.Height; y++) {
                for (var x = 0; x < this.Width; x++) {
                    if (this.RotatePiece(this.tiles)[y, x] == 1)
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
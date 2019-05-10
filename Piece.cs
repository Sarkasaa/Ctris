using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Ctris {
    public class Piece {

        //private int counter;
        public int[,] tiles;
        public Color Color;
        private PieceType pieceType;

        public int PieceRot = 0;
        public Point CurrPos;
        public int Width => this.tiles.GetLength(1);
        public int Height => this.tiles.GetLength(0);

        public Piece(PieceType pieceType) {
            this.pieceType = pieceType;
            if (pieceType == PieceType.O) {
                this.CurrPos = new Point(Board.Width / 2, 0);
                this.Color = Color.Yellow;
                this.tiles = new[,] {
                    {0, 0, 0, 0},
                    {0, 1, 1, 0},
                    {0, 1, 1, 0},
                    {0, 0, 0, 0}
                };
            } else if (pieceType == PieceType.I) {
                this.CurrPos = new Point(Board.Width / 2, 0);
                this.Color = Color.Aqua;
                this.tiles = new[,] {
                    {0, 0, 0, 0},
                    {1, 1, 1, 1},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0}
                };
            } else if (pieceType == PieceType.T) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.Color = Color.Purple;
                this.tiles = new[,] {
                    {0, 1, 0},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.J) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.Color = Color.Blue;
                this.tiles = new[,] {
                    {1, 0, 0},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.L) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.Color = Color.Orange;
                this.tiles = new[,] {
                    {0, 0, 1},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.S) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.Color = Color.Green;
                this.tiles = new[,] {
                    {0, 1, 1},
                    {1, 1, 0},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.Z) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 0);
                this.Color = Color.Red;
                this.tiles = new[,] {
                    {1, 1, 0},
                    {0, 1, 1},
                    {0, 0, 0}
                };
            }
        }


        public bool CanRotate(bool clockwise) {
            var result = new int[this.Width, this.Width];
            if (!clockwise) {
                //CCW
                for (var x = 0; x < this.Width; x++) {
                    for (var y = 0; y < this.Width; y++) {
                        var newX = y;
                        var newY = this.Width - (x + 1);
                        result[newY, newX] = this.tiles[y, x];
                    }
                }
            } else {
                //CW
                for (var x = 0; x < this.Width; x++) {
                    for (var y = 0; y < this.Width; y++) {
                        var newX = this.Width - (y + 1);
                        var newY = x;
                        result[newY, newX] = this.tiles[y, x];
                    }
                }
            }

            var offset = this.Width == 3 ? new Point(1, 1) : new Point(2, 2);
            var zeroPos = this.CurrPos - offset;
            var size = this.Width;
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    if (result[y, x] == 1) {
                        if (zeroPos.X + x < 0 || zeroPos.X + x >= Board.Width || zeroPos.Y + y >= Board.Height)
                            return false;
                    }
                }
            }
            return true;
        }

        public void RotatePieceCCW() {
            var result = new int[this.Width, this.Width];
            for (var x = 0; x < this.Width; x++) {
                for (var y = 0; y < this.Width; y++) {
                    var newX = y;
                    var newY = this.Width - (x + 1);
                    result[newY, newX] = this.tiles[y, x];
                }
            }

            this.tiles = result;
        }

        public void RotatePieceCW() {
            var result = new int[this.Width, this.Width];
            for (var x = 0; x < this.Width; x++) {
                for (var y = 0; y < this.Width; y++) {
                    var newX = this.Width - (y + 1);
                    var newY = x;
                    result[newY, newX] = this.tiles[y, x];
                }
            }

            this.tiles = result;
        }


        private bool CanMove(Point move) {
            var offset = this.Width == 3 ? new Point(1, 1) : new Point(2, 2);
            var zeroPos = this.CurrPos - offset + move;
            var size = this.Width;
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    if (this.tiles[y, x] == 1) {
                        if (zeroPos.X + x < 0 || zeroPos.X + x >= Board.Width || zeroPos.Y + y >= Board.Height)
                            return false;
                    }
                }
            }
            return true;
        }


        public void Move(Point offset) {
            if (!this.CanMove(offset)) {
                return;
            }
            this.CurrPos += offset;
        }


        public void Draw(SpriteBatch batch) {
            var renderPos = (this.CurrPos - new Point(this.Width / 2, this.Height / 2)).ToVector2();
            for (var y = 0; y < this.Height; y++) {
                for (var x = 0; x < this.Width; x++) {
                    if (this.tiles[y, x] == 1)
                        batch.FillRectangle(renderPos + new Vector2(x, y), new Size2(1, 1), this.Color);
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
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collections;

namespace Ctris {
    public class Piece {

        public Board board;
        public Color Color;
        public int[,] Tiles;
        public int PieceRot = 0;
        public Point CurrPos;
        public int Width => this.Tiles.GetLength(1);
        public int Height => this.Tiles.GetLength(0);
        public Point Offset => this.Width == 3 ? new Point(1, 1) : new Point(2, 2);

        public Piece(PieceType pieceType, Board board) {
            this.board = board;
            if (pieceType == PieceType.O) {
                this.CurrPos = new Point(Board.Width / 2, 2);
                this.Color = Color.Yellow;
                this.Tiles = new[,] {
                    {0, 0, 0, 0},
                    {0, 1, 1, 0},
                    {0, 1, 1, 0},
                    {0, 0, 0, 0}
                };
            } else if (pieceType == PieceType.I) {
                this.CurrPos = new Point(Board.Width / 2, 3);
                this.Color = Color.Cyan;
                this.Tiles = new[,] {
                    {0, 0, 0, 0},
                    {1, 1, 1, 1},
                    {0, 0, 0, 0},
                    {0, 0, 0, 0}
                };
            } else if (pieceType == PieceType.T) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 2);
                this.Color = Color.Purple;
                this.Tiles = new[,] {
                    {0, 1, 0},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.J) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 2);
                this.Color = Color.Blue;
                this.Tiles = new[,] {
                    {1, 0, 0},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.L) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 2);
                this.Color = Color.Orange;
                this.Tiles = new[,] {
                    {0, 0, 1},
                    {1, 1, 1},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.S) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 2);
                this.Color = Color.Green;
                this.Tiles = new[,] {
                    {0, 1, 1},
                    {1, 1, 0},
                    {0, 0, 0}
                };
            } else if (pieceType == PieceType.Z) {
                this.CurrPos = new Point(Board.Width / 2 - 1, 2);
                this.Color = Color.Red;
                this.Tiles = new[,] {
                    {1, 1, 0},
                    {0, 1, 1},
                    {0, 0, 0}
                };
            }
        }


        public virtual Color GetColor() {
            return this.Color;
        }

        public bool CanRotate(bool clockwise) {
            var result = new int[this.Width, this.Width];
            if (!clockwise) {
                //CCW
                for (var x = 0; x < this.Width; x++) {
                    for (var y = 0; y < this.Width; y++) {
                        var newX = y;
                        var newY = this.Width - (x + 1);
                        result[newY, newX] = this.Tiles[y, x];
                    }
                }
            } else {
                //CW
                for (var x = 0; x < this.Width; x++) {
                    for (var y = 0; y < this.Width; y++) {
                        var newX = this.Width - (y + 1);
                        var newY = x;
                        result[newY, newX] = this.Tiles[y, x];
                    }
                }
            }

            var zeroPos = this.CurrPos - this.Offset;
            var size = this.Width;
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    if (result[y, x] == 1) {
                        if (zeroPos.X + x < 0 || zeroPos.X + x >= Board.Width || zeroPos.Y + y >= Board.Height || this.board.Map[zeroPos.X + x, zeroPos.Y + y] != Color.Transparent)
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
                    result[newY, newX] = this.Tiles[y, x];
                }
            }

            this.Tiles = result;
        }

        public void RotatePieceCW() {
            var result = new int[this.Width, this.Width];
            for (var x = 0; x < this.Width; x++) {
                for (var y = 0; y < this.Width; y++) {
                    var newX = this.Width - (y + 1);
                    var newY = x;
                    result[newY, newX] = this.Tiles[y, x];
                }
            }

            this.Tiles = result;
        }


        public bool CanMove(Point move) {
            //var offset = this.Width == 3 ? new Point(1, 1) : new Point(2, 2);
            var zeroPos = this.CurrPos - this.Offset + move;
            var size = this.Width;
            for (var y = 0; y < size; y++) {
                for (var x = 0; x < size; x++) {
                    if (this.Tiles[y, x] == 1) {
                        var zeroX = zeroPos.X + x;
                        var zeroY = zeroPos.Y + y;
                        if (zeroX < 0 || zeroX >= Board.Width || zeroY >= Board.Height || this.board.Map[zeroX, zeroY] != Color.Transparent)
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
            var color = this.GetColor();
            for (var y = 0; y < this.Height; y++) {
                for (var x = 0; x < this.Width; x++) {
                    if (this.Tiles[y, x] == 1)

                        batch.FillRectangle(renderPos + new Vector2(x, y - 3), new Size2(1, 1), color);
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


    public class GhostPiece : Piece {

        public GhostPiece(PieceType pieceType, Board board) : base(pieceType, board) {
        }

        public void PositionGhost() {
            this.CurrPos = this.board.CurrPiece.CurrPos;

            while (this.CanMove(new Point(0, 1))) {
                this.Move(new Point(0, 1));
            }
        }

        public override Color GetColor() {
            return base.GetColor() * 0.55F;
        }

    }
}
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
            this.currPiece = new Piece(PieceType.S);
        }


        public void Draw(GameTime gameTime, SpriteBatch batch, Viewport screen) {
            var matrix = Matrix.CreateScale(Scale) * Matrix.CreateTranslation(screen.Width / 2 - Width * Scale / 2, 0, 0);
            batch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: matrix);
            this.currPiece.Draw(batch);
            for (var x = 0; x < Width; x++) {
                for (var y = 0; y < Height; y++) {
                    batch.DrawRectangle(new Vector2(x, y), new Size2(1, 1), Color.Black, 1 / 16F);
                }
            }
            batch.End();
        }

    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ctris {
    public class GameImpl : Game {

        private SpriteBatch batch;
        public Board Board;
        public static GameImpl instance;


        public GameImpl() {
            new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
            instance = this;
        }

        protected override void LoadContent() {
            this.batch = new SpriteBatch(this.GraphicsDevice);
            this.Board = new Board();
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.Board.Draw(gameTime, this.batch, this.GraphicsDevice.Viewport);
        }
        

        private double seconds;

        protected override void Update(GameTime gameTime) {
            InputManager.Update(this.Board.CurrPiece);
            seconds += gameTime.ElapsedGameTime.TotalSeconds;
            if (this.seconds >= 1) {
                this.seconds -= 1;
                if (!this.Board.CurrPiece.CanMove(new Point(0, 1)))
                    this.Board.PieceToMap();
                else
                    this.Board.CurrPiece.Move(new Point(0, 1));
            }
        }


    }
}
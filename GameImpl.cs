using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ctris {
    public class GameImpl : Game {

        private SpriteBatch batch;
        private Board Board;


        public GameImpl() {
            new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
        }

        protected override void LoadContent() {
            batch = new SpriteBatch(this.GraphicsDevice);
            this.Board = new Board();
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.Board.Draw(gameTime, batch, this.GraphicsDevice.Viewport);
        }

        protected override void Update(GameTime gameTime) {
            InputManager.Update(this.Board.currPiece);
        }


    }
}
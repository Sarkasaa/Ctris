using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ctris {
    public class GameImpl : Game {

        private SpriteBatch batch;
        private Board board;


        public GameImpl() {
            new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };
        }

        protected override void LoadContent() {
            batch = new SpriteBatch(this.GraphicsDevice);
            this.board = new Board();
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.board.Draw(gameTime, batch, this.GraphicsDevice.Viewport);
        }

        protected override void Update(GameTime gameTime) {
            InputManager.Update(this.board.currPiece);
        }


    }
}
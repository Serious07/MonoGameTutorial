using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tutorial.Effects;
using Tutorial.Utilities;

namespace Tutorial.Screens {
    public class SplashScreen : GameScreen {
        public Image image;

        public override void LoadContent() {
            base.LoadContent();
            image.LoadContent();
            image.fadeEffect.fadeSpeed = 0.5f;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            image.Update(gameTime);
        }

        public override void UnloadContent() {
            base.UnloadContent();
            image.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            image.Draw(spriteBatch);
        }
    }
}

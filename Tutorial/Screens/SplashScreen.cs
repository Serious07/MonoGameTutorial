using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial.Screens {
    public class SplashScreen : GameScreen {
        Texture2D image;
        string path;

        public override void LoadContent() {
            base.LoadContent();
            path = "SplashScreen/Image";
            image = content.Load<Texture2D>(path);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch) {
            Vector2 screenCenter = new Vector2(ScreenManager.Instance.Dimensions.X / 2f, 
                                               ScreenManager.Instance.Dimensions.Y / 2f);
            Vector2 imageCenter = new Vector2(image.Width / 2f, image.Height / 2f);

            spriteBatch.Draw(image, screenCenter, null, Color.White, 0f, imageCenter, 
                             1f, SpriteEffects.None, 0f);
        }
    }
}

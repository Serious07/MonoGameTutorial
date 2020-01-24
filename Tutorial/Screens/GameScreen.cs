using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tutorial.Screens {
    public class GameScreen {
        protected ContentManager content;
        [XmlIgnore]
        public Type type;
        public string xmlPath;

        public GameScreen() {
            type = this.GetType();
            xmlPath = "Load/" + type.ToString().Replace("Tutorial.Screens.", "") + ".xml";
        }

        public virtual void LoadContent() {
            content = 
                new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent() {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime) {

        }

        public virtual void Draw(SpriteBatch spriteBatch) {

        }
    }
}
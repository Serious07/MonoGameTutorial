using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tutorial.Screens;
using Tutorial.Effects;
using Tutorial.Utilities;
using System.Xml.Serialization;

namespace Tutorial.Screens {
    public class ScreenManager {
        [XmlIgnore]
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }

        private XmlManager<GameScreen> xmlGameScreenManager;
        private GameScreen currentScreen, newScreen;
        [XmlIgnore]
        public GraphicsDevice graphicsDevice;
        [XmlIgnore]
        public SpriteBatch spriteBatch;

        public Image image;
        [XmlIgnore]
        public bool isTransitioning { get; private set; }

        private static ScreenManager instance;

        public static ScreenManager Instance {
            get {
                if(instance == null) {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
                }

                return instance;
            }
        }

        public ScreenManager() {
            Dimensions = new Vector2(640, 480);
            currentScreen = new SplashScreen();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.type;
            currentScreen = xmlGameScreenManager.Load("Load/SplashScreen.xml");
        }

        public void ChangeScreens(string screenName) {
            newScreen = 
                (GameScreen)Activator.CreateInstance(Type.GetType("Tutorial.Screens." + screenName));
            image.isActive = true;
            image.fadeEffect.increase = true;
            image.alpha = 0.0f;
            isTransitioning = true;
        }

        public void Transition(GameTime gameTime) {
            if (isTransitioning) {
                image.Update(gameTime);
                if(image.alpha == 1.0f) {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    xmlGameScreenManager.Type = currentScreen.type;
                    if (File.Exists(currentScreen.xmlPath)) {
                        currentScreen = xmlGameScreenManager.Load(currentScreen.xmlPath);
                    }
                    currentScreen.LoadContent();
                } else if (image.alpha == 0.0f) {
                    image.isActive = false;
                    isTransitioning = false;
                }
            }
        }

        public virtual void LoadContent(ContentManager Content) {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
            image.LoadContent();
        }

        public virtual void UnloadContent() {
            currentScreen.UnloadContent();
            image.UnloadContent();
        }

        public virtual void Update(GameTime gameTime) {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            currentScreen.Draw(spriteBatch);
            if (isTransitioning) {
                image.Draw(spriteBatch);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tutorial.Screens;

namespace Tutorial
{
    public class ScreenManager {
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }

        GameScreen currentScreen;

        private static ScreenManager instance;

        public static ScreenManager Instance {
            get {
                if(instance == null) {
                    instance = new ScreenManager();
                }

                return instance;
            }
        }

        public ScreenManager() {
            Dimensions = new Vector2(640, 480);
            currentScreen = new SplashScreen();
        }

        public virtual void LoadContent(ContentManager Content) {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public virtual void UnloadContent() {
            currentScreen.UnloadContent();
        }

        public virtual void Update(GameTime gameTime) {
            currentScreen.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            currentScreen.Draw(spriteBatch);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tutorial.Effects;
using Tutorial.Screens;

namespace Tutorial.Utilities {
    public class Image {
        public float alpha;
        public string text, fontName, path;
        [XmlIgnore]
        public Texture2D texture;
        public Vector2 position, scale, origin;
        public Rectangle sourceRect;
        public bool isActive;
        private ContentManager content;
        private RenderTarget2D renderTarget;
        private SpriteFont font;
        private Dictionary<string, ImageEffect> effectList;
        public string effects;

        public FadeEffect fadeEffect;

        private void SetEffect<T>(ref T effect) {
            if(effect == null) {
                effect = (T)Activator.CreateInstance(typeof(T));
            } else {
                (effect as ImageEffect).isActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            effectList.Add(effect.GetType().ToString().Replace("Tutorial.Effects.", ""), 
                (effect as ImageEffect));
        }

        public void ActivateEffect(string effect) {
            if (effectList.ContainsKey(effect)) {
                effectList[effect].isActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect) {
            if (effectList.ContainsKey(effect)) {
                effectList[effect].isActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public Image() {
            path = text = effects = string.Empty;
            fontName = "Fonts/defaultFont";
            position = Vector2.Zero;
            scale = Vector2.One;
            alpha = 1.0f;
            sourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent() {
            content = 
                new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if(path != string.Empty) {
                texture = content.Load<Texture2D>(path);
            }

            font = content.Load<SpriteFont>(fontName);

            Vector2 dimensions = Vector2.Zero;

            if(texture != null) {
                dimensions.X = texture.Width;
            }
            dimensions.X += font.MeasureString(text).X;

            if(texture != null) {
                dimensions.Y = Math.Max(texture.Height, font.MeasureString(text).Y);
            } else {
                dimensions.Y = font.MeasureString(text).Y;
            }

            if (sourceRect == Rectangle.Empty) {
                sourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            }

            renderTarget = new RenderTarget2D(ScreenManager.Instance.graphicsDevice,
                                              (int)dimensions.X, (int)dimensions.Y);

            ScreenManager.Instance.graphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.graphicsDevice.Clear(Color.Transparent);
            
            ScreenManager.Instance.spriteBatch.Begin();
            if (texture != null) {
                ScreenManager.Instance.spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            }
            ScreenManager.Instance.spriteBatch.DrawString(font, text, Vector2.Zero, Color.White);
            ScreenManager.Instance.spriteBatch.End();

            texture = renderTarget;

            ScreenManager.Instance.graphicsDevice.SetRenderTarget(null);

            SetEffect<FadeEffect>(ref fadeEffect);

            if (effects != string.Empty) {
                string[] split = effects.Split(':');
                foreach (string item in split) {
                    ActivateEffect(item);
                }
            }
        }

        public void UnloadContent() {
            content.Unload();
            foreach (var effect in effectList) {
                DeactivateEffect(effect.Key);
            }
        }

        public void Update(GameTime gameTime) {
            foreach (var effect in effectList) {
                if (effect.Value.isActive) {
                    effect.Value.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
            spriteBatch.Draw(texture, position + origin, sourceRect, Color.White * alpha,
                0.0f, origin, scale, SpriteEffects.None, 0.0f);
        }
    }
}

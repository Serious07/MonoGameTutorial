using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using Tutorial.Screens;
using Tutorial.Utilities;

namespace Tutorial.Menu {
    public class Menu {
        public event EventHandler onMenuChange;

        public string axis;
        public string effects;
        [XmlElement("item")]
        public List<MenuItem> items;
        int itemNumber;
        string id;

        public string ID {
            get { return id; }
            set {
                id = value;
                onMenuChange(this, null);
            }
        }

        void AlignMenuItems() {
            Vector2 dimensions = Vector2.Zero;
            foreach(MenuItem item in items) {
                dimensions += new Vector2(item.image.sourceRect.Width, 
                                          item.image.sourceRect.Height);
            }

            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X - dimensions.X) / 2,
                                     (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2);

            foreach(MenuItem item in items) {
                if (axis == "X") {
                    item.image.position = new Vector2(dimensions.X,
                        (ScreenManager.Instance.Dimensions.Y - item.image.sourceRect.Height) / 2);
                } else if (axis == "Y") {
                    item.image.position = new Vector2((ScreenManager.Instance.Dimensions.X - 
                                                      item.image.sourceRect.Width) / 2, dimensions.Y);
                }
                dimensions += new Vector2(item.image.sourceRect.Width,
                                          item.image.sourceRect.Height);
            }
        }

        public Menu() {
            id = String.Empty;
            itemNumber = 0;
            effects = String.Empty;
            axis = "Y";
            items = new List<MenuItem>();
        }

        public void LoadContent() {
            string[] split = effects.Split(':');
            foreach(MenuItem item in items) {
                item.image.LoadContent();
                foreach(string s in split) {
                    item.image.ActivateEffect(s);
                }
            }
            AlignMenuItems();
        }

        public void UnloadContent() {
            foreach (MenuItem item in items) {
                item.image.UnloadContent();
            }
        }

        public void Update(GameTime gameTime) {
            if(axis == "X") {
                if (InputManager.Instance.KeyRelesed(Keys.Right)) {
                    itemNumber++;
                } else if (InputManager.Instance.KeyRelesed(Keys.Left)) {
                    itemNumber--;
                }
            } else if(axis == "Y") {
                if (InputManager.Instance.KeyRelesed(Keys.Down)) {
                    itemNumber++;
                } else if (InputManager.Instance.KeyRelesed(Keys.Up)) {
                    itemNumber--;
                }
            }

            if(itemNumber < 0) {
                itemNumber = 0;
            } else if(itemNumber > items.Count - 1) {
                itemNumber = items.Count - 1;
            }

            for(int i = 0; i < items.Count; i++) {
                if(i == itemNumber) {
                    items[i].image.isActive = true;
                } else {
                    items[i].image.isActive = false;
                }

                items[i].image.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach(MenuItem item in items) {
                item.image.Draw(spriteBatch);
            }
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial.Utilities;

namespace Tutorial.Effects {
    public class ImageEffect {
        public Image image;
        public bool isActive;

        public ImageEffect() {
            isActive = false;
        }

        public virtual void LoadContent(ref Image image) {
            this.image = image;
        }

        public virtual void UnloadContent() {

        }

        public virtual void Update(GameTime gameTime) {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Tutorial.Screens;

namespace Tutorial.Utilities {
    public class InputManager {
        KeyboardState currentKeyState, prevKeyState;

        private static InputManager instance;
        public static InputManager Instance {
            get {
                if(instance == null) {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        public void Update() {
            prevKeyState = currentKeyState;
            if (ScreenManager.Instance.isTransitioning == false) {
                currentKeyState = Keyboard.GetState();
            }
        }

        public bool KeyRelesed(params Keys[] keys) {
            foreach (Keys key in keys) {
                if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key)) {
                    return true;
                }
            }

            return false;
        }

        public bool KeyPressed(params Keys[] keys) {
            foreach (Keys key in keys) {
                if (currentKeyState.IsKeyDown(key)) {
                    return true;
                }
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaInput
{
    public delegate float InputMethod();

    public class Input : GameComponent
    {
        private Dictionary<PlayerIndex, Dictionary<string, InputMethod>> PlayerControls;
        public Dictionary<PlayerIndex, Pad> GamePads;
        public MouseHelper Mouse;
        public KeyboardHelper Keyboard;

        //Methods for assigning controls to a single player index

        /// <summary>
        /// Allows you to assign a custom-made InputMethod to a control. 
        /// InputMethods take no arguments, and return a float.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="method"></param>
        public void AssignControl(PlayerIndex player, string name, InputMethod method)
        {
            PlayerControls[player][name] = method;
        }

        /// <summary>
        /// Allows you to assign an analog GamePad part to an input control, with the same gamepad as player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="gen"></param>
        public void AssignControl(PlayerIndex player, string name, Pad.Generator gen)
        {
            PlayerControls[player][name] = gen(player);
        }

        /// <summary>
        /// Assigns a gamepad part to a control, with a different gamepad playerindex than player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="padPlayer"></param>
        /// <param name="gen"></param>
        public void AssignControl(PlayerIndex player, string name, PlayerIndex padPlayer, Pad.Generator gen)
        {
            PlayerControls[player][name] = gen(padPlayer);
        }

        /// <summary>
        /// Assigns a GamePad button to a control. The control will return 1.0f if the button is pressed, 0.0f otherwise.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="button"></param>
        public void AssignControl(PlayerIndex player, string name, Buttons button)
        {
            PlayerControls[player][name] = delegate {
                return (GamePads[player].IsConnected && GamePads[player].IsButtonDown(button)) ? 1.0f : 0.0f; 
            };
        }

        /// <summary>
        /// Assigns two Gamepad buttons to a control. The control returns 1.0f if the first is pressed, 
        /// -1.0f if the second is pressed, and 0.0f if both or none are pressed.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        public void AssignControl(PlayerIndex player, string name, Buttons high, Buttons low)
        {
            PlayerControls[player][name] = delegate
            {
                float val = 0.0f;
                if (GamePads[player].IsConnected)
                {
                    if (GamePads[player].IsButtonDown(high)) val += 1.0f;
                    if (GamePads[player].IsButtonDown(low)) val -= 1.0f;
                }
                return val;
            };
        }

        /// <summary>
        /// Assigns a keyboard key to a control. The control returns 1.0f if the key is pressed, 0.0f otherwise.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="key"></param>
        public void AssignControl(PlayerIndex player, string name, Keys key)
        {
            PlayerControls[player][name] = delegate { return (Keyboard.State.IsKeyDown(key)) ? 1.0f : 0.0f; };
        }

        /// <summary>
        /// Assigns two keys to a control. The control returns 1.0f if the first is pressed,
        /// -1.0f if the second is pressed, and 0.0f if both or neither are pressed.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        public void AssignControl(PlayerIndex player, string name, Keys high, Keys low)
        {
            PlayerControls[player][name] =
                delegate
                {
                    float val = 0.0f;
                    if (Keyboard.State.IsKeyDown(high)) val += 1.0f;
                    if (Keyboard.State.IsKeyDown(low)) val -= 1.0f;
                    return val;
                };
        }

        //Methods for assigning controls to all player indices

        /// <summary>
        /// Allows you to assign a custom-made InputMethod to a control for all players. 
        /// InputMethods take no arguments, and return a float.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="method"></param>
        public void AssignControl(string name, InputMethod method)
        {
            AssignControl(PlayerIndex.One, name, method);
            AssignControl(PlayerIndex.Two, name, method);
            AssignControl(PlayerIndex.Three, name, method);
            AssignControl(PlayerIndex.Four, name, method);
        }

        /// <summary>
        /// Allows you to assign an analog GamePad part to an input control for all players, with the same gamepad as player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="gen"></param>
        public void AssignControl(string name, Pad.Generator gen)
        {
            AssignControl(PlayerIndex.One, name, gen);
            AssignControl(PlayerIndex.Two, name, gen);
            AssignControl(PlayerIndex.Three, name, gen);
            AssignControl(PlayerIndex.Four, name, gen);
        }

        /// <summary>
        /// Assigns a gamepad part to a control for all players, with a different gamepad playerindex than player.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="padPlayer"></param>
        /// <param name="gen"></param>
        public void AssignControl(string name, PlayerIndex padPlayer, Pad.Generator gen)
        {
            AssignControl(PlayerIndex.One, name, padPlayer, gen);
            AssignControl(PlayerIndex.Two, name, padPlayer, gen);
            AssignControl(PlayerIndex.Three, name, padPlayer, gen);
            AssignControl(PlayerIndex.Four, name, padPlayer, gen);
        }

        /// <summary>
        /// Assigns a GamePad button to a control for all players. The control will return 1.0f if the button is pressed, 0.0f otherwise.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="button"></param>
        public void AssignControl(string name, Buttons button)
        {
            AssignControl(PlayerIndex.One, name, button);
            AssignControl(PlayerIndex.Two, name, button);
            AssignControl(PlayerIndex.Three, name, button);
            AssignControl(PlayerIndex.Four, name, button);
        }

        /// <summary>
        /// Assigns two Gamepad buttons to a control for all players. The control returns 1.0f if the first is pressed, 
        /// -1.0f if the second is pressed, and 0.0f if both or none are pressed.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        public void AssignControl(string name, Buttons high, Buttons low)
        {
            AssignControl(PlayerIndex.One, name, high, low);
            AssignControl(PlayerIndex.Two, name, high, low);
            AssignControl(PlayerIndex.Three, name, high, low);
            AssignControl(PlayerIndex.Four, name, high, low);
        }

        /// <summary>
        /// Assigns a keyboard key to a control for all players. The control returns 1.0f if the key is pressed, 0.0f otherwise.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="key"></param>
        public void AssignControl(string name, Keys key)
        {
            AssignControl(PlayerIndex.One, name, key);
            AssignControl(PlayerIndex.Two, name, key);
            AssignControl(PlayerIndex.Three, name, key);
            AssignControl(PlayerIndex.Four, name, key);
        }

        /// <summary>
        /// Assigns two keys to a control. The control returns 1.0f if the first is pressed,
        /// -1.0f if the second is pressed, and 0.0f if both or neither are pressed.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="name"></param>
        /// <param name="high"></param>
        /// <param name="low"></param>
        public void AssignControl(string name, Keys high, Keys low)
        {
            AssignControl(PlayerIndex.One, name, high, low);
            AssignControl(PlayerIndex.Two, name, high, low);
            AssignControl(PlayerIndex.Three, name, high, low);
            AssignControl(PlayerIndex.Four, name, high, low);
        }



        public float ControlState(PlayerIndex player, string name)
        {
            return PlayerControls[player][name]();
        }

        public Input(Game game) : base(game)
        {
            PlayerControls = new Dictionary<PlayerIndex, Dictionary<string, InputMethod>>();
            PlayerControls[PlayerIndex.One] = new Dictionary<string, InputMethod>();
            PlayerControls[PlayerIndex.Two] = new Dictionary<string, InputMethod>();
            PlayerControls[PlayerIndex.Three] = new Dictionary<string, InputMethod>();
            PlayerControls[PlayerIndex.Four] = new Dictionary<string, InputMethod>();

            GamePads = new Dictionary<PlayerIndex, Pad>();
            GamePads[PlayerIndex.One] = new Pad(PlayerIndex.One);
            GamePads[PlayerIndex.Two] = new Pad(PlayerIndex.Two);
            GamePads[PlayerIndex.Three] = new Pad(PlayerIndex.Three);
            GamePads[PlayerIndex.Four] = new Pad(PlayerIndex.Four);

            Mouse = new MouseHelper();
            Keyboard = new KeyboardHelper();

            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            GamePads[PlayerIndex.One].Update();
            GamePads[PlayerIndex.Two].Update();
            GamePads[PlayerIndex.Three].Update();
            GamePads[PlayerIndex.Four].Update();

            Mouse.Update();
            Keyboard.Update();

            base.Update(gameTime);
        }
    }
}

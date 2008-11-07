using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaInput
{
    public class Pad
    {
        public delegate InputMethod Generator(PlayerIndex player);

        public static class ThumbSticks
        {
            public static class Left
            {
                public static InputMethod X(PlayerIndex player)
                {
                    return delegate {
                        GamePadState state = GamePad.GetState(player);
                        return (state.IsConnected) ?
                            state.ThumbSticks.Left.X : 
                            0.0f; 
                    };
                }
                public static InputMethod Y(PlayerIndex player)
                {
                    return delegate
                    {
                        GamePadState state = GamePad.GetState(player);
                        return (state.IsConnected) ?
                            state.ThumbSticks.Left.Y :
                            0.0f;
                    };
                }
            }
            public static class Right
            {
                public static InputMethod X(PlayerIndex player)
                {
                    return delegate
                    {
                        GamePadState state = GamePad.GetState(player);
                        return (state.IsConnected) ?
                            state.ThumbSticks.Right.X :
                            0.0f;
                    };
                }
                public static InputMethod Y(PlayerIndex player)
                {
                    return delegate
                    {
                        GamePadState state = GamePad.GetState(player);
                        return (state.IsConnected) ?
                            state.ThumbSticks.Right.Y :
                            0.0f;
                    };
                }
            }
        }

        public static class Triggers
        {
            public static InputMethod Left(PlayerIndex player)
            {
                return delegate
                {
                    GamePadState state = GamePad.GetState(player);
                    return (state.IsConnected) ?
                        state.Triggers.Left :
                        0.0f;
                };
            }
            public static InputMethod Right(PlayerIndex player)
            {
                return delegate
                {
                    GamePadState state = GamePad.GetState(player);
                    return (state.IsConnected) ?
                        state.Triggers.Right :
                        0.0f;
                };
            }
        }

        GamePadState state;
        GamePadState previousState;
        PlayerIndex player;

        public bool IsConnected
        {
            get { return state.IsConnected; }
        }
                
        public Pad(PlayerIndex p)
        {
            player = p;
        }

        public void Update()
        {
            previousState = state;
            state = GamePad.GetState(player);
        }        

        public bool IsButtonDown(Buttons button)
        {
            return state.IsButtonDown(button);
        }

        public bool IsButtonUp(Buttons button)
        {
            return state.IsButtonUp(button);
        }

        public bool JustPressed(Buttons button)
        {
            return state.IsButtonDown(button) & previousState.IsButtonDown(button);
        }

        public bool JustReleased(Buttons button)
        {
            return state.IsButtonUp(button) & previousState.IsButtonUp(button);
        }
    }

    public class MouseHelper
    {
        public enum Button
        {
            Left = 0,
            Right,
            Middle,
            XButton1,
            XButton2
        }

        public MouseState PreviousState, State;

        public void Update()
        {
            PreviousState = State;
            State = Mouse.GetState();
        }

        public float X()
        {
            return (float)State.X;
        }

        public float Y()
        {
            return (float)State.Y;
        }

        public float LeftButton()
        {
            return (State.LeftButton == ButtonState.Pressed) ? 1.0f : 0.0f;
        }

        public float ScrollWheelValue()
        {
            return (float)State.ScrollWheelValue;
        }

        /// <summary>
        /// Creates an InputMethod that returns 1.0f if the mouse button was just clicked, and 0.0f otherwise.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public InputMethod Click(Button button)
        {
            switch (button)
            {
                case Button.Left:
                    return delegate
                    {
                        return (PreviousState.LeftButton == ButtonState.Released &&
                            State.LeftButton == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
                case Button.Middle:
                    return delegate {
                        return (PreviousState.MiddleButton == ButtonState.Released && 
                            State.MiddleButton == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
                case Button.Right:
                    return delegate {
                        return (PreviousState.RightButton == ButtonState.Released && 
                            State.RightButton == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
                case Button.XButton1:                    
                    return delegate {
                        return (PreviousState.XButton1 == ButtonState.Released && 
                            State.XButton1 == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
                case Button.XButton2:                    
                    return delegate {
                        return (PreviousState.XButton2 == ButtonState.Released && 
                            State.XButton2 == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
            }
            return delegate { return 0.0f; }; //should never happen
        }

        /// <summary>
        /// Creates an InputMethod that returns 1.0f if the mouse button was just released, and 0.0f otherwise.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public InputMethod Release(Button button)
        {
            switch (button)
            {
                case Button.Left:
                    return delegate
                    {
                        return (PreviousState.LeftButton == ButtonState.Pressed &&
                            State.LeftButton == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
                case Button.Middle:
                    return delegate
                    {
                        return (PreviousState.MiddleButton == ButtonState.Pressed &&
                            State.MiddleButton == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
                case Button.Right:
                    return delegate
                    {
                        return (PreviousState.RightButton == ButtonState.Pressed &&
                            State.RightButton == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
                case Button.XButton1:
                    return delegate
                    {
                        return (PreviousState.XButton1 == ButtonState.Pressed &&
                            State.XButton1 == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
                case Button.XButton2:
                    return delegate
                    {
                        return (PreviousState.XButton2 == ButtonState.Pressed &&
                            State.XButton2 == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
            }
            return delegate { return 0.0f; }; //should never happen
        }

        /// <summary>
        /// Creates an InputMethod that returns 1.0f if the mouse button is down, and 0.0f otherwise.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public InputMethod Down(Button button)
        {
            switch (button)
            {
                case Button.Left:
                    return delegate
                    {
                        return (State.LeftButton == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
                case Button.Middle:
                    return delegate
                    {
                        return (State.MiddleButton == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
                case Button.Right:
                    return delegate
                    {
                        return (State.RightButton == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
                case Button.XButton1:
                    return delegate
                    {
                        return (State.XButton1 == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
                case Button.XButton2:
                    return delegate
                    {
                        return (State.XButton2 == ButtonState.Pressed) ?
                            1.0f :
                            0.0f;
                    };
            }
            return delegate { return 0.0f; }; //should never happen
        }

        /// <summary>
        /// Creates an InputMethod that returns 1.0f if the mouse button is up, and 0.0f otherwise.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public InputMethod Up(Button button)
        {
            switch (button)
            {
                case Button.Left:
                    return delegate
                    {
                        return (State.LeftButton == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
                case Button.Middle:
                    return delegate
                    {
                        return (State.MiddleButton == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
                case Button.Right:
                    return delegate
                    {
                        return (State.RightButton == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
                case Button.XButton1:
                    return delegate
                    {
                        return (State.XButton1 == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
                case Button.XButton2:
                    return delegate
                    {
                        return (State.XButton2 == ButtonState.Released) ?
                            1.0f :
                            0.0f;
                    };
            }
            return delegate { return 0.0f; }; //should never happen
        }
    }
}

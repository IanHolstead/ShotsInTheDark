using UnityEngine;
using System.Collections;
using XInputDotNetPure;

namespace ControlWrapping
{
    public class Gamepad
    {
        private readonly PlayerIndex[] playerIndices =
            { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four };

        private GamePadState currentState;
        private GamePadState oldState;

        private float triggerSensitivity = .85f;

        private int playerID;

        public delegate void UpdateStateDel(float tick);

        private float timeSinceVibration = 0;
        private float vibrationDuration = 0;

        public Gamepad(int ID)
        {
            playerID = ID;
        }

        public void UpdateState(float tick)
        {
            timeSinceVibration += tick;
            CurrentState = GamePad.GetState(playerIndices[playerID], GamePadDeadZone.Circular);
        }

        public void SetVibration(float leftMotor, float rightMotor, float duration = 0)
        {
            vibrationDuration = duration;
            timeSinceVibration = 0;
            GamePad.SetVibration(playerIndices[playerID], leftMotor, rightMotor);
        }

        public void EndVibration()
        {
            if (vibrationDuration != 0 && timeSinceVibration > vibrationDuration)
            {
                GamePad.SetVibration(playerIndices[playerID], 0, 0);
            }
        }

        public GamePadState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                oldState = currentState;
                currentState = value;
            }
        }

        public GamePadState OldState
        {
            get
            {
                return oldState;
            }
        }

        public float TriggerSensitivity
        {
            get
            {
                return triggerSensitivity;
            }
            set
            {
                triggerSensitivity = Mathf.Clamp(value, 0, 1);
            }
        }

        public bool GetButton(ActionKeyCode button)
        {
            switch (button)
            {
                case ActionKeyCode.GamepadA:
                    return CurrentState.Buttons.A == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadB:
                    return CurrentState.Buttons.B == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadX:
                    return CurrentState.Buttons.X == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadY:
                    return CurrentState.Buttons.Y == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadRightTrigger:
                    return CurrentState.Triggers.Right >= TriggerSensitivity;
                case ActionKeyCode.GamepadLeftTrigger:
                    return CurrentState.Triggers.Left >= TriggerSensitivity;
                case ActionKeyCode.GamepadRightShoulder:
                    return CurrentState.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadLeftSholder:
                    return CurrentState.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadRightStick:
                    return CurrentState.Buttons.RightStick == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadLeftStick:
                    return CurrentState.Buttons.LeftStick == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadStart:
                    return CurrentState.Buttons.Start == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadBack:
                    return CurrentState.Buttons.Back == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadGuide:
                    return CurrentState.Buttons.Guide == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadDPadUp:
                    return CurrentState.DPad.Up == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadDPadRight:
                    return CurrentState.DPad.Right == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadDPadDown:
                    return CurrentState.DPad.Down == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadDpadLeft:
                    return CurrentState.DPad.Left == XInputDotNetPure.ButtonState.Pressed;
                default:
                    Debug.LogError("Not a Gamepad Button");
                    break;
            }
            return false;
        }

        public bool GetButtonDown(ActionKeyCode button)
        {
            switch (button)
            {
                case ActionKeyCode.GamepadA:
                    return CurrentState.Buttons.A == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.A == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadB:
                    return CurrentState.Buttons.B == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.B == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadX:
                    return CurrentState.Buttons.X == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.X == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadY:
                    return CurrentState.Buttons.Y == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.Y == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadRightTrigger:
                    return CurrentState.Triggers.Right >= TriggerSensitivity && OldState.Triggers.Right < TriggerSensitivity;
                case ActionKeyCode.GamepadLeftTrigger:
                    return CurrentState.Triggers.Left >= TriggerSensitivity && OldState.Triggers.Left < TriggerSensitivity;
                case ActionKeyCode.GamepadRightShoulder:
                    return CurrentState.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadLeftSholder:
                    return CurrentState.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadRightStick:
                    return CurrentState.Buttons.RightStick == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.RightStick == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadLeftStick:
                    return CurrentState.Buttons.LeftStick == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.LeftStick == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadStart:
                    return CurrentState.Buttons.Start == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.Start == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadBack:
                    return CurrentState.Buttons.Back == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.Back == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadGuide:
                    return CurrentState.Buttons.Guide == XInputDotNetPure.ButtonState.Pressed && OldState.Buttons.Guide == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadDPadUp:
                    return CurrentState.DPad.Up == XInputDotNetPure.ButtonState.Pressed && OldState.DPad.Up == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadDPadRight:
                    return CurrentState.DPad.Right == XInputDotNetPure.ButtonState.Pressed && OldState.DPad.Right == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadDPadDown:
                    return CurrentState.DPad.Down == XInputDotNetPure.ButtonState.Pressed && OldState.DPad.Down == XInputDotNetPure.ButtonState.Released;
                case ActionKeyCode.GamepadDpadLeft:
                    return CurrentState.DPad.Left == XInputDotNetPure.ButtonState.Pressed && OldState.DPad.Up == XInputDotNetPure.ButtonState.Released;
                default:
                    Debug.LogError("Not a Gamepad Button");
                    break;
            }
            return false;
        }

        public bool GetButtonUp(ActionKeyCode button)
        {
            switch (button)
            {
                case ActionKeyCode.GamepadA:
                    return CurrentState.Buttons.A == XInputDotNetPure.ButtonState.Released && OldState.Buttons.A == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadB:
                    return CurrentState.Buttons.B == XInputDotNetPure.ButtonState.Released && OldState.Buttons.B == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadX:
                    return CurrentState.Buttons.X == XInputDotNetPure.ButtonState.Released && OldState.Buttons.X == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadY:
                    return CurrentState.Buttons.Y == XInputDotNetPure.ButtonState.Released && OldState.Buttons.Y == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadRightTrigger:
                    return CurrentState.Triggers.Right <= TriggerSensitivity && OldState.Triggers.Right > TriggerSensitivity;
                case ActionKeyCode.GamepadLeftTrigger:
                    return CurrentState.Triggers.Left <= TriggerSensitivity && OldState.Triggers.Left > TriggerSensitivity;
                case ActionKeyCode.GamepadRightShoulder:
                    return CurrentState.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Released && OldState.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadLeftSholder:
                    return CurrentState.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Released && OldState.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadRightStick:
                    return CurrentState.Buttons.RightStick == XInputDotNetPure.ButtonState.Released && OldState.Buttons.RightStick == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadLeftStick:
                    return CurrentState.Buttons.LeftStick == XInputDotNetPure.ButtonState.Released && OldState.Buttons.LeftStick == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadStart:
                    return CurrentState.Buttons.Start == XInputDotNetPure.ButtonState.Released && OldState.Buttons.Start == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadBack:
                    return CurrentState.Buttons.Back == XInputDotNetPure.ButtonState.Released && OldState.Buttons.Back == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadGuide:
                    return CurrentState.Buttons.Guide == XInputDotNetPure.ButtonState.Released && OldState.Buttons.Guide == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadDPadUp:
                    return CurrentState.DPad.Up == XInputDotNetPure.ButtonState.Released && OldState.DPad.Up == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadDPadRight:
                    return CurrentState.DPad.Right == XInputDotNetPure.ButtonState.Released && OldState.DPad.Right == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadDPadDown:
                    return CurrentState.DPad.Down == XInputDotNetPure.ButtonState.Released && OldState.DPad.Down == XInputDotNetPure.ButtonState.Pressed;
                case ActionKeyCode.GamepadDpadLeft:
                    return CurrentState.DPad.Left == XInputDotNetPure.ButtonState.Released && OldState.DPad.Up == XInputDotNetPure.ButtonState.Pressed;
                default:
                    Debug.LogError("Not a Gamepad Button");
                    break;
            }
            return false;
        }

        public float GetAxis(AxisCode axis)
        {
            if ((int)axis < 450)
            {
                //TODO: Feels bad man
                return GetButton((ActionKeyCode)((int)axis)) ? 1 : 0;
            }
            switch (axis)
            {
                case AxisCode.GamepadAxisRightX:
                    return CurrentState.ThumbSticks.Right.X;
                case AxisCode.GamepadAxisRightY:
                    return CurrentState.ThumbSticks.Right.Y;
                case AxisCode.GamepadAxisLeftX:
                    return CurrentState.ThumbSticks.Left.X;
                case AxisCode.GamepadAxisLeftY:
                    return CurrentState.ThumbSticks.Left.Y;
                case AxisCode.GamepadAxisRightTrigger:
                    return CurrentState.Triggers.Right;
                case AxisCode.GamepadAxisLeftTrigger:
                    return CurrentState.Triggers.Left;
                default:
                    Debug.LogError("Not a Gamepad Axis");
                    break;
            }
            return 0.0f;
        }
    }
}
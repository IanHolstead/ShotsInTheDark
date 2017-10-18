using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControlWrapping
{

    public class MenuInput
    {
        //This functionality could be added later
        //public float delayBeforeContinuous = 1.5f;
        //public float itemsPerSecondWhenContinous = 5;

        //float[] durationOfDirectionHeld;

        public static int GetMenuMovement(Gamepad gamePad)
        {
            float value = gamePad.GetAxis(AxisCode.GamepadAxisLeftY);

            if (Mathf.Abs(value) > 0)
            {
                return value > 0 ? 1 : -1;
            }
            value = gamePad.GetAxis(AxisCode.GamepadDPadDown) * -1;
            value = value == 0 ? gamePad.GetAxis(AxisCode.GamepadDPadUp) : value;

            return (int)value;
        }

        public static int GetMenuMovement(Gamepad[] gamePads)
        {
            int value = 0;
            foreach (Gamepad gamepad in gamePads)
            {
                value = GetMenuMovement(gamepad);
                if (value != 0)
                {
                    return value;
                }
            }
            return 0;
        }

        public static int GetOptionMovement(Gamepad gamePad)
        {
            float value = gamePad.GetAxis(AxisCode.GamepadAxisLeftX);

            if (Mathf.Abs(value) > 0)
            {
                return value > 0 ? 1 : -1;
            }
            value = gamePad.GetAxis(AxisCode.GamepadDpadLeft) * -1;
            value = value == 0 ? gamePad.GetAxis(AxisCode.GamepadDPadRight) : value;

            return (int)value;
        }

        public static int GetOptionMovement(Gamepad[] gamePads)
        {
            int value = 0;
            foreach (Gamepad gamepad in gamePads)
            {
                value = GetMenuMovement(gamepad);
                if (value != 0)
                {
                    return value;
                }
            }
            return 0;
        }

        public static bool GetSelectKeyDown(Gamepad gamePad)
        {
            return gamePad.GetButtonDown(ActionKeyCode.GamepadA);
        }

        public static bool GetSelectKeyDown(Gamepad[] gamePads)
        {
            foreach (Gamepad gamepad in gamePads)
            {
                if (GetSelectKeyDown(gamepad))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetBackKeyDown(Gamepad gamePad)
        {
            return gamePad.GetButtonDown(ActionKeyCode.GamepadB);
        }

        public static bool GetBackKeyDown(Gamepad[] gamePads)
        {
            foreach (Gamepad gamepad in gamePads)
            {
                if (GetBackKeyDown(gamepad))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetStartKeyDown(Gamepad gamePad)
        {
            return gamePad.GetButtonDown(ActionKeyCode.GamepadStart);
        }

        public static bool GetStartKeyDown(Gamepad[] gamePads)
        {
            foreach (Gamepad gamepad in gamePads)
            {
                if (GetStartKeyDown(gamepad))
                {
                    return true;
                }
            }
            return false;
        }


        //private bool GetAnyButtonDown(ActionKeyCode button)
        //{
        //    foreach (Gamepad gamepad in gamePads)
        //    {
        //        if (gamepad.GetButtonDown(button))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
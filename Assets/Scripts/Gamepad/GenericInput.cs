using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControlWrapping
{
    public class GenericInput
    {

        public static Vector2 GetLeftStickMovement(Gamepad gamePad)
        {
            return new Vector2(gamePad.GetAxis(AxisCode.GamepadAxisLeftX), gamePad.GetAxis(AxisCode.GamepadAxisLeftY));
        }

        public static Vector2 GetLeftStickMovement(Gamepad[] gamePads)
        {
            Vector2 total = new Vector2();
            foreach (Gamepad gamepad in gamePads)
            {
                total += GetLeftStickMovement(gamepad);
            }
            return total;
        }
    }
}
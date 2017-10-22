using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlWrapping;

public class PlayerInput {
    private KeyBinding keyBinding;
    private Gamepad gamepad;

    public PlayerInput(KeyBinding keyBinding, int playerIndex)
    {
        this.keyBinding = keyBinding;

        gamepad = ControllerManager.instance.RequestSpecificGamepad(playerIndex);

        if (gamepad == null)
        {
            Logger.Log("Didn't get a gamepad!!", this, LogLevel.Error);
        }
    }

    public float GetAxis(AxisControl control)
    {
        float axisValue = 0f;
        foreach (AxisCode code in keyBinding.GetAxisCodes(control))
        {
            axisValue = gamepad.GetAxis(code);
            if (!axisValue.Equals(0f))
            {
                return axisValue;
            }
        }
        return 0f;
    }

    public bool GetKeyDown(ActionControl control)
    {
        bool isKeyDown = false;
        foreach (ActionKeyCode key in keyBinding.GetActionKeyCodes(control))
        {
            isKeyDown = gamepad.GetButtonDown(key);
            if (isKeyDown)
            {
                return true;
            }
        }
        return false;
    }

    public bool GetKeyUp(ActionControl control)
    {
        bool isKeyUp = false;
        foreach (ActionKeyCode key in keyBinding.GetActionKeyCodes(control))
        {
            isKeyUp = gamepad.GetButtonUp(key);
            if (isKeyUp)
            {
                return true;
            }
        }
        return false;
    }

    public bool GetKey(ActionControl control)
    {
        bool isKeyDown = false;
        foreach (ActionKeyCode key in keyBinding.GetActionKeyCodes(control))
        {
            isKeyDown = gamepad.GetButton(key);
            if (isKeyDown)
            {
                return true;
            }
        }
        return false;
    }
}

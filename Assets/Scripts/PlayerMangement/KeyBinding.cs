using UnityEngine;
using System.Collections.Generic;
using ControlWrapping;
public enum ActionControl
{
    menuSelect,
    menuBack,
    menuStart,
    shoot,
    light,
    pause,
    NONE
}

public enum AxisControl
{
    moveX,
    moveY,
    aimX,
    aimY,
    NONE
}

public class KeyBinding
{
    string name;
    Dictionary<ActionControl, List<ActionKeyCode>> actionMapping;
    Dictionary<AxisControl, List<AxisCode>> axisMapping;

    //public delegate
    public KeyBinding()
    {
        SetDefaultMapping();
    }

    public void CreateEmptyMapping()
    {
        actionMapping = new Dictionary<ActionControl, List<ActionKeyCode>>();
        axisMapping = new Dictionary<AxisControl, List<AxisCode>>();
        foreach (ActionControl control in System.Enum.GetValues(typeof(ActionControl)))
        {
            actionMapping[control] = new List<ActionKeyCode>();
        }

        foreach (AxisControl control in System.Enum.GetValues(typeof(AxisControl)))
        {
            axisMapping[control] = new List<AxisCode>();
        }
    }

    //TODO: Not implemented
    ActionControl CheckIfKeyIsUsed(ActionKeyCode key)
    {
        return ActionControl.NONE;
    }

    /// <summary>
    /// Returns true if key isn't used and sets the key. Returns false if binding wasn't set
    /// </summary>
    /// <param name="control"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool setActionControl(ActionControl control, ActionKeyCode key)
    {
        if (actionMapping == null)
        {
            CreateEmptyMapping();
        }
        if (CheckIfKeyIsUsed(key) == ActionControl.NONE)
        {
            //key isn't in use
            actionMapping[control].Add(key);
            return true;
        }
        return false;
    }

    private void SetDefaultMapping()
    {
        CreateEmptyMapping();
        actionMapping[ActionControl.menuSelect].Add(ActionKeyCode.GamepadA);
        actionMapping[ActionControl.menuBack].Add(ActionKeyCode.GamepadB);
        actionMapping[ActionControl.menuStart].Add(ActionKeyCode.GamepadStart);
        actionMapping[ActionControl.shoot].Add(ActionKeyCode.GamepadA);
        actionMapping[ActionControl.shoot].Add(ActionKeyCode.GamepadLeftTrigger);
        actionMapping[ActionControl.shoot].Add(ActionKeyCode.GamepadRightTrigger);
        actionMapping[ActionControl.light].Add(ActionKeyCode.GamepadB);
        actionMapping[ActionControl.pause].Add(ActionKeyCode.GamepadStart);

        axisMapping[AxisControl.moveX].Add(AxisCode.GamepadAxisLeftX);
        axisMapping[AxisControl.moveY].Add(AxisCode.GamepadAxisLeftY);
        axisMapping[AxisControl.aimX].Add(AxisCode.GamepadAxisRightX);
        axisMapping[AxisControl.aimY].Add(AxisCode.GamepadAxisRightY);
    }
}

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
    List<ActionKeyCode> usedKeys;

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
    
    public List<ActionControl> CheckIfKeyIsUsed(ActionKeyCode key)
    {
        List<ActionControl> toReturn = new List<ActionControl>();
        if (!usedKeys.Contains(key))
        {
            toReturn.Add(ActionControl.NONE);
        }
        else
        {
            foreach (KeyValuePair<ActionControl, List<ActionKeyCode>> control in actionMapping)
            {
                foreach (ActionKeyCode actionKey in control.Value)
                {
                    if (actionKey == key)
                    {
                        toReturn.Add(control.Key);
                    }
                }
            }
        }
        return toReturn;
    }

    public List<AxisCode> GetAxisCodes(AxisControl control)
    {
        return axisMapping[control];
    }

    public List<ActionKeyCode> GetActionKeyCodes(ActionControl control)
    {
        return actionMapping[control];
    }

    /// <summary>
    /// sets key binding naively
    /// </summary>
    /// <param name="control"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public void SetActionControl(ActionControl control, ActionKeyCode key)
    {
        actionMapping[control].Add(key);
        usedKeys.Add(key);
    }

    public bool RemoveKey(ActionControl control, ActionKeyCode key)
    {
        if (actionMapping[control].Remove(key))
        {
            usedKeys.Remove(key);
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

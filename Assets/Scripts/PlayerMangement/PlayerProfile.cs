using UnityEngine;
using System.Collections.Generic;
//using XInputDotNetPure;
using ControlWrapping;
public enum ActionControl
{
    menuSelect,
    menuBack,
    menuStart,
    shoot,
    light,
    pause,
}

public enum AxisControl
{
    move,
    aim
}

//TODO: OK look, this is broken, deal with it later

public class PlayerProfile {
    string name;
    Dictionary<ActionControl, List<ActionKeyCode>> actionMapping;
    Dictionary<AxisControl, List<AxisCode>> axisMapping;

    //public delegate

    public void NewPlayerProfile()
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

    public bool setActionControl(ActionControl control)
    {
        return false;
    }

    private void SetDefaultMapping()
    {
        NewPlayerProfile();
        actionMapping[ActionControl.menuSelect].Add(ActionKeyCode.GamepadA);
        actionMapping[ActionControl.menuBack].Add(ActionKeyCode.GamepadB);
        actionMapping[ActionControl.menuStart].Add(ActionKeyCode.GamepadStart);
        actionMapping[ActionControl.shoot].Add(ActionKeyCode.GamepadA);
        actionMapping[ActionControl.shoot].Add(ActionKeyCode.GamepadLeftTrigger);
        actionMapping[ActionControl.shoot].Add(ActionKeyCode.GamepadRightTrigger);
        actionMapping[ActionControl.light].Add(ActionKeyCode.GamepadB);
        actionMapping[ActionControl.pause].Add(ActionKeyCode.GamepadStart);
    }
    

	
}

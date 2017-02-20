using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlWrapping;


//TODO: this should probably implement an interface or something.
public class ControllerManager : MonoBehaviour //: HoloToolkit.Unity.Singleton<ControllerManager>
{
    private const int maxNumberOfGamePads = 4;

    public static ControllerManager instance;
    Gamepad[] gamePads = new Gamepad[4];
    Gamepad.UpdateStateDel[] gamePadStateUpdaters = new Gamepad.UpdateStateDel[4];

    private int numberOfGamepadsInUse = 0;

    // Use this for initialization
    void Awake()
    {
        Logger.Log("ControllerManagerAwake");
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Gamepad.UpdateStateDel updater in gamePadStateUpdaters)
        {
            if (updater != null)
            {
                updater(Time.deltaTime);
            }
        }
    }

    public Gamepad RequestGamepad()
    {
        if (numberOfGamepadsInUse >= maxNumberOfGamePads)
        {
            return null;
        }

        int id;

        for (id = 0; id < maxNumberOfGamePads; id++)
        {
            if (gamePads[id] == null)
            {
                break;
            }
        }

        numberOfGamepadsInUse++;

        gamePads[id] = new Gamepad(id);
        gamePadStateUpdaters[id] = gamePads[id].UpdateState;
        gamePadStateUpdaters[id](0f);

        return gamePads[id];
    }

    public Gamepad RequestSpecificGamepad(int id)
    {
        if (gamePads[id] == null)
        {
            numberOfGamepadsInUse++;

            gamePads[id] = new Gamepad(id);
            gamePadStateUpdaters[id] = gamePads[id].UpdateState;
            gamePadStateUpdaters[id](0f);

            return gamePads[id];
        }
        return null;
    }

    public void ReturnGamePad(int gamePadID)
    {
        if (gamePads[gamePadID] != null)
        {
            numberOfGamepadsInUse--;
            gamePads[gamePadID] = null;
            gamePadStateUpdaters[gamePadID] = null;
        }
    }
}
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
        Logger.Log("ControllerManagerAwake", this, LogLevel.Log);
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

    /// <summary>
    /// Returns gamepad ref regardless if in use or not
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Gamepad ForceGetGamePad(int id)
    {
        Gamepad toReturn = RequestSpecificGamepad(id);
        if (toReturn == null)
        {
            toReturn = gamePads[id];
        }
        return toReturn;
    }

    //TODO: If I'm correct, this won't actually garbage collect the gamepad since the class originally requesting it should still have a ref.
    //as such, the gamepad will no longer update but won't null which could lead to bugs
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
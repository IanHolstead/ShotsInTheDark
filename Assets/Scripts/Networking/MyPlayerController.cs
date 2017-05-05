using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyPlayerController : PlayerController
{
    ControlWrapping.GamePadWrapper controller;
    // Use this for initialization

    [SyncVar]
    float xAxis = 0;
    [SyncVar]
    float yAxis = 0;
    DoOnce registerGamePad = new DoOnce();
    //protected void Start()
    //{
    //    base.Start();
    //    if (localPlayerAuthority)
    //    {
    //        controller = GI.ControllerManager.Instance.RequestGamepad();
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        //TODO: All controllers have local player authority so its getting a bunch of gamepads
        
        if (registerGamePad.Enter())
        {
            if (isLocalPlayer)
            {
                Logger.Log("Controller " + ID + " is LocalPlayer");
                //controller = ControllerManager.Instance.RequestGamepad();
            }
            else
            {
                Logger.Log("Controller " + ID + " is not LocalPlayer");
            }
        }

        if (isLocalPlayer)
        {
            xAxis = controller.Stick.Left.X;
            yAxis = controller.Stick.Left.Y;
            Debug.Log("Local Controller: " + ID + " x:" + xAxis + ", y: " + yAxis);
        }
        else
        {
            Debug.Log("Controller: " + ID + " x:" + xAxis + ", y: " + yAxis);
        }
    }
}
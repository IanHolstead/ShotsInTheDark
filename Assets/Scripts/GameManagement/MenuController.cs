using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlWrapping;

public class MenuController : MonoBehaviour {
    Gamepad[] gamepads;
    MenuState state;

    //List<PlayerProfile> players;
    int[] controllerPlayerIDMapping;

    PlayerMovement titleScreenMovement;

    public float blockInputTimer = 0;

    public Vector2 mainMenuLocation;

    void Awake()
    {
        gamepads = new Gamepad[GM.MAXPLAYERCOUNT];
        controllerPlayerIDMapping = new int[GM.MAXPLAYERCOUNT];
        for (int i = 0; i < GM.MAXPLAYERCOUNT; i++)
        {
            controllerPlayerIDMapping[i] = -1;
        }
    }

    // Use this for initialization
    void Start () {
        GetAllGamePads();
        print(gamepads);
        titleScreenMovement = FindObjectOfType<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {

        if (blockInputTimer >= 0)
        {
            blockInputTimer -= Time.deltaTime;
            return;
        }
        switch (state)
        {
            case MenuState.TitleScreen:
                Vector2 input = GenericInput.GetLeftStickMovement(gamepads);
                
                titleScreenMovement.Move(input.x, input.y, false);
                if (MenuInput.GetStartKeyDown(gamepads))
                {
                    PlayerShooting shooting = titleScreenMovement.GetComponent<PlayerShooting>();
                    shooting.Aim(mainMenuLocation.x, mainMenuLocation.y, 0, 0);
                    shooting.Draw();
                    shooting.Shoot();
                    //Play camera script
                    //Load menu
                    state = MenuState.MainMenu;
                    //TODO: Transition time
                    blockInputTimer = 1.5f;
                }
                break;
            case MenuState.MainMenu:
                break;
            case MenuState.PlayerSelect:
                for (int i = 0; i < GM.MAXPLAYERCOUNT; i++)
                {
                    if (controllerPlayerIDMapping[i] == -1)
                    {
                        if (MenuInput.GetSelectKeyDown(gamepads[i]))
                        {
                            //register player
                            controllerPlayerIDMapping[i] = GM.GameInstance.AddPlayer(i);
                        }
                    }
                    else
                    {
                        //pass on input
                    }
                    
                }
                break;
            case MenuState.LevelSelect:
                if (MenuInput.GetSelectKeyDown(gamepads))
                {
                    //Get current stage
                    //load current stage
                    return;
                }
                //Pass on input
                break;
        }
	}

    void GetAllGamePads()
    {
        for (int i = 0; i < GM.MAXPLAYERCOUNT; i++)
        {
            gamepads[i] = ControllerManager.instance.ForceGetGamePad(i);
        }
    }
}

enum MenuState
{
    TitleScreen, MainMenu, PlayerSelect, LevelSelect
}

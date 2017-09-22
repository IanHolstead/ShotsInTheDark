using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This doesn't really need to be a singleton anymore
public class GameInstance : Singleton<GameInstance> {

    void Awake()
    {
        DontDestroyOnLoad(this);
        GM.GameInstance = this;
    }
    
    void Start () {
		
	}
	
	void Update () {
		
	}
}

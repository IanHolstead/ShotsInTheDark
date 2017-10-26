using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour {
    bool toLoad = true;

	void Update() {
        if (toLoad)
        {
            toLoad = false;
            SceneManager.LoadScene("MainMenu");
        }
    }
}

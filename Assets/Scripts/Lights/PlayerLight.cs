using UnityEngine;
using System.Collections.Generic;

public class PlayerLight : MonoBehaviour {

    Dictionary<LightScript, float> lights;
    private SpriteRenderer characterSpritRender;
    private SpriteRenderer arrowSpritRender;

    void Awake()
    {
        characterSpritRender = GetComponent<SpriteRenderer>();
        arrowSpritRender = GetComponentInChildren<SpriteRenderer>();

        lights = new Dictionary<LightScript, float>();
    }
    
    public void UpdateTransperency()
    {
        float alpha = 0f;
        foreach (KeyValuePair<LightScript,float> light in lights)
        {
            alpha += light.Value;
        } 
        Color spiteColour = characterSpritRender.color;
        spiteColour.a = Mathf.Clamp01(alpha);
        characterSpritRender.color = spiteColour;
        arrowSpritRender.color = spiteColour;
    }

    public void updateTransperencyByLight(LightScript light, float transperency)
    {
        if (lights.ContainsKey(light))
        {
            lights[light] = transperency;
        }
        else
        {
            lights.Add(light, transperency);
        }
    }

    public void removeLight (LightScript light)
    {
        lights.Remove(light);
    }

    void OnDisable()
    {
        Logger.Log("Removing " + GetComponent<PlayerPawn>() + " from Player Light", this, LogLevel.Verbose);
        foreach (KeyValuePair<LightScript, float> light in lights)
        {
            light.Key.RemoveCharacter(this);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class LightScript : MonoBehaviour {
    float radius;
    List<PlayerLight> characters;
    private Light lightRef;

    private const float LIGHT_TO_COLLIDER_RADUIS = 2.24f;

    float fadeTime = -1f;
    float age = 0f;
    float initialLightIntensity;

    public AnimationCurve LightFalloff;

    

    void Awake()
    {
        lightRef = GetComponent<Light>();
        characters = new List<PlayerLight>();
    }

    void Start () {
        //makes the detection object the same size as the light
        radius = GetComponent<Light>().range;
        GetComponent<CircleCollider2D>().radius = radius*LIGHT_TO_COLLIDER_RADUIS;
        initialLightIntensity = lightRef.intensity;
    }
	
	void Update () {
        age += Time.deltaTime;

        //This is an optimization so that a new list isn't created each frame
        List<PlayerLight> toRemove = null;
        foreach (PlayerLight character in characters)
        {
            if (character == null)
            {
                if (toRemove == null)
                {
                    toRemove = new List<PlayerLight>();
                }
                toRemove.Add(character);
            }
            else
            {
                character.updateTransperencyByLight(this, GetLightValue(character.transform.position));
            }
        }
        if (toRemove != null)
        {
            foreach (PlayerLight character in toRemove)
            {
                characters.Remove(character);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: Tags are the worst
        if (other.gameObject.tag == "Player")
        {
            //TODO: Crashes on menu character
            //Logger.Log("Added: " + other.GetComponent<PlayerPawn>().PlayerIndex, this, LogLevel.Verbose);

            characters.Add(other.GetComponent<PlayerLight>());
        }
    }

    //TODO: this looks dated
    void OnTriggerExit2D(Collider2D other)
    {
        //TODO: Tags are the worst
        if (other.gameObject.tag == "Player")
        {
            //TODO: Crashes on menu character
            //Logger.Log("Removed: " + other.GetComponent<PlayerPawn>().PlayerIndex, this, LogLevel.Verbose);
            other.GetComponentInParent<PlayerLight>().removeLight(this);
            characters.Remove(other.GetComponent<PlayerLight>());
        }
    }

    //For removing dead players
    public void RemoveCharacter(PlayerLight character)
    {
        characters.Remove(character);
        Logger.Log("Removed: " + character, this, LogLevel.Verbose);
    }

	void OnDisable()
	{
        if (characters == null)
        {
            return;
        }
		foreach (PlayerLight character in characters)
		{
			character.removeLight(this);
		}
	}

    public void FadeAway(float timeToFade)
    {
        fadeTime = timeToFade;
        age = 0f;
        Destroy(gameObject, timeToFade);
    }

    float GetLightValue( Vector2 position )
    {
        float lightPercent = Mathf.Max(radius - GetDistance(position), 0f) / radius;

        lightPercent = LightFalloff.Evaluate(lightPercent);

        if (fadeTime != -1)
        {
            float percentDead = age / fadeTime;
            lightRef.intensity = Mathf.Lerp(initialLightIntensity, 0f, percentDead);
            lightPercent *= percentDead;
        }
        
        return lightPercent;
    }

    float GetDistance ( Vector2 position)
    {
        Vector2 difference = transform.position;
        difference -= position;
        return difference.magnitude;
    }
}

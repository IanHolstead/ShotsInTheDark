using UnityEngine;
using System.Collections.Generic;

public class LightScript : MonoBehaviour {
    float radius;
    List<PlayerLight> characters;

    float fadeTime = -1f;
    float age = 0f;
    float initialLightIntensity;

    public AnimationCurve LightFalloff;

    private const float LIGHT_TO_COLLIDER_RADUIS = 2.24f;

    //Add light ref

	void Start () {
        characters = new List<PlayerLight>();
        //makes the detection object the same size as the light
        radius = GetComponent<Light>().range;
        GetComponent<CircleCollider2D>().radius = radius*LIGHT_TO_COLLIDER_RADUIS;
        initialLightIntensity = GetComponent<Light>().intensity;
    }
	
	void Update () {
        age += Time.deltaTime;
        List<PlayerLight> toRemove = null;
        //TODO: what is the null check for?
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
            Logger.Log("Added: " + other.GetComponent<PlayerPawn>().PlayerIndex, this, LogLevel.Log);

            characters.Add(other.GetComponent<PlayerLight>());
        }
    }

    //TODO: this looks dated
    void OnTriggerExit2D(Collider2D other)
    {
        //TODO: Tags are the worst
        if (other.gameObject.tag == "Player")
        {
            Logger.Log("Removed: " + other.GetComponent<PlayerPawn>().PlayerIndex, this, LogLevel.Log);
            other.GetComponentInParent<PlayerLight>().removeLight(this);
            characters.Remove(other.GetComponent<PlayerLight>());
        }
    }

    //For removing dead players
    public void RemoveCharacter(PlayerLight character)
    {
        characters.Remove(character);
        Logger.Log("Removed: " + character, this, LogLevel.Log);
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
	
    //TODO: remove me
	//void OnDestroy()
	//{
 //       foreach (characterLight character in characters)
 //       {
 //           character.removeLight(this);
 //       }
 //   }

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
            //TODO: use light ref
            GetComponent<Light>().intensity = Mathf.Lerp(initialLightIntensity, 0f, percentDead);
            lightPercent *= percentDead;
        }
        
        return lightPercent;
    }

    float GetDistance ( Vector2 position)
    {
        Vector2 difference = this.transform.position;
        difference -= position;
        return difference.magnitude;
    }
}

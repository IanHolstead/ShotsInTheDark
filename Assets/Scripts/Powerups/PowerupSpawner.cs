using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupSpawner : MonoBehaviour {

    float randomSpawnTime;
    float age = 0f;
    GameObject pickupToSpawn;
    bool powerupOnLevel = false;
    public float earliestSpawn = 0f;
    public float latestSpawn = 25f;

    //public GameObject LightEmUp;
    //public GameObject MoreArrows;
    //public GameObject BetterBow;

    public GameObject[] powerups;

	public AudioClip sound;
	public AudioClip got;


    void Start () {
        //init();
        Setup();
    }

    //void init()
    //{
    //    powerups.Add(LightEmUp);
    //    powerups.Add(MoreArrows);
    //    powerups.Add(BetterBow);
    //}

    void Setup()
    {
        PowerupManager.pickupSpawned += PickupSpawned;
        PowerupManager.pickupPickedup += PickupPickedup;

        if (powerups.Length == 0)
        {
            Logger.Log("No Powerups!", this, LogLevel.Error);
            Destroy(gameObject);
            return;
        }
        randomSpawnTime = Random.Range(earliestSpawn, latestSpawn);
        pickupToSpawn = powerups[Random.Range(0, powerups.Length - 1)];
        Logger.Log(pickupToSpawn, this, LogLevel.Log);
    }

    void Update () {
        if (!powerupOnLevel)
        {
            age += Time.deltaTime;
        }
        
        if (age >= randomSpawnTime)
        {
            PowerupManager.pickupSpawned();
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(pickupToSpawn, transform.position, transform.rotation);
		AudioSource.PlayClipAtPoint(sound,transform.position, 0.25f);
    }

    void PickupPickedup()
    {
        powerupOnLevel = false;
		AudioSource.PlayClipAtPoint(got,transform.position, 0.25f);
        Setup();
    }

    public void PickupSpawned()
    {
        powerupOnLevel = true;
        age = 0;
    }
}

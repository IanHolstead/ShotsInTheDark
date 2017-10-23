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

    public GameObject[] powerups;

    //POLISH: There should be a way to control the spawn rates per powerup

	public AudioClip sound;
	public AudioClip got;


    void Start () {
        Setup();
    }

    void Setup()
    {
        PowerupManager.pickupSpawned += PickupSpawned;
        PowerupManager.pickupPickedup += PickupPickedup;

        if (powerups.Length == 0)
        {
            Logger.Log("No Powerups!", this, LogLevel.Warning);
            Destroy(gameObject);
            return;
        }
        randomSpawnTime = ChooseSpawnTime();
        Logger.Log(pickupToSpawn, this, LogLevel.Verbose);
    }

    void Update () {
        if (!powerupOnLevel)
        {
            age += Time.deltaTime;
        }
        
        if (age >= randomSpawnTime)
        {
            PowerupManager.pickupSpawned();
            pickupToSpawn = ChoosePickup();
            Spawn();
        }
    }

    //POLISH: this is very niave. should consider how the game is going.
    private float ChooseSpawnTime()
    {
        return Random.Range(earliestSpawn, latestSpawn);
    }

    //POLISH: this is very niave. should consider how the game is going.
    private GameObject ChoosePickup()
    {
        return powerups[Random.Range(0, powerups.Length - 1)];
    }

    /* POLISH: This code works and all, but there isn't any way to tune the game play
    * ultimately, logic for how pickups are spawned is going to have to be moved into
    * its own class that considers all the spawners and how the game is going and stuff 
    */
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

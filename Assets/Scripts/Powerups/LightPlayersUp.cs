using UnityEngine;
using System.Collections;

public class LightPlayersUp : MonoBehaviour {

    public GameObject playerToStalk;
    public float lifeSpan;
    float age = 0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
        age += Time.deltaTime;
        if (age <= 1f)
        {
            transform.position = Vector3.Lerp(transform.position, playerToStalk.transform.position + new Vector3(0, 0, -1), age);
        }
        else
        {
            transform.position = playerToStalk.transform.position + new Vector3(0, 0, -1);
            if (age / lifeSpan >= .9f)
            {
                GetComponent<LightScript>().FadeAway(lifeSpan - age);
            }
        }
    }
}

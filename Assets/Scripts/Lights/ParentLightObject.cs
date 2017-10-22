using UnityEngine;

//TODO: this isn't used
public abstract class ParentLightObject : MonoBehaviour
{
    public float defaultFadeTime = .25f;

    public virtual float GetFadeTime()
    {
        return defaultFadeTime;
    }

    public void FadeObjectAway()
    {
        Destroy(gameObject, GetFadeTime());
        GetComponentInChildren<LightScript>().FadeAway(GetFadeTime());
        //TODO: this looks wrong
        GetComponent<SpriteRenderer>().enabled = false;//color = new Color(255, 255, 255, 0);
    }
}

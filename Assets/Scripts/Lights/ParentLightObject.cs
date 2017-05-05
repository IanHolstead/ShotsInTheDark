using UnityEngine;

//TODO: this isn't used
public abstract class ParentLightObject : MonoBehaviour
{

    public float defaultFadeTime = .25f;

    public virtual float getFadeTime()
    {
        return defaultFadeTime;
    }

    public void fadeObjectAway()
    {
        Destroy(gameObject, getFadeTime());
        GetComponentInChildren<LightScript>().FadeAway(getFadeTime());
        //TODO: this looks wrong
        GetComponent<SpriteRenderer>().enabled = false;//color = new Color(255, 255, 255, 0);
    }
}

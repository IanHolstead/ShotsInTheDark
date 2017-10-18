using System.Collections.Generic;

/// <summary>
/// This is a data only class ment to fetch prefs from file
/// </summary>
public class PlayerProfile {
    string name;
    KeyBinding keyBinding;

    public PlayerProfile()
    {
        keyBinding = new KeyBinding();
    }

    public KeyBinding KeyBinding
    {
        get
        {
            return keyBinding;
        }
    }
}

﻿using System.Collections.Generic;

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
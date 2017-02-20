using UnityEngine;
using System.Collections.Generic;

static class PowerupManager
{
    public delegate void pickupSpawnedDel();
    public delegate void pickupPickedupDel();


    public static pickupSpawnedDel pickupSpawned;
    public static pickupPickedupDel pickupPickedup;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Garbage System / Garbage Spawner Data", fileName = "Garbage Spawner Data")]
public class GarbageSpawnerData : ScriptableObject
{
    public List<GarbageData> Garbages;
}
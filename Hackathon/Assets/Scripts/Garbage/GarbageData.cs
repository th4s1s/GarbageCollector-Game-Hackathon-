using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Garbage System / Garbage Data", fileName = "Garbage Data")]
public class GarbageData : ScriptableObject
{
    public string ID;
    public Sprite Icon;
    public int Price;
    public GarbageType Type;
    public int PollutionAmount;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrashInventory : MonoBehaviour
{
    public static TrashInventory Instance {get; private set;}  
    public List <TrashItem> trashList;

    void Awake()
    {
        Instance = this;
    }
    void FixedUpdate()
    {
        int trashCountListSize = Player.Instance.trashCountList.Count;
        for (int i=0; i<trashCountListSize; i++)
        {
            // Debug.Log(Player.Instance.trashCountList[i].ToString());
            trashList[i].amountTrashText.text = Player.Instance.trashCountList[i].ToString();
        }
    }
}

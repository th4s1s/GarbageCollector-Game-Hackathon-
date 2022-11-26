using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrashInventory : MonoBehaviour
{
    public List <TrashItem> trashList;
    void Start()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        // int trashCountListSize = this.trashCountList.Count;
        // Debug.Log(trashCountListSize);
        int trashCountListSize = 1;
        for (int i=0; i<trashCountListSize; i++)
        {
            // trashList[i].amountTrashText.text = player.trashCountList[i].ToString();
            trashList[i].amountTrashText.text = "10";
        }
    }
}

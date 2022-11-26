using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TrashInventory : MonoBehaviour
{
    public static TrashInventory Instance {get; private set;}  
    public List <TrashItem> trashList;
    public Text currNum;

    void Awake()
    {
        Instance = this;
    }
    void FixedUpdate()
    {
        int trashCountListSize = Player.Instance.trashCountList.Count;
        for (int i=0; i<trashCountListSize; i++)
        {
            trashList[i].amountTrashText.text = Player.Instance.trashCountList[i].ToString();
            currNum.text = Player.Instance.currentTrash.ToString();
        }
    }
}

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
    public void UpdateTrashCount(int i)
    {
        trashList[i].amountTrashText.text = Player.Instance.trashCountList[i].ToString();
        currNum.text = Player.Instance.currentTrash.ToString();
        trashList[i].GetComponentInChildren<Animator>().Play("number");
    }
}

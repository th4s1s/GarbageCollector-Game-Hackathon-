using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashItem : MonoBehaviour
{
    public Image spriteTrash;
    public bool isCollected;
    public Text amountTrashText;
    public GarbageData garbageData;

    void Update()
    {
        if (ShopMachine.Instance.isOpeningMachine == false) this.GetComponent<DragTrash>().enabled = false;
        else this.GetComponent<DragTrash>().enabled = true;
    }

    public void UpdateSprite()
    {
        if (isCollected == false)
        {
            spriteTrash = null; // dau cham hoi
        }
        else
        {
            spriteTrash = null; // da co hinh anh
        }
    }

}

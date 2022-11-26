using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantListButton : MonoBehaviour
{
    [SerializeField] int i;
    public void PlayerGoPlantID(int idx)
    {
        if (Player.Instance.isNear == false) 
            Player.Instance.PlantTree(idx);
    }

    void Update()
    {
        this.GetComponentInChildren<Text>().text = Player.Instance.treeCountList[i].ToString();
    }
}

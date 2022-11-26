using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButton : MonoBehaviour
{
    public GameObject plantList;
    bool isOpenThePlantList;
    void Start()
    {
        plantList.SetActive(false);
        isOpenThePlantList = false;
    }
    public void PlayerGoPlant()
    {
        if (isOpenThePlantList == false)
        {
            isOpenThePlantList = true;
            if (Player.Instance.isPlantTree == false)
            {
                Player.Instance.isPlantTree = true;
                Player.Instance.PreparePlant(0);
                plantList.SetActive(true);
            }
        }
        else 
        {
            isOpenThePlantList = false;
            plantList.SetActive(false);
            Player.Instance.isPlantTree = false;
            Player.Instance.PlantTree(-1);
        }
    }
}

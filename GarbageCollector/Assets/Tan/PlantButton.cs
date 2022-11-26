using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButton : MonoBehaviour
{
    public void PlayerGoPlant()
    {
        // gọi instance của player để xuất hiện cái vùng xanh/đỏ
        if (Player.Instance.isPlantTree == false)
        {
            Player.Instance.isPlantTree = true;
            Player.Instance.PreparePlant(1);
        }
    }
}

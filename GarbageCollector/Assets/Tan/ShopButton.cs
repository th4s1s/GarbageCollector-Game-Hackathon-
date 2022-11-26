using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public void BuyTree(int i)
    {
        int requireCoin = 0;
        switch(i) 
        {
            case 0:
                requireCoin = 10;
                break;
            case 1:
                requireCoin = 20;
                break;
            case 2:
                requireCoin = 30;
                break;
        }
        if (Player.Instance.coinInStage >= requireCoin)
        {
            Player.Instance.treeCountList[i]++;
            Player.Instance.coinInStage -= requireCoin;
        }
    }

    public void BuyRange()
    {
        // Player.Instance.
        if (Player.Instance.coinInStage >= 50)
        {
            // Player.Instance.capacityTrash += 10;
            Player.Instance.coinInStage -= 50;
        }
    }

    public void BuyCapacity()
    {
        if (Player.Instance.coinInStage >= 50)
        {
            Player.Instance.capacityTrash += 10;
            Player.Instance.coinInStage -= 50;
        }
    }
}

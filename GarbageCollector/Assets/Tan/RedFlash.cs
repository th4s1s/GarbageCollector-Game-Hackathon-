using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFlash : MonoBehaviour
{
    [SerializeField] GameObject redFlash;
    void Update()
    {
        if (GameController.Instance.Oxy <= 10 && GameController.Instance.Oxy > 0)
        {
            redFlash.SetActive(true);
        }
        else
        {
            redFlash.SetActive(false);
        }
    }
}

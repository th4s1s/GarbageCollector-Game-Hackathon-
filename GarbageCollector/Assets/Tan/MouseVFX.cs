using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVFX : MonoBehaviour
{
    GameObject mouseEffect;
    void Start()
    {
        mouseEffect = this.gameObject;
    }
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseEffect.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
    }
}

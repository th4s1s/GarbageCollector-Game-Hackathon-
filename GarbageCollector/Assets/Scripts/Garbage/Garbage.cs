using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public string id;
    public Sprite icon { get; private set; }
    public int price { get; private set; }
    public GarbageType type { get; private set; }
    public int pollutionAmount { get; private set; }

    void Start()
    {
        gameObject.transform.localScale = Vector3.zero;
    }

    void FixedUpdate()
    {
        if(gameObject.transform.localScale.x < 1) gameObject.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
    }

    public void Setup(string _id, Sprite _icon, int _price, GarbageType _type, int _pollutionAmount)
    {
        this.id = _id;
        this.icon = _icon;
        this.price = _price;
        this.type = _type;
        this.pollutionAmount = _pollutionAmount;

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = _icon;
    }
}

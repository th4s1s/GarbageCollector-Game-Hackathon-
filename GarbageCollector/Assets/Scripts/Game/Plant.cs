using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Plant : MonoBehaviour
{
    [SerializeField] Sprite[] stages = new Sprite[3];
    [SerializeField] int oxy;
    [SerializeField] float timeToGrow;
    [SerializeField] float powerScale;
    
    float timePlant;
    float timeSpawnOxy=0;
    GameObject plant;
    SpriteRenderer spriteRenderer;
    Transform size;
    // Start is called before the first frame update
    void Start()
    {
        plant = gameObject;
        spriteRenderer = plant.GetComponent<SpriteRenderer>();
        size = plant.GetComponent<Transform>();

        spriteRenderer.sprite = stages[0];
        timePlant = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - timePlant<=timeToGrow+0.2f)
        {
            if (Time.time - timePlant >= timeToGrow / 2 && Time.time - timePlant < timeToGrow)
            {
                float timeVar = Time.time - timePlant - timeToGrow / 2;
                float scaleFunction = (-100 * powerScale + 100) * timeVar * timeVar + (20 * powerScale - 20) * timeVar + 1;
                if (timeVar <= 0.2f) size.localScale = new Vector3(scaleFunction, scaleFunction, 1);
                if (timeVar>=0.1f) spriteRenderer.sprite = stages[1];
            }
            if (Time.time - timePlant >= timeToGrow)
            {
                float timeVar = Time.time - timePlant - timeToGrow;
                float scaleFunction = (-100 * powerScale +100 ) * timeVar * timeVar + (20 * powerScale - 20) * timeVar + 1;
                if (timeVar <= 0.2f) size.localScale = new Vector3(scaleFunction, scaleFunction, 1);
                if (timeVar >=0.1f) spriteRenderer.sprite = stages[2];
            }
        }
        if (spriteRenderer.sprite == stages[2])
        {
            if (Time.time>=timeSpawnOxy+10)
            {
                timeSpawnOxy = Time.time;
                GameController.Instance.Oxy += oxy;
            }
        }
    }
    
}

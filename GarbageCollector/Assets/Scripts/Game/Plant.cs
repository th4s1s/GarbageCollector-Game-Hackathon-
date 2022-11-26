using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Plant : MonoBehaviour
{
    [SerializeField] int oxy;
    [SerializeField] float timeToGrow;
    [SerializeField] float powerScale;
    Collider2D col;
    public bool isMature = false;
    public bool isDead = false;
    float timePlant;
    float timeSpawnOxy=0;
    GameObject plant;
    Animator anim;
    Transform size;
    // Start is called before the first frame update
    void Start()
    {
        plant = gameObject;
        anim = plant.GetComponent<Animator>();
        size = plant.GetComponent<Transform>();
        col = plant.GetComponent<Collider2D>();
        col.isTrigger = true;
        anim.Play("Stage_1");
        timePlant = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDead)
        {
            if (Time.time - timePlant <= timeToGrow + 0.2f)
            {
                if (Time.time - timePlant >= timeToGrow / 2 && Time.time - timePlant < timeToGrow)
                {
                    float timeVar = Time.time - timePlant - timeToGrow / 2;
                    float scaleFunction = (-100 * powerScale + 100) * timeVar * timeVar + (20 * powerScale - 20) * timeVar + 1;
                    if (timeVar <= 0.2f) size.localScale = new Vector3(scaleFunction, scaleFunction, 1);
                    if (timeVar >= 0.1f) { anim.Play("Stage_2"); col.isTrigger = false; }
                }
                if (Time.time - timePlant >= timeToGrow)
                {
                    float timeVar = Time.time - timePlant - timeToGrow;
                    float scaleFunction = (-100 * powerScale + 100) * timeVar * timeVar + (20 * powerScale - 20) * timeVar + 1;
                    if (timeVar <= 0.2f) size.localScale = new Vector3(scaleFunction, scaleFunction, 1);
                    if (timeVar >= 0.1f) anim.Play("Stage_3");
                    isMature = true;
                }
            }
            if (isMature)
            {
                if (Time.time >= timeSpawnOxy + 10)
                {
                    timeSpawnOxy = Time.time;
                    GameController.Instance.Oxy += oxy;
                }
            }
        }
        if (isDead)
        {
            anim.Play("Dead");
            Destroy(this);
        }
    }
    void Dead() => Destroy(gameObject);
}

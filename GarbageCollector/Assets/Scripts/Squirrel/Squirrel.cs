using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Squirrel : MonoBehaviour
{
    [SerializeField] Vector2 roamingZoneX;
    [SerializeField] Vector2 roamingZoneY;
    [SerializeField] float speed;
    private Vector2 target;
    GameObject treeToDestroy;
    Transform player;
    private bool isRoam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        treeToDestroy = null;
        isRoam = true;
        target = new Vector2(Random.Range(roamingZoneX.x, roamingZoneX.y), Random.Range(roamingZoneY.x, roamingZoneY.y));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = transform.position - player.position;
        if (dir.magnitude < 2f)
        {
            StopAllCoroutines();
            isRoam = true;
            transform.Translate(dir.normalized * speed * Time.deltaTime);
            target = new Vector2(Random.Range(roamingZoneX.x, roamingZoneX.y), Random.Range(roamingZoneY.x, roamingZoneY.y));
        }
        else if (isRoam)
        {
            if (Vector2.Distance((Vector2)transform.position, target) < 0.02f)
            {
                Decide();
            }
            else
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target, step);
            }
        }
        else
        {
            Debug.Log("Tree");
            RunToTree();
        }
    }

    void RunToTree()
    {
        if(Vector2.Distance((Vector2)transform.position, target) < 0.01f)
        {
            StartCoroutine(DestroyTree(treeToDestroy));
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }

    IEnumerator DestroyTree(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        Destroy(obj);
        Decide();
    }

    void Decide()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        if (trees.Length == 0)
        {
            isRoam = true;
            target = new Vector2(Random.Range(roamingZoneX.x, roamingZoneX.y), Random.Range(roamingZoneY.x, roamingZoneY.y));
        }
        else
        {
            foreach(GameObject obj in trees)
            {
                if(obj.GetComponent<Plant>()?.isMature == true)
                {
                    Debug.Log("found");
                    isRoam = false;
                    treeToDestroy = obj;
                    target = obj.transform.position;
                    return;
                }
            }
        }
        isRoam = true;
        target = new Vector2(Random.Range(roamingZoneX.x, roamingZoneX.y), Random.Range(roamingZoneY.x, roamingZoneY.y));
    }
}

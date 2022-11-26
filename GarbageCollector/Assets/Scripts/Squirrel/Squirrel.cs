using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Squirrel : MonoBehaviour
{
    public Vector2 roamingZoneX;
    public Vector2 roamingZoneY;
    private float speed;
    private Vector2 target;
    GameObject treeToDestroy;
    Transform player;
    private bool isRoam;
    private bool yeet;
    [SerializeField] Animator anim;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        treeToDestroy = null;
        isRoam = true;
        yeet = false;
        speed = 6f;
        target = new Vector2(Random.Range(roamingZoneX.x, roamingZoneX.y), Random.Range(roamingZoneY.x, roamingZoneY.y));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = transform.position - player.position;
        if(yeet)
        {
            if (dir.magnitude > 15f)
            {
                SquirrelSpawner.Instance.Cast(transform.position);
                Destroy(gameObject);
            }
            if (dir.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(dir.normalized * speed * Time.deltaTime);
        }
        else if (dir.magnitude < 2f)
        {
            StopAllCoroutines();
            anim.SetBool("run", true);
            isRoam = true;
            yeet = true;
            if (dir.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(dir.normalized * speed * Time.deltaTime);
            target = new Vector2(Random.Range(roamingZoneX.x, roamingZoneX.y), Random.Range(roamingZoneY.x, roamingZoneY.y));
        }
        else if (isRoam)
        {
            if (Vector2.Distance((Vector2)transform.position, target) < 0.05f)
            {
                Decide();
            }
            else
            {
                Vector2 pos = target - (Vector2) transform.position;
                if (pos.x < 0) transform.localScale = new Vector3(-1, 1, 1);
                else transform.localScale = new Vector3(1, 1, 1);
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target, step);
            }
        }
        else
        {
            RunToTree();
        }
    }

    void RunToTree()
    {
        if(Vector2.Distance((Vector2)transform.position, target) < 0.025f)
        {
            anim.SetBool("run", false);
            StartCoroutine(DestroyTree(treeToDestroy));
        }
        else
        {
            Vector2 pos = target - (Vector2)transform.position;
            if (pos.x < 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }
    }

    IEnumerator DestroyTree(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        if(obj.GetComponent<Plant>() != null) obj.GetComponent<Plant>().isDead = true;
        anim.SetBool("run", true);
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

    IEnumerator FadeOut()
    {
        for(float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = sprite.material.color;
            c.a = f;
            sprite.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        SquirrelSpawner.Instance.Cast(transform.position);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(FadeOut());
    }
}

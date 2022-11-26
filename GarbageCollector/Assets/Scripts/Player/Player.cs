using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public enum Weaption
    {
        none,
        vending,
    }

    [SerializeField] float speed;
    [SerializeField] Animator anim;

    float xdir, ydir;
    [SerializeField] bool isWalking;
    public bool isPlantTree;

    public List<int> trashCountList = new List<int>();

    public Weaption weaption;
    public float collectRadius, checkTreeRadius;

    [SerializeField] LayerMask trashLayer, treeLayer;

    [SerializeField] SpriteRenderer plantTreeRangeSp;
    [SerializeField] GameObject collectRangeObj;

    [SerializeField] List<GameObject> treeList = new List<GameObject>();
    int treeID;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        isPlantTree = false;
        isWalking = false;
        collectRangeObj.transform.localScale = new Vector3(1, 1, 1) * collectRadius/3;
    }
    private void Update()
    {
        collectRangeObj.transform.localScale = new Vector3(1, 1, 1) * collectRadius / 1.7f;
        xdir = Input.GetAxisRaw("Horizontal");
        ydir = Input.GetAxisRaw("Vertical");

        if (xdir != 0 || ydir != 0) isWalking = true;
        else isWalking = false;
        //anim.SetBool("isWalking", isWalking);
        if (isWalking) Walking();

        if (xdir > 0) transform.localScale = new Vector3(1, 1, 1);
        if (xdir < 0) transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space)) CheckTrash();
        
        // if (Input.GetKeyDown(KeyCode.X) && isPlantTree == false)
        // {
        //     isPlantTree = true;
        //     PreparePlant();
        // }

        if (isPlantTree)
        {
            Debug.Log(IsTreeNear());
            if (Input.GetKeyDown(KeyCode.Z) && IsTreeNear()==false) PlantTree();
        }

        if (isWalking)
        {
            anim.Play("Walk");
        }
        else
        {
            anim.Play("Idle");
        }
    }

    void Walking()
    {
        transform.Translate(xdir * speed * Time.fixedDeltaTime, ydir * speed * Time.fixedDeltaTime, 0);

        
        //anim.SetFloat("xSpeed", xdir);
        //anim.SetFloat("ySpeed", ydir);
    }

    IEnumerator ICollect(GameObject obj)
    {
        while (obj.transform.position != transform.position)
        {
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, transform.position, 10 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        if (obj.GetComponent<Garbage>().id == "Banana") trashCountList[0]++;
        if (obj.GetComponent<Garbage>().id == "Paper") trashCountList[1]++;
        GarbageSpawner.Instance.ChangePollutionMeter(-obj.GetComponent<Garbage>().pollutionAmount);
        Destroy(obj);
        yield return new WaitForFixedUpdate();
    }


    void CheckTrash()
    {
        Debug.Log("CheckTrash");
        Collider2D coll = Physics2D.OverlapCircle(transform.position, collectRadius, trashLayer);
        if (coll != null)
        {
            StartCoroutine(ICollect(coll.gameObject));
        }
    }

    public void PlantTree()
    {
        plantTreeRangeSp.gameObject.SetActive(false);
        isPlantTree = false;
        Debug.Log("Plant");
        for(int i =0; i<treeList.Count; i++)
        {
            if (i == treeID) Instantiate(treeList[i], transform.position, Quaternion.identity);
        }
        //Instantiate(tree, transform.position, Quaternion.identity);
    }

    public void PreparePlant(int treeID)
    {
        this.treeID = treeID;
        plantTreeRangeSp.gameObject.SetActive(true);
        Debug.Log("Press Z to plant!");
    }

    bool IsTreeNear()
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, checkTreeRadius, treeLayer);
        if (coll != null)
        {
            plantTreeRangeSp.color = new Color(1, 0, 0, 0.25f);
            return true;
        }
        else
        {
            plantTreeRangeSp.color = new Color(0, 1, 0, 0.25f);
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, collectRadius);
    }
}

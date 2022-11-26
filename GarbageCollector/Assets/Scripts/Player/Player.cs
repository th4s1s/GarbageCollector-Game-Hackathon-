using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public int coinInStage = 0;
    [SerializeField] float speed;
    [SerializeField] Animator anim;

    float xdir, ydir;
    [SerializeField] bool isWalking;
    public bool isPlantTree, isCanMove;
    public int currentTrash, capacityTrash;

    public List<int> trashCountList = new List<int>() {0,0,0,0,0,0,0};

    public float collectRadius, checkTreeRadius;

    [SerializeField] LayerMask trashLayer, treeLayer;

    [SerializeField] SpriteRenderer plantTreeRangeSp;
    [SerializeField] GameObject collectRangeObj;

    [SerializeField] List<GameObject> treeList = new List<GameObject>() {null, null, null};
    public List<int> treeCountList = new List<int>() {0, 0, 0};
    public int treeID;
    public bool isNear;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        isPlantTree = false;
        isWalking = false;
        isCanMove = true;
        capacityTrash = 10;
        collectRangeObj.transform.localScale = new Vector3(1, 1, 1) * collectRadius/1.7f;
    }
    private void Update()
    {
        if (isCanMove)
        {
            xdir = Input.GetAxisRaw("Horizontal");
            ydir = Input.GetAxisRaw("Vertical");

            if (xdir != 0 || ydir != 0) isWalking = true;
            else isWalking = false;
            //anim.SetBool("isWalking", isWalking);
            if (isWalking) Walking();

            if (xdir > 0) transform.localScale = new Vector3(1, 1, 1);
            if (xdir < 0) transform.localScale = new Vector3(-1, 1, 1);

            if (Input.GetKeyDown(KeyCode.Space)) CheckTrash();


            if (isPlantTree)
            {
                isNear = IsTreeNear();
            }

            if (Input.GetKeyDown(KeyCode.L)) GameController.Instance.Lose();

            if (isWalking)
            {
                anim.Play("Walk");
            }
            else
            {
                anim.Play("Idle");
            }
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
        while (Vector3.Magnitude(obj.transform.position - transform.position)>0.2f)
        {
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, transform.position, 10 * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        int idx = 0;
        if (obj.GetComponent<Garbage>().id == "0") idx = 0;
        if (obj.GetComponent<Garbage>().id == "1") idx = 1;
        if (obj.GetComponent<Garbage>().id == "2") idx = 2;
        if (obj.GetComponent<Garbage>().id == "3") idx = 3;
        if (obj.GetComponent<Garbage>().id == "4") idx = 4;
        if (obj.GetComponent<Garbage>().id == "5") idx = 5;
        if (obj.GetComponent<Garbage>().id == "6") idx = 6;
        trashCountList[idx]++;
        currentTrash++;
        GarbageSpawner.Instance.ChangePollutionMeter(-obj.GetComponent<Garbage>().pollutionAmount);
        Destroy(obj);
        yield return new WaitForFixedUpdate();
    }


    void CheckTrash()
    {
        // Debug.Log("CheckTrash");
        Collider2D coll = Physics2D.OverlapCircle(transform.position, collectRadius, trashLayer);
        if (coll != null)
        {
            if (currentTrash < capacityTrash)
                StartCoroutine(ICollect(coll.gameObject));
            else 
                Debug.Log("FULL");
        }
    }

    public void PlantTree(int i)
    {
        plantTreeRangeSp.gameObject.SetActive(false);
        isPlantTree = false;
        // for(int i =0; i<treeList.Count; i++)
        // {
            if (treeCountList[i]>0) 
            {
                Instantiate(treeList[i], transform.position, Quaternion.identity);
                treeCountList[i]--;
            }
        // }
        //Instantiate(tree, transform.position, Quaternion.identity);
    }

    public void PreparePlant(int treeID)
    {
        this.treeID = treeID;
        plantTreeRangeSp.gameObject.SetActive(true);
        // Debug.Log("Press Z to plant!");
    }

    public bool IsTreeNear()
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

    public void Die()
    {
        isCanMove = false;
        anim.Play("Die");
    }

    public void SetCollectRange(float rangeAdd)
    {
        collectRadius += rangeAdd;
        collectRangeObj.transform.localScale = new Vector3(1, 1, 1) * collectRadius / 1.7f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, collectRadius);
    }
}

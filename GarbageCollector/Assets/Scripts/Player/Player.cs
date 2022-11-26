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

    public int coinInStage;
    [SerializeField] float speed;
    [SerializeField] Animator anim;

    float xdir, ydir;
    [SerializeField] bool isWalking;
    public bool isPlantTree;
    public int currentTrash, capacityTrash;

    public List<int> trashCountList = new List<int>() {0,0,0,0,0,0,0};

    public Weaption weaption;
    public float collectRadius, checkTreeRadius;

    [SerializeField] LayerMask trashLayer, treeLayer;

    [SerializeField] SpriteRenderer plantTreeRangeSp;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        isPlantTree = false;
        isWalking = false;
        capacityTrash = 10;
    }
    private void Update()
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
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, transform.position, 5 * Time.fixedDeltaTime);
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

    public void PlantTree()
    {
        plantTreeRangeSp.gameObject.SetActive(false);
        isPlantTree = false;
        Debug.Log("Plant");
        //Instantiate(tree, transform.position, Quaternion.identity);
    }

    public void PreparePlant()
    {
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
        Gizmos.DrawSphere(transform.position, checkTreeRadius);
    }
}

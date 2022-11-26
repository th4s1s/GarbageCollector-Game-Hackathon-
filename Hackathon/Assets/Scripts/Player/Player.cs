using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Weaption
    {
        none,
        vending,
    }

    [SerializeField] float speed;
    [SerializeField] Animator anim;

    float xdir, ydir;
    bool isWalking;

    public List<int> trashCountList = new List<int>();

    public Weaption weaption;
    public float collectRadius;

    [SerializeField] LayerMask trashLayer;


    private void Update()
    {
        xdir = Input.GetAxisRaw("Horizontal");
        ydir = Input.GetAxisRaw("Vertical");

        if (xdir != 0 || ydir != 0) isWalking = true;
        else isWalking = false;
        //anim.SetBool("isWalking", isWalking);
        if (isWalking) Walking();

        if (xdir > 0) transform.localScale = new Vector3(-1, 1, 1);
        if (xdir < 0) transform.localScale = new Vector3(1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space)) CheckTrash();
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, collectRadius);
    }
}

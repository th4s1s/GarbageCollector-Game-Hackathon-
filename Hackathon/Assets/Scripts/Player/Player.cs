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

    public Weaption weaption;

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


    }

    void Walking()
    {
        transform.Translate(xdir * speed * Time.fixedDeltaTime, ydir * speed * Time.fixedDeltaTime, 0);

        //anim.SetFloat("xSpeed", xdir);
        //anim.SetFloat("ySpeed", ydir);
    }

    void Collect()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}

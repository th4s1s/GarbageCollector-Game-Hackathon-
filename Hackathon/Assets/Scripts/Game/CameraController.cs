using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    Animator anim;
    public Transform followingTarget;
    public float maxX = 0, minX = 0, maxY = 0, minY = 0;
    public float moveSpeed;

    public bool isFollowTarget = true;
    //bool isScoll = false;

    //Rigidbody2D rigi;

    [HideInInspector]
    public GameObject targetObj;
    Vector3 target;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        anim = gameObject.GetComponent<Animator>();
        //rigi = obj.GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        isFollowTarget = true;
        target = gameObject.transform.position;
        targetObj = followingTarget.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowTarget)
            FollowTarget();

        //Test camera animation
        if (Input.GetKeyDown(KeyCode.Z)) Shake();

        //if (isFollowPlayer == false) Invoke("FollowPlayer", 0.5f);
    }

    void FollowTarget()
    {
        if (targetObj==null)
        {
            Debug.LogError("Camera follow to null object");
        }
        else
        {
            // Tao camera di chuyen theo targetObj
            target.x = targetObj.transform.position.x;
            target.y = targetObj.transform.position.y;

            if (target.x < minX) target.x = minX;
            if (target.x > maxX) target.x = maxX;
            if (target.y < minY) target.y = minY;
            if (target.y > maxY) target.y = maxY;

            transform.position = Vector3.Lerp(transform.position, target, moveSpeed);
        }
    }

    public Vector2 CameraPos
    {
        get { return transform.position; }
        set
        {
            transform.position = value;
            transform.position -= new Vector3(0, 0, 10);
        }
    }

    public void Shake() //rung
    {
        anim.Play("Shake");
    }

    public void LightShake() //rung nhe
    {
        anim.Play("LightShake");
    }

    public void SlideRight() //cuon phai
    {
        anim.Play("SlideRight");
        isFollowTarget = false;
    }

    public void SlideLeft()
    {
        anim.Play("SlideLeft");
        isFollowTarget = false;
    }

    public void ZoomIn()
    {
        anim.Play("ZoomIn");
    }

    public void ZoomOut()
    {
        anim.Play("ZoomOut");
    }

    public void SuperZoomIn()
    {
        anim.Play("SuperZoomIn");
    }

    void SetFollowPlayer()
    {
        isFollowTarget = true;
    }

    //public void Scoll()
    //{
    //    isScoll = true;
    //}
}


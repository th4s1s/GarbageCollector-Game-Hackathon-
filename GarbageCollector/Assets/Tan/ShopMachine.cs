using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMachine : MonoBehaviour
{
    private GameObject player;
    public Text machineUISentence;
    public Image machinePanel;
    private bool canOpenMachine = false;
    public bool isOpeningMachine = false;

    public static ShopMachine Instance {get; private set;}
    void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != null && Instance != this) return;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckDistanceAndOpen();
    }

    void CheckDistanceAndOpen()
    {
        if (Vector2.Distance(player.transform.position, this.transform.position) >= 2f)
        {
            machineUISentence.gameObject.SetActive(false);
            canOpenMachine = false;
        }
        else 
        {
            machineUISentence.gameObject.SetActive(true);
            canOpenMachine = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && canOpenMachine == true)
        {
            isOpeningMachine = true;
            machinePanel.gameObject.SetActive(true);
            machineUISentence.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isOpeningMachine == true)
        {
            isOpeningMachine = false;
            machinePanel.gameObject.SetActive(false);
            machineUISentence.gameObject.SetActive(false);
        }
    }
}
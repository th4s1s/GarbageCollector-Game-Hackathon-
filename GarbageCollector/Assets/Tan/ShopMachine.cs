using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMachine : MonoBehaviour
{
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


    void Update()
    {
        CheckDistanceAndOpen();
    }

    void CheckDistanceAndOpen()
    {
        if (Vector2.Distance(Player.Instance.transform.position, this.transform.position) >= 2f)
        {
            machineUISentence.gameObject.SetActive(false);
            canOpenMachine = false;
        }
        else 
        {
            machineUISentence.gameObject.SetActive(true);
            canOpenMachine = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isOpeningMachine == true) // đóng
        {
            isOpeningMachine = false;
            // machinePanel.GetComponent<Animator>().Play("MachineClose");
            machinePanel.gameObject.SetActive(false);
            machineUISentence.gameObject.SetActive(false);
            Player.Instance.isCanMove = true;
            Time.timeScale = 1;
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && canOpenMachine == true) // mở
        {
            isOpeningMachine = true;
            machinePanel.gameObject.SetActive(true);
            // machinePanel.GetComponent<Animator>().Play("MachineOpen");
            machineUISentence.gameObject.SetActive(false);
            Player.Instance.isCanMove = false;
            Time.timeScale = 0;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    int tutorialIndex = 0;
    [SerializeField] Animator tutorialAnim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextTutorial();
        }
    }

    void NextTutorial()
    {
        string st = "Tutorial" + tutorialIndex.ToString();
        tutorialAnim.Play(st);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    int tutorialIndex = 0;
    [SerializeField] List<GameObject> listPanel = new List<GameObject>();
    [SerializeField] GameObject machineObj;
    [SerializeField] GameObject blackScene;

    private void Start()
    {
        blackScene.SetActive(true);
    }
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
        tutorialIndex++;
        if (tutorialIndex>=listPanel.Count)
        {
            listPanel[tutorialIndex - 1].SetActive(false);
            blackScene.GetComponent<Animator>().Play("BlackSceneEnd");
            Invoke("NextScene", 2);
        }
        if (tutorialIndex == 5 || tutorialIndex == 6) machineObj.SetActive(true);
        else machineObj.SetActive(false);

        listPanel[tutorialIndex].SetActive(true);
        listPanel[tutorialIndex - 1].SetActive(false);
    }

    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

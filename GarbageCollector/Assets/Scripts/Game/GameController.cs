using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField] int maxOxy;
    [SerializeField] int oxy;
    float timeReduceOxy=0;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject flashRiseObj, grass2Tilemap, blackScene;
    public int Oxy
    {
        get { return oxy; }
        set { oxy = value; }
    }
    [SerializeField] int oxyReduceSpeed;
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        blackScene.SetActive(true);
        slider.value = (float)oxy / maxOxy;
    }
    private void FixedUpdate()
    {
        if (Time.time>=timeReduceOxy+5)
        {
            timeReduceOxy = Time.time;
            oxy -= oxyReduceSpeed;
        }
        if (oxy > maxOxy) oxy = maxOxy;
        slider.value = (float)oxy / maxOxy;
        fill.color = gradient.Evaluate(slider.value);
    }

    private void Update()
    {
        if (slider.value <= 0) Lose();
        if (slider.value >= slider.maxValue) Win();

        //if (Input.GetKeyDown(KeyCode.M)) Win();
    }
    public void NextLevel()
    {
        Debug.Log("Go to next level");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart()
    {
        Debug.Log("Restart Level");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Win()
    {
        Debug.Log("Win");
        flashRiseObj.SetActive(true);
        Invoke("SetActiveGrass2",1f);
    }

    public void SetActiveGrass2()
    {
        grass2Tilemap.SetActive(true);
        Invoke("DoBlackSceneEnd", 2);
        Invoke("NextLevel",3);
    }

    public void Lose()
    {
        Player.Instance.Die();
        gameOverPanel.SetActive(true);
        Invoke("DoBlackSceneEnd", 5);
        Invoke("Restart", 6);
        //Todo
    }

    void DoBlackSceneEnd()
    {
        blackScene.GetComponent<Animator>().Play("BlackSceneEnd");
    }
    public void Exit()
    {
        //Todo
    }
    
}

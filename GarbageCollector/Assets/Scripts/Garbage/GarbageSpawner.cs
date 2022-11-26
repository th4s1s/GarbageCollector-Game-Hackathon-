using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] float spawnTime;
    [SerializeField] Vector2 spawnZoneX;
    [SerializeField] Vector2 spawnZoneY;
    [SerializeField] GarbageSpawnerData spawnerData;
    [SerializeField] Garbage garbageTemplate;
    [SerializeField] Slider pollutionMeter;
    [SerializeField] float maxPollution;
    public static GarbageSpawner Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        pollutionMeter.value = 0;
        StartCoroutine(GarbageWave());
    }

    void SpawnGarbage()
    {
        int idx = Random.Range(0, spawnerData.Garbages.Count);
        GameObject garbageInstance = Instantiate(garbageTemplate.gameObject);
        garbageInstance.transform.position = new Vector2(Random.Range(spawnZoneX.x, spawnZoneX.y), Random.Range(spawnZoneY.x, spawnZoneY.y));
        garbageInstance.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        garbageInstance.GetComponent<Garbage>().Setup(spawnerData.Garbages[idx].ID, spawnerData.Garbages[idx].Icon, spawnerData.Garbages[idx].Price, spawnerData.Garbages[idx].Type, spawnerData.Garbages[idx].PollutionAmount);
        ChangePollutionMeter(spawnerData.Garbages[idx].PollutionAmount);
    }

    IEnumerator GarbageWave()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnGarbage();
        }
    }

    public void ChangePollutionMeter(int amount)
    {
        float val = pollutionMeter.value + ((float)amount / maxPollution * 1.0f);
        if (val < 0) val = 0;
        pollutionMeter.value = val;
    }

}

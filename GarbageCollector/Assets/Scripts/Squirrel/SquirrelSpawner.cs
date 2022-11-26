using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelSpawner : MonoBehaviour
{
    public static SquirrelSpawner Instance;
    [SerializeField] GameObject squirrel;
    [SerializeField] Vector2 spawnZoneX;
    [SerializeField] Vector2 spawnZoneY;
    [SerializeField] int number;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        for(int i = 0; i < number; i++)
        {
            Create(new Vector2(Random.Range(spawnZoneX.x, spawnZoneX.y), Random.Range(spawnZoneY.x, spawnZoneY.y)));
        }
    }
    void Create(Vector2 location)
    {
        GameObject ins = Instantiate(squirrel);
        ins.transform.position = location;
        Squirrel obj = ins.GetComponent<Squirrel>();
        obj.roamingZoneX = spawnZoneX;
        obj.roamingZoneY = spawnZoneY;
    }
    IEnumerator Spawn(Vector2 location)
    {
        yield return new WaitForSeconds(5);
        Create(location);
    }
    public void Cast(Vector2 location)
    {
        StartCoroutine(Spawn(location));
    }
}

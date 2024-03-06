using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameWatcher : MonoBehaviour
{
    private int interval;
    public int minInterval; // How often do you want cats to spawn
    public int maxInterval;
    public float startTime;

    public CatSpawner catSpawnScript;
    // Start is called before the first frame update
    void Start()
    {
        catSpawnScript = GameObject.Find("Cat Spawn").GetComponent<CatSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the time interval and tell the spawner to spawn a cat on the next frame
        // Goes to next position in the list to tell that spawner to spawn a cat
        if(Time.time - startTime > interval)
        {
            catSpawnScript.setSpawnTrue();
            // randomize interval and reset time
            startTime = Time.time;
            // edit range values to change how fast the cats spawn
            interval = Random.Range(minInterval, maxInterval);
        }   
    }
}

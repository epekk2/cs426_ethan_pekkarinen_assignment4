using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Runtime.Versioning;
using System.Threading;
using System;
public class CatSpawner : NetworkBehaviour
{
    public GameObject cat;
    bool spawnNow;
    int[] catPlacement = {-7,0,7};
    int pos;

    // Should be called in gameWatcher to set this to true
    // Will allow the spawner to spawn something
    public void setSpawnTrue (){
        spawnNow = true;
    }
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        // if time passed is greater than interval, spawn cat
        if (spawnNow == true) 
        {
            catSpawningServerRpc(GetComponent<NetworkObject>().transform.position, GetComponent<NetworkObject>().transform.rotation);
            spawnNow = false;
        }
    }

    // need to add the [ServerRPC] attribute
    [ServerRpc]
    // method name must end with ServerRPC
    private void catSpawningServerRpc(Vector3 position, Quaternion rotation)
    {
        // call catSpawningClientRpc method to locally create the cat on all clients
        pos = UnityEngine.Random.Range(0,3);
        catSpawningClientRpc(position, rotation, pos);
    }

    [ClientRpc]
    private void catSpawningClientRpc(Vector3 position, Quaternion rotation, int pos)
    {
        GameObject newCat = Instantiate(cat, position, rotation);
        newCat.transform.Rotate(-90.0f,90.0f,0.0f,Space.Self);
        newCat.transform.position = new Vector3(catPlacement[pos], transform.position.y, transform.position.z);

    }
    
}

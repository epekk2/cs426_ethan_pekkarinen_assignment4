using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{

    // reference to the camera audio listener
    [SerializeField] private AudioListener audioListener;
    // reference to the camera
    [SerializeField] private Camera playerCamera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check if the player is the owner of the object
        // makes sure the script is only executed on the owners 
        // not on the other prefabs 
        if (!IsOwner) return;
    }

    public override void OnNetworkSpawn()
    {
        // check if the player is the owner of the object
        if (!IsOwner) return;
        // if the player is the owner of the object
        // enable the camera and the audio listener
        audioListener.enabled = true;
        playerCamera.enabled = true;

        transform.position = new Vector3(0f, 15.5f, -14f);
    }
}

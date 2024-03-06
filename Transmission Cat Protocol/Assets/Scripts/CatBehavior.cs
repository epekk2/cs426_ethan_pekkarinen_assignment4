using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Diagnostics;

public class CatBehavior : NetworkBehaviour
{
    public float speed = 2f;

    private void OnCollisionEnter(Collision collision)
    {

        // printing if collision is detected on the console
        //Debug.Log("Collision Detected");
        Destroy(gameObject);
        GetComponent<NetworkObject>().Despawn(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        moveDirection.z = +1f;
        transform.position += moveDirection * speed * Time.deltaTime;

        // move to rightmost track when left clicked, else do nothing if already on rightmost


    }
    

    // need to add the [ServerRPC] attribute
    [ServerRpc]
    // method name must end with ServerRPC
    private void catSwitchingServerRpc(int valToChange)
    {
        // call catSpawningClientRpc method to locally create the cat on all clients
        catSwitchingClientRpc(valToChange);
    }

    [ClientRpc]
    private void catSwitchingClientRpc(int valToChange)
    {
        //transform.position = new Vector3(valToChange, transform.position.y, transform.position.z);
        GetComponent<NetworkObject>().transform.position = new Vector3(valToChange, transform.position.y, transform.position.z);
    }

    // handles functionality for left clicking a cat
    void OnMouseDown()
    {
        // if cat is not already in left most lane, switch its lane
        if (transform.position.x != -7)
        {
            int valToChange = 0;
            if (transform.position.x == 0)
            {
                valToChange = -7;
            }
            else
            {
                valToChange = 0;
            }

            transform.position = new Vector3(valToChange, transform.position.y, transform.position.z);
            //catSwitchingServerRpc(valToChange);
        }
    }

    // handles functionality for right clicking a cat
    public void OnMouseOver()
    {
        // makes sure it was a right click
        if (Input.GetMouseButtonDown(1))
        {
            if (transform.position.x != 7)
            {
                int valToChange = 0;
                if (transform.position.x == 0)
                {
                    valToChange = 7;
                }
                else
                {
                    valToChange = 0;
                }

                transform.position = new Vector3(valToChange, transform.position.y, transform.position.z);
                //catSwitchingServerRpc(valToChange);
            }
        }
    }

}

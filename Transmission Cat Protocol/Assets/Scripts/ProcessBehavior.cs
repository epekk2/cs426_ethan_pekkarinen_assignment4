using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public class ProcessBehavior : NetworkBehaviour
{

    public bool onCooldown;
    public float cooldown;  // set cooldown time
    public float cooldownTimer;  // current cooldown time
    // Start is called before the first frame update

    private Renderer render;
    public Material defaultMaterial;
    public Material cooldownMaterial;

    public ScoreManager scoreManager;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (onCooldown) {
            if (cooldownTimer > 0) {
                cooldownTimer -= Time.deltaTime;
            }

            else if (cooldownTimer <= 0) {
                render.material = defaultMaterial;  // remove cooldown material
                onCooldown = false;
            }
            
        }

    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Projectile") {
            if (!onCooldown) {
                scoreManager.score += 1;
                cooldownTimer = cooldown;  // reset cooldown
                onCooldown = true;
                render.material = cooldownMaterial;  // show cooldown
            }
            else {
                scoreManager.waste += 1;
            }


            

        }
    }



}

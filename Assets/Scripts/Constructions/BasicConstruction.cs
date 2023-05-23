using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicConstruction : Construction
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;
        //Debug.Log("Colliding with " + collision.collider.name);
        renderer.material.color = Color.red;
        CanBePlacedOnTerrain = false;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            return;
        CanBePlacedOnTerrain = true;
        //Debug.Log("Exiting collision with " + collision.collider.name);
        renderer.material.color = Color.green;
    }
}

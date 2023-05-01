using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Construction : MonoBehaviour
{
    protected new Renderer renderer;
    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    //public virtual void OnCollisionEnter (Collision collision) {
    //    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //        return;
    //    Debug.Log("Colliding with "+collision.collider.name);
    //    renderer.material.color = Color.red;
    //}

    //public virtual void OnCollisionExit (Collision collision)
    //{
    //    if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //        return;
    //    Debug.Log("Exiting collision with "+collision.collider.name);
    //    renderer.material.color = Color.green;
    //}
}

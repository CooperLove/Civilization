using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public Vector3 worldPosition;
    Plane plane = new(Vector3.up, 0);
    [SerializeField] GameObject g;
    [SerializeField] float rayDuration = 1f;
    [SerializeField] float rayLength = 1f;
    [SerializeField] bool drawRay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        //if (plane.Raycast(ray, out float distance)){
        //    worldPosition = ray.GetPoint(distance);
        //    g.transform.position = worldPosition;
        //}

        if (Physics.Raycast(ray, out RaycastHit hit, 2000f)) { 
            worldPosition = hit.point;
            g.transform.position = worldPosition;
            rayLength = hit.distance;
        }
        if (drawRay)
            Debug.DrawRay(Camera.main.transform.position, ray.direction * rayLength, Color.blue, rayDuration, true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance { get; private set; }

    public Vector3 WorldPosition { get; private set; }
    Plane plane = new(Vector3.up, 0);
    LayerMask layerMask = 1 << 9;
    public GameObject g;
    [SerializeField] float rayDuration = 1f;
    [SerializeField] float rayLength = 1f;
    [SerializeField] bool drawRay = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (g == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (plane.Raycast(ray, out float distance))
        {
            WorldPosition = ray.GetPoint(distance);
        }

        if (Physics.Raycast(ray, out RaycastHit hit, 2000f, layerMask)) {

            WorldPosition = hit.point;
            g.transform.position = WorldPosition;
            rayLength = hit.distance;
        }
        if (drawRay)
            Debug.DrawRay(Camera.main.transform.position, ray.direction * rayLength, Color.blue, rayDuration, true);
    }
}

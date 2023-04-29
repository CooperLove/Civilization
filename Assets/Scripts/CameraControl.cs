using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CameraControl : MonoBehaviour
{
    private PlayerInput playerInput;
    PlayerInputActions playerInputActions;
    private Camera cameraScript;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float zoomSpeed = 1f;

    private void Awake()
    {
        
        cameraScript = GetComponent<Camera>();

        playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = true;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        //playerInputActions.Player.Move.performed += MoveCamera;
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (playerInput.enabled == false)
        //    playerInput.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomCamera(playerInputActions.Player.Zoom.ReadValue<Vector2>());
        MoveCamera(playerInputActions.Player.Move.ReadValue<Vector2>());
    }


    private void MoveCamera (InputAction.CallbackContext context) {
        Debug.Log(context);
    }

    private void MoveCamera(Vector2 v)
    {
        transform.position += speed * Time.deltaTime * new Vector3(v.x, 0, v.y);
    }

    private void ZoomCamera(Vector2 v) {
        cameraScript.fieldOfView = Math.Clamp(cameraScript.fieldOfView + (v.y * -1) * zoomSpeed * Time.deltaTime, 40, 70);
        
    }
}

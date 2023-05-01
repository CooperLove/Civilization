using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildBarManager : MonoBehaviour
{
    public static BuildBarManager Instance { get; private set; }

    [SerializeField] private PlayerInputActions _actions;
    Dictionary<InputControl, int> inputControls = new();
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

    [SerializeField] private GameObject currentBuildable = null;


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        inputControls = new() {
            { Keyboard.current.digit1Key, 0 },
            { Keyboard.current.digit2Key, 1 },
            { Keyboard.current.digit3Key, 2 },
            { Keyboard.current.digit4Key, 3 },
            { Keyboard.current.digit5Key, 4 },
            { Keyboard.current.digit6Key, 5 },
            { Keyboard.current.digit7Key, 6 },
            { Keyboard.current.digit8Key, 7 },
            { Keyboard.current.digit9Key, 8 },
            { Keyboard.current.digit0Key, 9 },
        };
        foreach (KeyValuePair<InputControl, int> kvp in inputControls)
        {
            Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value);
        }
        _actions = new PlayerInputActions();
        _actions.Player.Enable();
        //_actions.Player.InstantiateConstruction.performed += 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnListenToInput(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (context.control.Equals(Mouse.current.leftButton))
        {
            Debug.Log("Trying to place a buildable");
            OnTryPlaceBuildable();
        }

        if (context.control.Equals(Keyboard.current.escapeKey))
        {
            OnDestroyBuildable();
        }

        if (inputControls.TryGetValue(context.control, out int index))
        {
            OnDestroyBuildable();
            OnCreateBuildable(index);
        }

    }

    private void OnCreateBuildable(int index)
    {
        Debug.Log("Creating construction number " + index);
        Create(index);
    }

    private void Create (int index) {
        if (gameObjects[index] == null)
            return;

        currentBuildable = Instantiate(gameObjects[index], MouseManager.Instance.WorldPosition, new Quaternion(0,0,0,0));
        currentBuildable.AddComponent<NetworkRigidbody>();
        Rigidbody rb = currentBuildable.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.freezeRotation = true;
        MouseManager.Instance.g = currentBuildable;
    }

    private void OnDestroyBuildable() => Destroy(MouseManager.Instance.g);
    private void OnPlaceBuildable() => MouseManager.Instance.g = null;

    private void OnTryPlaceBuildable() {
        if (currentBuildable == null)
            return;

        if (!currentBuildable.GetComponent<Construction>().CanBePlacedOnTerrain)
            return;

        Destroy(currentBuildable.GetComponent<NetworkRigidbody>());
        Destroy(currentBuildable.GetComponent<Rigidbody>()); 
        OnPlaceBuildable();
    } 
}

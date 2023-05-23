using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildBarManager : MonoBehaviour
{
    public static BuildBarManager Instance { get; private set; }

    private PlayerInput _playerInput;
    [SerializeField] private PlayerInputActions _actions;
    Dictionary<InputControl, int> inputControls = new();
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

    [SerializeField] private GameObject currentBuildable = null;
    [SerializeField] private GameObject buildBar= null;
    private Dictionary<InputControl, Action> methods = new();

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        _playerInput = GetComponent<PlayerInput>();

        methods = new() {
            {Keyboard.current.escapeKey, () => OnDestroyBuildable() },
            {Keyboard.current.bKey, () => OpenBuildBar() },
            {Mouse.current.leftButton, () => OnTryPlaceBuildable() },
        };

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
        
        Debug.Log("Performing "+context.control);

        if (methods.ContainsKey(context.control)) { 
            methods.TryGetValue(context.control, out Action action);
            action?.Invoke();
            return;
        }

        if (inputControls.TryGetValue(context.control, out int index))
        {
            OnDestroyBuildable();
            OnCreateBuildable(index);
        }

    }

    public void OpenBuildBar() => buildBar?.SetActive(!buildBar.activeInHierarchy);

    public void OnCreateBuildable(int index)
    {
        _playerInput.SwitchCurrentActionMap("Construction");
        Debug.Log("Creating construction number " + index+" \n"+_actions.Player.Get().enabled);
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

    private void OnDestroyBuildable() {
        _playerInput.SwitchCurrentActionMap("Player");
        Destroy(MouseManager.Instance.g);
    }
    private void OnPlaceBuildable() => MouseManager.Instance.g = null;

    private void OnTryPlaceBuildable() {
        if (currentBuildable == null)
            return;

        if (!currentBuildable.GetComponent<Construction>().CanBePlacedOnTerrain)
            return;

        Destroy(currentBuildable.GetComponent<NetworkRigidbody>());
        Destroy(currentBuildable.GetComponent<Rigidbody>()); 
        OnPlaceBuildable();
        _playerInput.SwitchCurrentActionMap("Player");
    } 
}

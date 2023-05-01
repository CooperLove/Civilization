using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConstructionManager : MonoBehaviour
{
    [SerializeField] private PlayerInputActions _actions;
    Dictionary<InputControl, int> inputControls = new();
    // Start is called before the first frame update
    void Awake()
    {
        inputControls = new() {
            { Keyboard.current.digit0Key, 0 },
            { Keyboard.current.digit1Key, 1 },
            { Keyboard.current.digit2Key, 2 },
            { Keyboard.current.digit3Key, 3 },
            { Keyboard.current.digit4Key, 4 },
            { Keyboard.current.digit5Key, 5 },
            { Keyboard.current.digit6Key, 6 },
            { Keyboard.current.digit7Key, 7 },
            { Keyboard.current.digit8Key, 8 },
            { Keyboard.current.digit9Key, 9 },
        };
        foreach (KeyValuePair<InputControl, int> kvp in inputControls)
        {
            Debug.Log("Key = "+kvp.Key + ", Value = "+kvp.Value);
        }
        _actions = new PlayerInputActions();
        _actions.Player.Enable();
        //_actions.Player.InstantiateConstruction.performed += 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateConstruction(InputAction.CallbackContext context) {
        if (!context.performed)
            return;

        if (context.control.Equals(Keyboard.current.digit1Key)) { 
            Debug.Log("Pressed " + context.control);
        }
        if (inputControls.TryGetValue(context.control, out int index)) { 
            OnCreateConstruction(index);
        }

    }

    private void OnCreateConstruction(int index) {
        Debug.Log("Creating construction number "+index);
    }
}

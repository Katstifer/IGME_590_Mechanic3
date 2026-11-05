using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class shaderManager : MonoBehaviour
{

    //Editable dictionary of shader effects to their tag, which should be applied to all objects that use the
    //effect
    [SerializeField] private effectShader[] shaders;

    effectShader currentShader;
    int currentIndex;

    //Reference to the shader input
    private InputAction toggleAction;
    private InputAction rotateAction;


    private void Awake()
    {
        InputActionMap shaderInput = InputSystem.actions.FindActionMap("EnhancedVision");

        toggleAction = shaderInput.FindAction("ToggleShader");
        rotateAction = shaderInput.FindAction("RotateShader");

        if (toggleAction == null || rotateAction == null)
        {
            print("Error: cannot locate shader inputs.");
        }

        toggleAction.performed += OnToggleShaders;
        rotateAction.performed += OnRotateShaders;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnToggleShaders(InputAction.CallbackContext obj)
    {
        if (currentShader.IsActive())
        {
            currentShader.disableShader(); 
        }
        else
        {
            currentShader.enableShader(); 
        }
    }

    void OnRotateShaders(InputAction.CallbackContext obj)
    {
        currentIndex++; 
        if (currentIndex >= shaders.Length)
        {
            currentIndex = 0;
        }

        //Disables the old shaders
        currentShader.disableShader(); 

        //Finds next in rotation and changes the current shader to be that.
        currentShader = shaders[currentIndex];
        print("New shader tag: " + currentShader.GetTagName());
        currentShader.enableShader(); 

        print("Shaders rotated!");
    }
}

using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class shaderManager : MonoBehaviour
{

    //List of shaders that this shader manager will rotate/apply to objects
    [SerializeField] private effectShader[] shaders;

    //Current effect shader to apply to game objects
    effectShader currentShader;
    //Index for tracking shader rotation
    int currentIndex;

    //Reference to the shader input
    private InputAction toggleAction;
    private InputAction rotateAction;

    bool shadersActive = false; 


    //Finds input map and attaches listeners
    private void Awake()
    {
        InputActionMap shaderInput = InputSystem.actions.FindActionMap("EnhancedVision");

        if (!shaderInput.enabled)
        {
            shaderInput.Enable();
        }

        toggleAction = shaderInput.FindAction("ToggleShaders");
        rotateAction = shaderInput.FindAction("RotateShaders");


        if (toggleAction == null || rotateAction == null)
        {
            print("Error: cannot locate shader inputs.");
        }

        toggleAction.performed += OnToggleShaders;
        rotateAction.performed += OnRotateShaders;

        //Returns if no shaders are attached in inspector
        if (shaders.Length <= 0)
        {
            print("Shaders array is empty.");
            return;
        }
        currentShader = shaders[0];
        shadersActive = false; 
    }

    //Toggles shaders on and off, depending on their current state
    void OnToggleShaders(InputAction.CallbackContext obj)
    {

        print("Toggling shaders!");

        if (currentShader == null)
        {
            print("Current shader is null.");
            currentShader = shaders[0];
        }

        print("Current shader: " + currentShader);

        currentShader.toggleShaders(); 
        shadersActive = currentShader.IsActive(); 

        print("Shader active: " + shadersActive);
    }

    //Rotates to the next shader, disabling the previous one if active and enabling the new one.
    void OnRotateShaders(InputAction.CallbackContext obj)
    {

        print("Rotating shaders!");

        currentIndex++;
        if (currentIndex >= shaders.Length)
        {
            currentIndex = 0;
        }

        if (currentShader.IsActive())
        {
            currentShader.disableShader();    
        }

        //Finds next in rotation and changes the current shader to be that.
        currentShader = shaders[currentIndex];

        print("Shaders active? " + shadersActive);
        if (shadersActive)
        {
            currentShader.enableShader(); 
        }
        else
        {
            currentShader.disableShader(); 
        }

        print("Shaders rotated!");
    }
}

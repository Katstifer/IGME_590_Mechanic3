using System;
using UnityEngine;

public class effectShader : MonoBehaviour
{

    //Tag that shader searches for to apply materials to
    [SerializeField] private string tagName;

    //Material to apply to found objects
    [SerializeField] private Material shaderMaterial;

    //Default material to return objects to once the shader is disabled.
    [SerializeField] private Material defaultMaterial; 

    //Whether the shader is currently active
    private bool isActive = false;

    //List of objects with specified tag
    GameObject[] objects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive = false;
    }

    //Gets the tag name that this shader should be applied to objects with
    public string GetTagName()
    {
        return tagName;
    }

    //Is the shader active/currently applied
    public bool IsActive()
    {
        return isActive;
    }

    //Finds game objects that have the right tag
    public void findGameObjects(string tagName)
    {
        objects = GameObject.FindGameObjectsWithTag(tagName);

        if (objects == null || objects.Length <= 0)
        {
            print("No objects found with tagName: " + tagName);
        }

        print("Found objects: " + objects);
    }

    //Disables shader on object with matching tagName
    public void disableShader()
    {
        if (isActive == false)
        {
            print("Shader is already disabled, skipping method.");
            return;
        }

        findGameObjects(tagName);

        if (objects.Length <= 0)
        {
            print("Can't find objects with tag.");
            return;
        }

        foreach (GameObject obj in objects)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>(); 

            sr.material = shaderMaterial; 
        }

        isActive = false;
    }

    //Enables shader on objects with the matching tagName
    public void enableShader()
    {
        if (isActive == true)
        {
            print("Shader is already enabled, skipping method.");
            return;
        }
        
        findGameObjects(tagName);

        if (objects.Length <= 0)
        {
            print("Can't find objects with tag.");
            return;
        }

        foreach (GameObject obj in objects)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>(); 

            sr.material = defaultMaterial; 
        }

        isActive = true;
    }
}

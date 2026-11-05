using System;
using UnityEngine;

public class effectShader : MonoBehaviour
{

    [SerializeField] private string tagName;
    [SerializeField] private string effectLayerID;
    [SerializeField] private string defaultLayerID = "Default";

    private bool isActive = false;

    GameObject[] objects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetTagName()
    {
        return tagName;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void findGameObjects(string tagName)
    {
        objects = GameObject.FindGameObjectsWithTag(tagName);

        if (objects == null || objects.Length <= 0)
        {
            print("No objects found with tagName: " + tagName);
        }
    }

    //Disables shader on object with matching tagName
    public void disableShader()
    {
        if (isActive == false)
        {
            print("Shader is already disabled, skipping method.");
            return;
        }


        int targetLayer = LayerMask.NameToLayer(defaultLayerID);
        findGameObjects(tagName);

        if (objects.Length <= 0)
        {
            print("Can't find objects with tag.");
            return;
        }

        if (targetLayer != -1)
        {
            foreach (GameObject obj in objects)
            {
                obj.layer = targetLayer;
            }
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

        print("Enabling shader for " + tagName);

        int targetLayer = LayerMask.NameToLayer(effectLayerID);
        findGameObjects(tagName);

        if (objects.Length <= 0)
        {
            print("Can't find objects with tag.");
            return;
        }

        if (targetLayer != -1)
        {
            foreach (GameObject obj in objects)
            {
                obj.layer = targetLayer;
            }
        }

        isActive = true;
    }
}

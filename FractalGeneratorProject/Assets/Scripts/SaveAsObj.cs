using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class SaveAsObj : MonoBehaviour
{
    public MeshFilter targetObject;
    public bool save = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(save)
        {
            ObjExporter.MeshToFile(targetObject, targetObject.name + ".obj");
            save = false;
        }
    }
}

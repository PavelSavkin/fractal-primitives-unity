using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTJ.FbxExporter;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshMerger : MonoBehaviour
{
    //public bool boolean_combine = false;
    public bool combine = false;
    public bool saveObj = false;
    public bool saveObjNoCombine = false;
    public GameObject targetObject;

    public static GameObject Clone(GameObject go)
    {
        var clone = GameObject.Instantiate(go) as GameObject;
        clone.transform.parent = go.transform.parent;
        clone.transform.localPosition = go.transform.localPosition;
        clone.transform.localScale = go.transform.localScale;
        return clone;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetObject = GameObject.FindGameObjectWithTag("Fractal");
        if (combine)
        {
            MeshFilter[] meshFilters = targetObject.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combines = new CombineInstance[meshFilters.Length];
            int i = 0;
            while (i < meshFilters.Length)
            {
                combines[i].mesh = meshFilters[i].sharedMesh;
                combines[i].transform = meshFilters[i].transform.localToWorldMatrix;
                i++;
                //meshFilters[i].gameObject.SetActive(false);
            }
            transform.GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combines);
            transform.GetComponent<MeshRenderer>().material = (Material)Resources.Load("combined");
            combine = false;
        }
        if(saveObj)
        {
            if(transform.GetComponent<MeshFilter>().mesh != null)
            {
                ObjExporter.MeshToFile(transform.GetComponent<MeshFilter>(), targetObject.name + ".obj");
            }
            saveObj = false;
        }
        if(saveObjNoCombine)
        {
            ObjExporter.MeshToFile(targetObject.GetComponent<MeshFilter>(), "testsetset.obj");
            saveObjNoCombine = false;
        }
        
    }
}

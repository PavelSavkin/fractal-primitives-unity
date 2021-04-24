using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UTJ.FbxExporter;

public class Fractal2Fbx : MonoBehaviour
{
    private FbxExporter _exporter;
    private FbxExporter.ExportOptions _save_mode;
    private FbxExporter.Format _save_foramt;

    private bool DoExport(string path, GameObject[] objects,FbxExporter.Format format)
    {
        _save_mode = FbxExporter.ExportOptions.defaultValue;
        _exporter = new FbxExporter(_save_mode);
        _exporter.CreateScene(System.IO.Path.GetFileName(path));

        foreach (var obj in objects)
            _exporter.AddNode(obj);

        if (_exporter.WriteAsync(path, format))
        {
            Debug.Log("Export started: " + path);
            return true;
        }
        else
        {
            Debug.Log("Export failed: " + path);
            return false;
        }
    }

    public bool IsFinished() { return _exporter.IsFinished(); }

    public void ExportFbx(GameObject go, string path)
    {
        var objects = new HashSet<GameObject>();
        objects.Add(go);
        foreach (var c in go.GetComponentsInChildren<MeshRenderer>())
            objects.Add(c.gameObject);

        if (objects.Count == 0)
        {
            Debug.LogWarning("FbxExporter: Nothing to export");
        }
        else
        {
            if (path != null && path.Length > 0)
            {
                _save_foramt = FbxExporter.Format.FbxBinary;
                DoExport(path, objects.ToArray(), _save_foramt);
            }
        }
    }
}

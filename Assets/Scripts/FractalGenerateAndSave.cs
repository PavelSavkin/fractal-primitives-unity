using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Fractal2Fbx))]
public class FractalGenerateAndSave : MonoBehaviour
{
    [Header("File property")]
    public string save_path = "";
    public int num_save = 0;
    [Space(10)]
    [Header("Required objects")]
    private CreateFractal _fractal_creator;
    private Fractal2Fbx _fractal_recorder;
    //Increase this number for saving heavier fractals
    [Header("wait to save")]
    public float wait_time = 0.05f;
    [Space(10)]
    [Header("Start recording")]
    public bool start = false;

    private int _count = 0;

    IEnumerator GenerateLoop()
    {
        yield return null;
        for (int i = 0; i < num_save; i++)
        {
            string filename = save_path + "//" + _count.ToString();
            yield return GenerateAndSave(filename);
            _count++;
        }
    }

    IEnumerator GenerateAndSave(string filename)
    {
        var fractal = _fractal_creator.Create();
        yield return new WaitForSeconds(wait_time);
        _fractal_recorder.ExportFbx(fractal, filename);
        while (!_fractal_recorder.IsFinished()) continue;
        yield return new WaitForSeconds(wait_time);

    }

    void Start()
    {
        _fractal_creator = this.GetComponent<CreateFractal>();
        _fractal_creator.SetDespawn(true);
        _fractal_recorder = this.GetComponent<Fractal2Fbx>();
    }

    void Update()
    {
        if(start)
        {
            Debug.Log("Start recording fractal objects.");
            StartCoroutine(GenerateLoop());
            Debug.Log("Finished recording.");
            start = false;
        }
    }
    
}

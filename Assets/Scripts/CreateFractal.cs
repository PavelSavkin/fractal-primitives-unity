using UnityEngine;

public class CreateFractal : MonoBehaviour
{
    private int _count = 0;
    private GameObject _obj = null;
    private bool _delete_when_spawn = false;

    public void SetDespawn(bool delete_when_spawn)
    {
        _delete_when_spawn = delete_when_spawn;
    }

    public GameObject Create()
    {
        if (_delete_when_spawn && _obj != null)
        {
            var result = Delete(_obj);
        }
        var go = (GameObject)Resources.Load("FractalObject");
        go.name = "Fractal" + (_count+1).ToString();
        _obj = Instantiate(go);
        _count++;
        return _obj;
    }

    public bool Delete(GameObject fractal)
    {
        foreach (Transform child in fractal.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(fractal);
        return true;
    }
}

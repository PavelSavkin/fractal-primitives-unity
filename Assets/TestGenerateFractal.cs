using UnityEngine;
[RequireComponent(typeof(CreateFractal))]
public class TestGenerateFractal : MonoBehaviour
{
    public bool create = false;

    private CreateFractal _fractal_creator;

    // Start is called before the first frame update
    void Start()
    {
        _fractal_creator = this.GetComponent<CreateFractal>();
        _fractal_creator.SetDespawn(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (create)
        {
            var result = _fractal_creator.Create();
            create = false;
        }
    }
}

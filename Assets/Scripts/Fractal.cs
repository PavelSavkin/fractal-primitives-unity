//expanded from https://catlikecoding.com/unity/tutorials/constructing-a-fractal/
using UnityEngine;

public class Fractal : MonoBehaviour
{
    public Mesh[] meshes;
    public Material material;
    [Range(1,5)]
    public int maxDepth;
    [Range(0f,1f)]
    public float childScale;
    [Range(0f,1f)]
    public float spawnProbability;
    [Range(0f, 1f)]
    public float scaleProbability;
    [Range(0f, 1f)]
    public float scaleDownProbability;
    [Space(10)]
    public float maxTwist;

    private int _depth;
    private const float _DISTANCE_FACTOR = 0.8f;
    private const float _MIN_SCALE_RANGE = 0.75f;
    private const float _MAX_SCALE_RANGE = 1.25f;

    private Vector3[] _childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back,
        Vector3.down
    };

    private Quaternion[] _childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f),
        Quaternion.identity
    };

    private void Initialize(Fractal parent, int childIndex)
    {
        //inherits from parent
        meshes = parent.meshes;
        material = parent.material;
        maxDepth = parent.maxDepth;
        _depth = parent._depth + 1;
        spawnProbability = parent.spawnProbability;
        scaleProbability = parent.scaleProbability;
        scaleDownProbability = parent.scaleDownProbability;
        maxTwist = parent.maxTwist;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;

        //don't allow scaling randomly
        if (Random.value < scaleDownProbability)
        {
            transform.localScale = Vector3.one;
            
        }

        //Note : this commented out section will allow to spawn objects more agressively.
        //transform.localPosition = parent.transform.localPosition + _childDirections[childIndex] * (Random.Range(0.3f,0.5f) + Random.Range(0.3f, 0.5f) * parent.childScale);
        transform.localPosition = parent.transform.localPosition + _childDirections[childIndex] * parent.childScale * _DISTANCE_FACTOR;
        transform.localRotation = _childOrientations[childIndex];
    }

    private void CreateChildren()
    {
        for (int i = 0; i < _childDirections.Length; i++)
        {
            if(Random.value< spawnProbability)
            {
                var newobj = new GameObject("Fractal Child").AddComponent<Fractal>();
                newobj.tag = "Fractal";
                newobj.Initialize(this, i);
            }
        }
    }

    void Start()
    {
        transform.Rotate(Random.Range(-maxTwist, maxTwist), Random.Range(-maxTwist, maxTwist), Random.Range(-maxTwist, maxTwist));
        if(Random.value < scaleProbability)
        {
            transform.localScale = new Vector3(Random.Range(_MIN_SCALE_RANGE, _MAX_SCALE_RANGE), Random.Range(_MIN_SCALE_RANGE, _MAX_SCALE_RANGE), Random.Range(_MIN_SCALE_RANGE, _MAX_SCALE_RANGE));
        }

        gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        gameObject.AddComponent<MeshRenderer>().material = material;
        if (_depth < maxDepth)
        {
            CreateChildren();
        }
    }
}

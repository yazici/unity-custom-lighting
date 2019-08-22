using UnityEngine;

public class VertexShow : MonoBehaviour
{
    [SerializeField] protected Material mat;
    [SerializeField, Range(0f, 0.1f)] protected float _pointSize = 0.03f;

    protected Mesh originalMesh;
    protected Mesh pointMesh;

    void Setup()
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        pointMesh = Instantiate(originalMesh);
        pointMesh.SetIndices(originalMesh.triangles, MeshTopology.Points, 0);
        var transformMatrix = Matrix4x4.TRS(Vector3.zero, transform.rotation, transform.lossyScale);
        mat.SetMatrix("_TransformMatrix", transformMatrix);
    }

    void Reset()
    {
        originalMesh = null;
        Destroy(pointMesh); pointMesh = null;
    }

    void OnEnable()
    {
        Setup();
    }

    void OnDisable()
    {
        Reset();
    }

    protected virtual void Update()
    {
        if (transform.hasChanged)
        {
            Reset();
            Setup();
        }

        mat.SetFloat("_PointSize", _pointSize);
        Graphics.DrawMesh(pointMesh, transform.position, Quaternion.identity, mat, 0);
    }
}


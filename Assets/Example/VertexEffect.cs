using UnityEngine;

public class VertexEffect : VertexShow
{
    RenderTexture rt;
    [SerializeField] Transform sphereT;
    [SerializeField] float radius;

    void Start()
    {
        var vts = originalMesh.vertices;
        rt = new RenderTexture(vts.Length, 1, 0, RenderTextureFormat.ARGB32);
    }

    void OnDestroy()
    {
        rt.Release();
        rt = null;
    }

    protected override void Update()
    {
        base.Update();
        mat.SetVector("_MousePosition", Camera.main.ScreenToViewportPoint(Input.mousePosition));
        mat.SetTexture("_EventDataTex", rt);
        mat.SetVector("_SpherePos", sphereT.position);
        mat.SetFloat("_SphereRad", radius);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sphereT.position, radius);
    }
}

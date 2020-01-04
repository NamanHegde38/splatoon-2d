using UnityEngine;

public class MeshHandler : MonoBehaviour {
    
    private void Start() {
        HandleMesh();
    }

    private void HandleMesh() {
        var mesh = new Mesh();
        
        var vertices = new Vector3[4];
        var uv = new Vector2[4];
        var triangles = new int[6];
        vertices = SetVertices(vertices);
        uv = SetUv(uv);
        triangles = SetTriangles(triangles);
        SetMesh(mesh, vertices, uv, triangles);
    }

    private static Vector3[] SetVertices(Vector3[] vertices) {
        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 1);
        vertices[2] = new Vector3(1, 1);
        vertices[3] = new Vector3(1, 0);
        return vertices;
    }

    private static Vector2[] SetUv(Vector2[] uv) {
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);
        return uv;
    }

    private static int[] SetTriangles(int[] triangles) {
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;
        return triangles;
    }

    private void SetMesh(Mesh mesh, Vector3[] vertices, Vector2[] uv, int[] triangles) {
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;
    }
}

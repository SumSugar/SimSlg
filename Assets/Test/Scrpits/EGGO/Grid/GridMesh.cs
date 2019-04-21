using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GridMesh : MonoBehaviour {

    Mesh gridMesh;
    List<Vector3> vertices;
    List<int> triangles;
    // Use this for initialization

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = gridMesh = new Mesh();
        gridMesh.name = "Grid Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }
    void Start() {

    }
    /// <summary>
    /// 绘制所有三角
    /// </summary>
    /// <param name="cells"></param>
    public void Triangulate(Cell[] cells)
    {
        gridMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        for (int i = 0; i < cells.Length; i++) 
        {
            Triangulate(cells[i]);
        }
        gridMesh.vertices = vertices.ToArray();
        gridMesh.triangles = triangles.ToArray();

    }
    /// <summary>
    /// 重载绘制单一三角
    /// </summary>
    /// <param name="cell"></param>
    void Triangulate (Cell cell)
    {
        Vector3 center = cell.transform.localPosition;
        
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex+1);
        triangles.Add(vertexIndex+2);
    }
}

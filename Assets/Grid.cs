using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
//using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int xSize, ySize;
    private Vector3[] vertices;
    private Mesh mesh;

    
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        


        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                yield return wait;
            }
        }

        mesh.vertices = vertices;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = xSize + 1;
        triangles[2] = 1;
        triangles[3] = 1;
        triangles[4] = xSize + 1;
        triangles[5] = xSize + 2;
        mesh.triangles = triangles;
    }

    void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);

        }
    }
}

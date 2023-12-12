using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{

    ShapeGenerator shapeGenerator;
    Mesh mesh;
    int res;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int res, Vector3 localUp)
    {
        // set the shape generator, mesh, resolution, and local up from constructor
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.res = res;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        // create arrays for vertices, triangles, and uvs

        Vector3[] vertices = new Vector3[res * res];

        // Tutorial 1: Triangles and UVs

        // triangles are 3 points that make up a triangle it would take 2 triangles to make a square in a mesh 
        int[] triangles = new int[(res - 1) * (res - 1) * 6];


        int tri = 0;
        Vector2[] uv = mesh.uv;

        // for each vertex
        for (int y = 0; y < res; y++)
        {
            // for each vertex in the row
            for (int x = 0; x < res; x++)
            {

                // get the index of the vertex
                int i = x + y * res;

                // get the percent of the vertex
                Vector2 percent = new Vector2(x, y) / (res - 1);

                // get the point on the cube
                Vector3 pointOnCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;

                // get the point on the sphere
                Vector3 pointOnSphere = pointOnCube.normalized;

                // get the elevation
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnSphere);

                // if the x and y are not the last vertex
                if (x != res - 1 && y != res - 1)
                {
                    // set the triangles
                    triangles[tri] = i;
                    triangles[tri + 1] = i + res + 1;
                    triangles[tri + 2] = i + res;

                    triangles[tri + 3] = i;
                    triangles[tri + 4] = i + 1;
                    triangles[tri + 5] = i + res + 1;
                    tri += 6;
                }
            }
        }

        // clear the mesh and set the vertices, triangles, and uvs 
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uv;
    }

    public void UpdateUVs(ColourGenerator colourGenerator)
    {
        // create a new uv array
        Vector2[] uv = new Vector2[res * res];

        // for each vertex
        for (int y = 0; y < res; y++)
        {
            // for each vertex in the row
            for (int x = 0; x < res; x++)
            {
                // get the index of the vertex
                int i = x + y * res;

                // get the percent of the vertex
                Vector2 percent = new Vector2(x, y) / (res - 1);

                // get the point on the cube
                Vector3 pointOnCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;

                // get the point on the sphere
                Vector3 pointOnSphere = pointOnCube.normalized;

                // set the uv for the vertex
                uv[i] = new Vector2(colourGenerator.BiomePercentFromPoint(pointOnSphere), 0);
            }
        }
        // set the uv
        mesh.uv = uv;
    }

}
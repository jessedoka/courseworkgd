using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{

    [Range(2, 256)]
    public int resolution = 10;
    
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colourSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColourGenerator colourGenerator = new ColourGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;


    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colourGenerator.UpdateSettings(colourSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        // for each face
        for (int i = 0; i < 6; i++)
        {
            // if there is no mesh filter
            if (meshFilters[i] == null)
            {
                // create a mesh filter
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            // set face render mask
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;

            // if the face render mask is all or the face render mask is the same as the index
            if (faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i)
            {
                // if the mesh is active
                meshFilters[i].gameObject.SetActive(true);
                // create the mesh
                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
            }
            else
            {
                // if the mesh is not active
                meshFilters[i].gameObject.SetActive(false);
            }
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColourSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColours();
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            // if the mesh is active
            if (meshFilters[i].gameObject.activeSelf)
            {
                // create the mesh
                terrainFaces[i].ConstructMesh();
            }
        }

        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColours()
    {
        colourGenerator.UpdateColours();

        // for each face
        for (int i = 0; i < 6; i++)
        {
            // if the mesh is active
            if (meshFilters[i].gameObject.activeSelf)
            {
                // update the UVs
                terrainFaces[i].UpdateUVs(colourGenerator);
            }
        }
    }

    // planet rotating
    public float rotationSpeed = 10f;
    public float rotationSpeedMultiplier = 1f;
    public float rotationSpeedMax = 100f;

    // Update is called once per frame
    void Update()
    {
        // rotate the planet around the y axis by the rotation speed 
        transform.Rotate(Vector3.up * rotationSpeed * rotationSpeedMultiplier * Time.deltaTime);
        rotationSpeedMultiplier = Mathf.Clamp(rotationSpeedMultiplier, 0f, rotationSpeedMax);
    }
}
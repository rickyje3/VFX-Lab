using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    public List<SkinnedMeshRenderer> skinnedMeshes;
    public VisualEffect VFXGraph;
    public float refreshRate;
    private List<Mesh> bakedMeshes;


    // Start is called before the first frame update
    void Start()
    {
        bakedMeshes = new List<Mesh>(skinnedMeshes.Count);
        for (int i = 0; i < skinnedMeshes.Count; i++)
        {
            bakedMeshes.Add(new Mesh()); //Create a mesh for each skinnedmeshrenderer
        }

        StartCoroutine(UpdateVFXGraph());
    }


    IEnumerator UpdateVFXGraph()
    {
        Mesh combinedMesh = new Mesh();
        CombineInstance[] combineInstances = new CombineInstance[skinnedMeshes.Count]; //combine the instances of the skinned mesh into one singular mesh

        while (gameObject.activeSelf)
        {
            for (int i = 0; i < skinnedMeshes.Count; i++)
            {
                var skinnedMesh = skinnedMeshes[i];
                var bakedMesh = bakedMeshes[i];

                skinnedMesh.BakeMesh(bakedMesh); //bake into the mesh
                Vector3[] vertices = combinedMesh.vertices;
                combinedMesh.vertices = vertices;
                combineInstances[i] = new CombineInstance()
                {
                    mesh = bakedMesh, //bake each instance of combined mesh
                    transform = skinnedMesh.transform.localToWorldMatrix
                };
            }
            combinedMesh.CombineMeshes(combineInstances, true, true);
            VFXGraph.SetMesh("Mesh", combinedMesh); //Assigns vfx graph to combined mesh

            yield return new WaitForSeconds(refreshRate);
        }
    }
}

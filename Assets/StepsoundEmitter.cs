using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HumanoidAnimator))]
[RequireComponent(typeof(CharacterController))]
public class StepsoundEmitter : MonoBehaviour
{
    [SerializeField] AudioSource source;
    Dictionary<Material, StepMaterial> m_Sounds = new Dictionary<Material, StepMaterial>();
    [SerializeField] AudioClip genericClip;
    [System.Serializable]
    public class StepMaterial
    {
        public Material material;
        public AudioClip stepSound;
    }
    public List<StepMaterial> tempSteps = new List<StepMaterial>();

    void Start()
    {
        foreach(var step in tempSteps)
        {
            m_Sounds.Add(step.material, step);
        }
    }

    private void Update()
    {
        if(Mathf.Abs(GetComponent<HumanoidAnimator>().GetVelocity().magnitude) > 0 && GetComponent<CharacterController>().isGrounded)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, -transform.up, out hit))
            {
                MeshCollider meshCollider = hit.collider as MeshCollider;
                if (meshCollider == null || meshCollider.sharedMesh == null)
                    return;
                Mesh mesh = meshCollider.sharedMesh;
                Renderer rend = meshCollider.transform.GetComponent<Renderer>();
                if(rend == null)
                    return;
                int i = GetSubMeshIndex(mesh, hit.triangleIndex);
                var mats = new List<Material>();
                rend.GetSharedMaterials(mats);
                print(rend.gameObject.name);
                Material mat = mats[i];
                StepMaterial clip;
                if(m_Sounds.TryGetValue(mat, out clip))
                {
                    source.clip = clip.stepSound;
                }else if(genericClip != null)
                {
                    source.clip = genericClip;
                }else
                {
                    source.Stop();
                }
                if(!source.isPlaying)
                {
                    source.Play();
                }

            }
        }else
        {
            source.Stop();
        }
    }

    public static int GetSubMeshIndex(Mesh mesh, int triangleIndex)
    {
        if (mesh.isReadable == false)
        {
            Debug.LogError("You need to mark model's mesh as Read/Write Enabled in Import Settings.", mesh);
            return 0;
        }

        int triangleCounter = 0;
        for (int subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
        {
            var indexCount = mesh.GetSubMesh(subMeshIndex).indexCount;
            triangleCounter += indexCount / 3;
            if (triangleIndex < triangleCounter)
            {
                return subMeshIndex;
            }
        }

        Debug.LogError(
            $"Failed to find triangle with index {triangleIndex} in mesh '{mesh.name}'. Total triangle count: {triangleCounter}",
            mesh);
        return 0;
    }

}

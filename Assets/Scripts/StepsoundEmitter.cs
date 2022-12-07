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
        public float volume = 0.4f;
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
                int i = mesh.GetSubMeshIndex(hit.triangleIndex);
                var mats = new List<Material>();
                rend.GetSharedMaterials(mats);
                Material mat = mats[i];
                StepMaterial clip;
                if(m_Sounds.TryGetValue(mat, out clip))
                {
                    source.clip = clip.stepSound;
                    source.volume = clip.volume;
                }else if(genericClip != null)
                {
                    source.clip = genericClip;
                    source.volume = 0.4f;
                }
                else
                {
                    source.clip = null;
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

    

}

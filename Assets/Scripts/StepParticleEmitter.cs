using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepParticleEmitter : MonoBehaviour
{
    public ParticleSystem particle;
    // Update is called once per frame
    public void tick()
    {
        if(Mathf.Abs(GetComponent<HumanoidAnimator>().GetVelocity().magnitude) <= 0.5)
        {
            tryStopParticle();
            return;
        }
        if (!GetComponent<CharacterController>().isGrounded)
        {
            tryStopParticle();
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                MeshCollider meshCollider = hit.collider as MeshCollider;
                if (meshCollider == null || meshCollider.sharedMesh == null)
                    return;
                Mesh mesh = meshCollider.sharedMesh;
                Renderer rend = meshCollider.transform.GetComponent<Renderer>();
                if (rend == null)
                    return;

                particle.GetComponent<ParticleSystemRenderer>().material = rend.sharedMaterials[mesh.GetSubMeshIndex(hit.triangleIndex)];
            }
            if(!particle.isPlaying)
                particle.Play();
            return;
        }
        if(particle.isPlaying)
            particle.Stop();
    }

    void tryStopParticle()
    {
        if (particle.isPlaying)
            particle.Stop();
    }
}

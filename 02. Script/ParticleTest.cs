using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }


    [ExecuteInEditMode]
    void OnParticleTrigger()
    {
        // particles
        List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        // get
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside, out var insideData);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle p = inside[i];
            if (insideData.GetCollider(i, 0).name == "Red")
            {
                p.startColor = new Color32(255, 0, 0, 255);
            }
            else if (insideData.GetCollider(i, 0).name == "Green")
            {
                p.startColor = new Color32(0, 255, 0, 255);
            }
            else if(insideData.GetCollider(i, 0).name == "Blue")
            {
                p.startColor = new Color32(0, 0, 255, 255);
            }
            inside[i] = p;
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(255, 255, 255, 255);
            exit[i] = p;
        }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}

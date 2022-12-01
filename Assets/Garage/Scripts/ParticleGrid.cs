using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGrid : MonoBehaviour
{
    ParticleSystem particle_system;

    public Vector3 bounds = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3Int resolution = new Vector3Int(2, 2, 2);

    private void OnEnable()
    {
        particle_system = GetComponent<ParticleSystem>();

        Vector3 scale;
        Vector3 bounds_half = bounds / 2.0f;

        scale.x = bounds.x / resolution.x;
        scale.y = bounds.y / resolution.y;
        scale.z = bounds.z / resolution.z;

        ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();

        for (int i = 0; i < resolution.x; i++)
        {
            for (int j = 0; j < resolution.y; j++)
            {
                for (int k = 0; k < resolution.z; k++)
                {
                    Vector3 position;

                    position.x = (i * scale.x) - bounds_half.x;
                    position.y = (j * scale.y) - bounds_half.y;
                    position.z = (k * scale.z) - bounds_half.z;

                    ep.position = position;
                    ep.startSize = 0.5f;
                    particle_system.Emit(ep, 1);
                }
            }
        }
    }
}

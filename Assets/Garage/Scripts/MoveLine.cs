using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLine : MonoBehaviour
{
    LineRenderer line;

    public bool active;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                for (int i = 1; i < line.positionCount; i++)
                {
                    Vector3 position = line.GetPosition(i);
                    if (position.x != 0)
                    {
                        position.x += 0.1f;
                        if (position.x > 0.1f * (i + 2))
                        {
                            position.x = 0.1f * i;
                        }
                    }
                    if (position.y != 0)
                    {
                        position.y += 0.1f;
                        if (position.y > 0.1f * (i + 2))
                        {
                            position.y = 0.1f * i;
                        }
                    }
                    if (position.z != 0)
                    {
                        position.z += 0.1f;
                        if (position.z > 0.1f * (i + 2))
                        {
                            position.z = 0.1f * i;
                        }
                    }
                    line.SetPosition(i, position);
                }
                timer = 0;
            }
           
        }
    }
}

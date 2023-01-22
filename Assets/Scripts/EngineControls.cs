using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EngineControls : MonoBehaviour
{
    public static EngineControls instance;

    int speed = 0;
    Rigidbody engine_body;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        engine_body = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        engine_body.AddForce(this.gameObject.transform.forward * (movement.y * 0.1f), ForceMode.VelocityChange);
        engine_body.AddTorque(new Vector3(0, movement.x * 10, 0), ForceMode.Force);
    }

    public void OnVehicleMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
}

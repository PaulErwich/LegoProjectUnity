using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    public InputActionMap cameraSwapping;
    public InputAction swapCamera;

    public GameObject player;
    public GameObject player_camera_obj;
    public GameObject garage;

    public Cinemachine.CinemachineVirtualCamera cm_player_cam;
    public Cinemachine.CinemachineVirtualCamera cm_garage_cam;
    public Cinemachine.CinemachineBrain cm_brain;

    public Camera player_camera;
    public Camera garage_camera;

    public bool is_player_cam;

    private void OnEnable()
    {
        cameraSwapping.Enable();
        swapCamera.Enable();
    }

    private void OnDisable()
    {
        cameraSwapping.Disable();
        swapCamera.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        cm_player_cam = player_camera_obj.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cm_garage_cam = garage.GetComponent<Cinemachine.CinemachineVirtualCamera>();

        player_camera = player.GetComponent<Camera>();
        garage_camera = garage.GetComponent<Camera>();

        cm_player_cam.Priority = 10;
        cm_garage_cam.Priority = 0;

        player_camera.enabled = true;
        garage_camera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (swapCamera.triggered)
        {
            Debug.Log("Swap camera");
            if (is_player_cam)
            {
                cm_player_cam.Priority = 0;
                cm_garage_cam.Priority = 10;
            }
            else
            {
                cm_player_cam.Priority = 10;
                cm_garage_cam.Priority = 0;
            }
            is_player_cam = !is_player_cam;
        }
    }
}

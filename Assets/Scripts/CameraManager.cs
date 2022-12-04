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

    public PlayerInput player_input;

    public bool is_player_cam = true;

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
        cm_garage_cam = garage.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();

        cm_player_cam.Priority = 10;
        cm_garage_cam.Priority = 0;

        player_input = player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Swap camera");
            if (is_player_cam)
            {
                Debug.Log("swap from player");
                cm_player_cam.Priority = 0;
                cm_garage_cam.Priority = 10;
                //cm_player_cam.Follow = garage.transform;
                player_input.SwitchCurrentActionMap("Garage");
                Debug.Log(player_input.currentActionMap);
            }
            else
            {
                Debug.Log("swap from garage");
                cm_player_cam.Priority = 10;
                cm_garage_cam.Priority = 0;
                //cm_player_cam.Follow = player.transform;
                player_input.SwitchCurrentActionMap("Player");
                Debug.Log(player_input.currentActionMap);
            }
            is_player_cam = !is_player_cam;
        }
    }
}

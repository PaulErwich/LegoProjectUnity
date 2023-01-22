using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool enter_vehicle;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public Garage garage_script;
		public EngineControls engine_script;

        private void Start()
        {
			garage_script = Garage.instance;
			StartCoroutine(LateStart(1));
        }

		IEnumerator LateStart(float waitTime)
        {
			yield return new WaitForSeconds(waitTime);
			engine_script = EngineControls.instance;
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

		public void OnVehicleMove(InputValue value)
        {
			engine_script.OnVehicleMove(value);
        }
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnEnterVehicle(InputValue value)
        {
			EnterVehicleInput(value.isPressed);
        }

		public void OnExitVehicle(InputValue value)
        {
			EnterVehicleInput(value.isPressed);
        }

		public void OnVehicleInGarage(InputValue value)
        {
			garage_script.EnterGarage();
        }			

		private void OnMoveUpDown(InputValue value)
		{
			garage_script.OnMoveUpDown(value);
		}

		private void OnMoveForwardBackward(InputValue value)
		{
			garage_script.OnMoveForwardBackward(value);
		}

		private void OnMoveLeftRight(InputValue value)
		{
			garage_script.OnMoveLeftRight(value);
		}

		private void OnGarageLook(InputValue value)
		{
			garage_script.OnGarageLook(value);
		}

		private void OnPlaceBlock(InputValue value)
		{
			garage_script.OnPlaceBlock();
		}

		private void OnSpawnBlock(InputValue value)
		{
			garage_script.OnSpawnBlock();
		}

		private void OnBuildVehicle(InputValue value)
		{
			garage_script.OnBuildVehicle();
		}
		private void OnChangeBlock(InputValue value)
		{
			garage_script.OnChangeBlock(value);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void EnterVehicleInput(bool newEnterVehicleState)
        {
			enter_vehicle = newEnterVehicleState;
        }
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}
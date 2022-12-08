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

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		public Garage garage_script;

        private void Start()
        {
			garage_script = Garage.instance;
        }

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
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
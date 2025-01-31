using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace S_Camera
{
    public class SwitchCamera360 : MonoBehaviour
    {
        public CinemachineVirtualCamera[] cameras; // Array of 4 cameras
        public InputActionAsset inputActions; 

        private InputAction _nextCamera;
        private InputAction _previousCamera;

        private int _currentCamIndex;

        private void Awake()
        {
            var cameraActions = inputActions.FindActionMap("Camera");
            _nextCamera = cameraActions.FindAction("NextCamera");
            _previousCamera = cameraActions.FindAction("PreviousCamera");
        }

        private void OnEnable()
        {
            _nextCamera.Enable();
            _previousCamera.Enable();
            _nextCamera.performed += OnNextCamera;
            _previousCamera.performed += OnPreviousCamera;
        }

        private void OnDisable()
        {
            _nextCamera.performed -= OnNextCamera;
            _previousCamera.performed -= OnPreviousCamera;
            _nextCamera.Disable();
            _previousCamera.Disable();
        }

        private void Start()
        {
            SetCamera(0); // Start with Camera 1
        }

        private void OnNextCamera(InputAction.CallbackContext context)
        {
            SwitchCamera(1); // Move to the next camera
        }

        private void OnPreviousCamera(InputAction.CallbackContext context)
        {
            SwitchCamera(-1); // Move to the previous camera
        }

        private void SwitchCamera(int direction)
        {
            _currentCamIndex = (_currentCamIndex + direction + cameras.Length) % cameras.Length;
            SetCamera(_currentCamIndex);
        }

        private void SetCamera(int index)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].Priority = (i == index) ? 10 : 0; // Active camera gets priority 10, others 0
            }
        }
    }
}
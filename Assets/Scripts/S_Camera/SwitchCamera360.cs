using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace S_Camera
{
    public class SwitchCamera360 : MonoBehaviour
    {
        public CinemachineVirtualCamera[] cameras;
        private int _currentCamIndex;
        [SerializeField] InputActionAsset inputActions;

        private InputAction _nextCamera;
        private InputAction _previousCamera;

        public static Transform ActiveCameraTransform { get; private set; }  // Store active camera

        private void Awake()
        {
            var cameraActions = inputActions.FindActionMap("Camera");
            _nextCamera = cameraActions.FindAction("NextCamera");
            _previousCamera = cameraActions.FindAction("PreviousCamera");

            if (cameras.Length > 0)
                SetActiveCamera(0); // Start with the first camera
        }

        private void OnEnable()
        {
            if (_nextCamera == null || _previousCamera == null)
                return;

            _nextCamera.Enable();
            _previousCamera.Enable();
            _nextCamera.performed += OnNextCamera;
            _previousCamera.performed += OnPreviousCamera;
        }

        private void OnDisable()
        {
            if (_nextCamera != null)
                _nextCamera.performed -= OnNextCamera;

            if (_previousCamera != null)
                _previousCamera.performed -= OnPreviousCamera;

            _nextCamera?.Disable();
            _previousCamera?.Disable();
        }

        private void OnNextCamera(InputAction.CallbackContext context)
        {
            SwitchCamera(1);
        }

        private void OnPreviousCamera(InputAction.CallbackContext context)
        {
            SwitchCamera(-1);
        }

        private void SwitchCamera(int direction)
        {
            if (cameras == null || cameras.Length == 0)
                return;

            _currentCamIndex = (_currentCamIndex + direction + cameras.Length) % cameras.Length;
            SetActiveCamera(_currentCamIndex);
        }

        private void SetActiveCamera(int index)
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] != null)
                    cameras[i].Priority = (i == index) ? 10 : 0;
            }

            // Update the active camera reference
            if (cameras[index] != null)
                ActiveCameraTransform = cameras[index].transform;
        }
    }
}

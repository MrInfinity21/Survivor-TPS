using UnityEngine;
using Unity.Cinemachine;
public class AimStateManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        RotatePlayerToCamera();
    }

    private void RotatePlayerToCamera()
    {
        Vector3 viewDirection = cameraTransform.forward;
        viewDirection.y = 0f;
        viewDirection.Normalize();

        if (viewDirection != Vector3.zero)
            transform.forward = viewDirection;  
    }
}

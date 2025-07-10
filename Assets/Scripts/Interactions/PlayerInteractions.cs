using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform cam;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //we never ever ever ever ever want to use KeyCode. You can only use things like this for debugging, not for actual gameplay.
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactableLayer))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Door door = hit.collider.GetComponentInParent<Door>();
                if (door != null)
                {
                    door.OpenDoor();
                }
            }
        }
    }
}

using UnityEngine;

public class Door : MonoBehaviour
{

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;

    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 1f;
    [SerializeField] private float closeDelay = 5f;

    private float t = 0f;

    private void Start()
    {
        
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0,openAngle,0));


    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(RotateDoor(openRotation));
            Invoke(nameof(CloseDoor), closeDelay);
            isOpen = true;

        }
    }

    private void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(RotateDoor(closedRotation));
        isOpen = false;
    }

    System.Collections.IEnumerator RotateDoor(Quaternion targetRotation)
    {
        Quaternion startRot = transform.rotation;
        t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(startRot, targetRotation, t);
            yield return null;
        }

        transform.rotation = targetRotation;
    }



}

using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _target;

    private void Update()
    {
        Vector3 direction = _target.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }
}

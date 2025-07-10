using UnityEngine;
using UnityEngine.AI;

public class BadGuyController : MonoBehaviour
{
    private NavMeshAgent _badGuyController;
    [SerializeField] private Transform _player;

    private void Awake()
    {
        _badGuyController = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {   
        _badGuyController.speed = 50f;
        _badGuyController.destination = _player.position;
    }
}

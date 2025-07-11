using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent))]
public class BadGuyController : MonoBehaviour
{

    [Header("Patrol Settings")]
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _patrolSpeed = 2f;
    private int _currentPatrolIndex = 0;
    private bool _isChasing = false;
    private Tween _patrolTween;

    [Header("Player Detection")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _detectionRange = 10f;

    private NavMeshAgent _badEnemy;
    

    private void Awake()
    {
        _badEnemy = GetComponent<NavMeshAgent>();
        _badEnemy.enabled = false; // Disable during patrol with DOTween
        _badEnemy.enabled = false;
    }

    private void Start()
    {
        SnapToNavMesh();
        StartPatrolling();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if(!_isChasing && distanceToPlayer <= _detectionRange)
        {
      
                StartChasing();
        }
        
        if (_isChasing && _badEnemy.enabled && _badEnemy.isOnNavMesh)
        {
            _badEnemy.SetDestination(_player.position);

            Vector3 direction = (_player.position - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }

    private void StartPatrolling()
    {
        if (_patrolPoints.Length == 0) return;

        MoveToNextPatrolPoint();
    }

    private void MoveToNextPatrolPoint()
    {
        Transform target = _patrolPoints[_currentPatrolIndex];
        float distance = Vector3.Distance(transform.position, target.position);

        Vector3 lookTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        

        transform.DOLookAt(target.position, 0.5f);

        _patrolTween = transform.DOMove(target.position, distance / _patrolSpeed)
        .SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Length;
            MoveToNextPatrolPoint();
        });

    }

    private void StartChasing()
    {
        _isChasing = true;
        _patrolTween?.Kill();
        SnapToNavMesh();
        _badEnemy.enabled = true;               
    }
    
    private void SnapToNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            _badEnemy.enabled = true;
        }
        else
        {
            Debug.LogWarning("Enemy is not on a valid NavMesh position!");
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (_player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRange);
        }
    }




    /*private NavMeshAgent _badGuyController;
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
    }*/
}

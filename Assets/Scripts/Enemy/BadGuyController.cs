using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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

    [Header("Attack Settings")]
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private float _shootCooldown = 1f;
    [SerializeField] private float _shootRange = 15f;
    [SerializeField] private float _stoppingDistance = 10f;
    [SerializeField] private LayerMask _lineOfSightMask;
    private float _lastShootTime = 0f;

    private NavMeshAgent _badEnemy;
    

    private void Awake()
    {
        _badEnemy = GetComponent<NavMeshAgent>();
        _badEnemy.enabled = false; // Disable during patrol with DOTween
       
    }

    private void Start()
    {
        SnapToNavMesh();
        StartPatrolling();
    }

    private void Update()
    {
        if (_player == null || !_badEnemy.isOnNavMesh) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if(!_isChasing && distanceToPlayer <= _detectionRange)
        {
      
                StartChasing();
        }
        
        if (_isChasing && _badEnemy.enabled)
        {

            if (distanceToPlayer > _stoppingDistance)
            {
                _badEnemy.isStopped = false;
                _badEnemy.SetDestination(_player.position);
            }
            else
            {
                _badEnemy.ResetPath();
                _badEnemy.isStopped = true;
                AimTowardsPlayer();
                TryShootAtPlayer();
            }

        }
        
    }

    private void StartPatrolling()
    {
        if (_patrolPoints == null || _patrolPoints.Length == 0) return;

        MoveToNextPatrolPoint();
    }

    private void MoveToNextPatrolPoint()
    {
        if (_patrolPoints.Length == 0) return;

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
    

    private void AimTowardsPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void TryShootAtPlayer()
    {
        if (Time.time < _lastShootTime + _shootCooldown) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (distanceToPlayer > _shootRange) return;
        if (_shootOrigin == null || _projectilePrefab == null) return;

        Vector3 dir = (_player.position - _shootOrigin.position).normalized;
        Shoot(dir);
        _lastShootTime = Time.time;
    }

    private void Shoot(Vector3 direction)
    {
        GameObject proj = Instantiate(_projectilePrefab, _shootOrigin.position, Quaternion.LookRotation(direction));
        EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
        if (ep != null)
        {
            ep.Init(direction);
        }
    
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

        if (_shootOrigin != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_shootOrigin.position, _shootOrigin.position + transform.forward * 1f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _stoppingDistance);
    }



    /*private NavMeshAgent _badGuyController;
    [SerializeField] private Transform _player;

    private void Awake()
    {
        _badGuyController = Get
Component<NavMeshAgent>();
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

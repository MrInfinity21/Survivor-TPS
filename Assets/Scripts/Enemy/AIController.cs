using System.Collections;
using SeanExample;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is your AI controller. Changes are made
/// </summary>
public class AIController : MonoBehaviour
{
    //variables should only be public as properties.
    private NavMeshAgent _navMeshAgent; //all variables that are public should be properties. The reason: changing a variable in an object should be handled by that object.
    //Objects should handle their own data.
    [SerializeField]private float _startWaitTime = 4; //in Unity, SerializeField is meant for when you want to change a value in the editor, but have it not changeable by other objects
    [SerializeField]private float _timeToRotate = 2;
    [SerializeField]private float _speedWalk = 6;
    [SerializeField]private float _speedRun = 9;

    [SerializeField]private float _viewRadius = 15;
    [SerializeField]private float _viewAngle = 90;
    [SerializeField]private LayerMask _playerMask;
    [SerializeField]private LayerMask _obstacleMask;
    [SerializeField]private float _meshResolution = 1f;
    [SerializeField]private int _edgeIterations = 4;
    [SerializeField]private float _edgeDistance = 0.5f;
    [SerializeField] private float _agentCheckTime = 1f;
    [SerializeField]private Transform[] _waypoints;
    int m_CurrentWaypointIndex;

    Vector3 _playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float _waitTime;
    float _currentTimeToRotate;
    bool _playerInRange;
    bool _playerNear;
    bool _isPatrol;
    bool _caughtPlayer;

    Transform _player;


    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>(); //You can get a component easily, and this tends to be easier, as there's less set up.
    }
    void Start()
    {
        
        m_PlayerPosition = Vector3.zero;
        _isPatrol = true;
        _caughtPlayer = false;
        _playerInRange = false;    
        _waitTime = _startWaitTime;
        _currentTimeToRotate = _timeToRotate;

        m_CurrentWaypointIndex = 0;
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _player = PlayerManager.Instance.Player.transform; //this allows you to grab the player from anywhere without look ups and settings tags

        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = _speedWalk;
        _navMeshAgent.SetDestination(_waypoints[m_CurrentWaypointIndex].position);
        StartCoroutine(EnemyHandler());
    }

    IEnumerator EnemyHandler()
    {
        while (true) //change this later to a death condition
        {
            //in here, implement something to handle how often you check for the player; so this way you don't have the enemy always calculating their navmesh direction
            yield return new WaitForSeconds(_agentCheckTime); //only update the enemy based on some time value
        }
    }
    void Update()
    {
        EnviromentView();
        if (!_isPatrol)
        {
            Chasing(); //in chasing, just set a variable for your animator
        }
        else
        {
            Patroling();
        }


    }

    private void CheckChasing()
    {
        
    }

    private void CheckPatroling()
    {
        
    }

    private void Chasing()
    {
        _playerNear = false;
        _playerLastPosition = Vector3.zero;

        if (!_caughtPlayer)
        {
            Move(_speedRun);
            _navMeshAgent.SetDestination(m_PlayerPosition);
        }
        if(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            if (_waitTime <= 0 && !_caughtPlayer && Vector3.Distance(transform.position, _player.position) >= 6f)
            {
                _isPatrol = true;
                _playerNear = false;
                Move(_speedWalk);
                _currentTimeToRotate = _timeToRotate;
                _waitTime = _startWaitTime;
                _navMeshAgent.SetDestination(_waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if(Vector3.Distance(transform.position, _player.position) >= 2.5f)
                {
                    Stop();
                    _waitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Patroling()
    {
        if (_playerNear)
        {
            if(_currentTimeToRotate <= 0)
            {
                Move(_speedWalk);
                LookingPlayer(_playerLastPosition);
            }
            else
            {
                Stop();
                _currentTimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            _playerNear = false;
            _playerLastPosition = Vector3.zero;
            _navMeshAgent.SetDestination(_waypoints[m_CurrentWaypointIndex].position);
            if(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if(_waitTime <= 0)
                {
                    NextPoint();
                    Move(_speedWalk);
                    _waitTime = _startWaitTime;
                }
                else
                {
                    Stop();
                    _waitTime -= Time.deltaTime;
                }
            }
        }
    }

    void Move(float speed)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
    }

    void Stop()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex+1) % _waypoints.Length;
        _navMeshAgent.SetDestination(_waypoints[m_CurrentWaypointIndex].position);
    }

    void CaughtPlayer()
    {
        _caughtPlayer = true;
    }

    void LookingPlayer(Vector3 player)
    {
        _navMeshAgent.SetDestination(player);
        if(Vector3.Distance(transform.position, player) <= 0.3)
        {
            if(_waitTime <= 0)
            {
                _playerNear = false;
                Move(_speedWalk);
                _navMeshAgent.SetDestination(_waypoints[m_CurrentWaypointIndex].position);
                _waitTime = _startWaitTime;
                _currentTimeToRotate = _timeToRotate;
            }
            else
            {
                Stop();
                _waitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, _viewRadius, _playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < _viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, _obstacleMask))
                {
                    _playerInRange = true;
                    _isPatrol = false;
                    m_PlayerPosition = transform.position;
                    return;
                }
                else
                {
                    _playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > _viewRadius)
            {
                _playerInRange = false;
            }
        }
    }
}

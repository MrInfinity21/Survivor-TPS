using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _damage = 50f;
    [SerializeField] private LayerMask _layerMask;

    private float _timer;

    private void Awake()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & _layerMask) == 0) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    public void Init(Vector3 direction)
    {
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction.normalized);
    }
}

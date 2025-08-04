using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 5f;
    [SerializeField] private float _damage = 25f;
    private float _timer;


    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeToDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null )
            {
                enemyHealth.TakeDamage(_damage);
            }
        }
        Destroy(this.gameObject);
    }
}

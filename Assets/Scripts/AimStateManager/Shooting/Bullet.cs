using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _timeToDestroy = 5f;
    float _timer;


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
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
}

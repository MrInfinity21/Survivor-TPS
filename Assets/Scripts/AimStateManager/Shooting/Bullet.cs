using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _timeToDestroy;
    float _timer;
    void Start()
    {
            
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeToDestroy) Destroy(this.gameObject); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}

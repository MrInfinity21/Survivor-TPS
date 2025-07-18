using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("Fire Rate")]
    [SerializeField] float _fireRate;
    [SerializeField] bool _semiAuto;
    float _fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _barrelPos;
    [SerializeField] float _bulletVelocity;
    [SerializeField] int _bulletsPerShot;
    AimStateManager _aim;

    [SerializeField] AudioClip _gunShot;
    AudioSource _audioSource;
    WeaponAmmo _ammo;

    ActionStateManager _actions;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _aim = GetComponentInParent<AimStateManager>();
        _ammo = GetComponent<WeaponAmmo>();
        _actions = GetComponentInParent<ActionStateManager>();
        _fireRateTimer = _fireRate;    
    }

    void Update()
    {
        if (shouldFire()) fire();
        Debug.Log(_ammo._currentAmmo);
    }

    bool shouldFire()
    {
        _fireRateTimer += Time.deltaTime;
        if (_fireRateTimer < _fireRate) return false;
        if(_ammo._currentAmmo == 0) return false;
        if (_actions._currentState == _actions.Reload) return false;
        if (_semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!_semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void fire()
    {
        _fireRateTimer = 0;
        _barrelPos.LookAt(_aim._aimPos);
        _audioSource.PlayOneShot(_gunShot);
        _ammo._currentAmmo--;
        for(int i = 0; i < _bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(_bullet, _barrelPos.position, _barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(_barrelPos.forward * _bulletVelocity, ForceMode.Impulse);
        }
        
    }
}

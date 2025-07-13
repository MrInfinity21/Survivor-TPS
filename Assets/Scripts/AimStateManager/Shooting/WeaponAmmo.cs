using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int _clipSize;
    public int _extraAmmo;
    [HideInInspector] public int _currentAmmo;
    void Start()
    {
        _currentAmmo = _clipSize;
    }


    public void Reload()
    {
        if(_extraAmmo >= _clipSize)
        {
            int ammoToReload = _clipSize - _currentAmmo;
            _extraAmmo -= ammoToReload;
            _currentAmmo += ammoToReload;
        }
        else if( _extraAmmo > 0)
        {
            if(_extraAmmo + _currentAmmo > _clipSize)
            {
                int leftOverAmmo = _extraAmmo + _currentAmmo - _clipSize;
                _extraAmmo = leftOverAmmo;
                _currentAmmo = _clipSize;
            }
            else
            {
                _currentAmmo += _extraAmmo;
                _extraAmmo = 0;
            }
        } 
    }

  

}

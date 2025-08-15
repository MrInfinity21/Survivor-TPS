using TMPro;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [Header("Ammo Settings")]
    public int _clipSize;
    public int _extraAmmo;
    [HideInInspector] public int _currentAmmo;

    [Header("Audio")]
    public AudioClip _magInSound;
    public AudioClip _magOutSound;
    public AudioClip _releaseSlideSound;

    [Header("UI")]
    public TextMeshProUGUI ammoText;
    void Start()
    {
        _currentAmmo = _clipSize;
        UpdateAmmoUI();
    }

    private void Update()
    {
        UpdateAmmoUI();
    }

    public void Shoot()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo--;
            UpdateAmmoUI();
        }
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
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = _currentAmmo + " / " + _extraAmmo;
        }
    }

  

}

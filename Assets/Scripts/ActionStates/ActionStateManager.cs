using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    [HideInInspector] public ActionBaseState _currentState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();

    public GameObject _currentWeapon;
    [HideInInspector] public WeaponAmmo _ammo;

    [HideInInspector] public Animator _anim;

    public MultiAimConstraint _rHandAim;
    public MultiAimConstraint _lHandAim;
    AudioSource _audioSource;
    void Start()
    {
        SwitchState(Default);
        _ammo = _currentWeapon.GetComponent<WeaponAmmo>();
        _audioSource = _currentWeapon.GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();

    }

    
    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    public void WeaponReloaded()
    {
        _ammo.Reload();
        SwitchState(Default);
    }

    public void MagOut()
    {
        _audioSource.PlayOneShot(_ammo._magOutSound);
    }

    public void MagIn()
    {
        _audioSource.PlayOneShot(_ammo._magInSound);
    }

    public void ReleaseSlide()
    {
        _audioSource.PlayOneShot(_ammo._releaseSlideSound);
    }
}

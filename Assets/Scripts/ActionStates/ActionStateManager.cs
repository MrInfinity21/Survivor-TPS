using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActionStateManager : MonoBehaviour
{
    ActionBaseState _currentState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();

    public GameObject _currentWeapon;
    [HideInInspector] public WeaponAmmo _ammo;

    [HideInInspector] public Animator _anim;

    public MultiAimConstraint _rHandAim;
    public MultiAimConstraint _lHandAim;
    void Start()
    {
        SwitchState(Default);
        _ammo = _currentWeapon.GetComponent<WeaponAmmo>();
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

    public void ReloadState()
    {
        _ammo.Reload();
        SwitchState(Default);
    }
}

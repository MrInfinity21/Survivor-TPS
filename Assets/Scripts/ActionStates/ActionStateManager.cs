using UnityEngine;

public class ActionStateManager : MonoBehaviour
{
    ActionBaseState _currentState;

    public ReloadState Reload = new ReloadState();
    public DefaultState Default = new DefaultState();

    public GameObject _currentWeapon;
    [HideInInspector] public WeaponAmmo _ammo;

    [HideInInspector] public Animator _anim;
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
    }
}

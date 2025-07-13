using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager _aim)
    {
        _aim._anim.SetBool("Aiming", true);
        _aim._currentFov = _aim._adsFov;
    }

    public override void UpdateState(AimStateManager _aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) _aim.SwitchState(_aim.Hip);
    }
}

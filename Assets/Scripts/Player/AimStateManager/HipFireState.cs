using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager _aim)
    {
        _aim._anim.SetBool("Aiming", false);
        _aim._currentFov = _aim._hipFov;
    }

    public override void UpdateState(AimStateManager _aim)
    {
        if (Input.GetKey(KeyCode.Mouse1)) _aim.SwitchState(_aim.Aim);
    }
}

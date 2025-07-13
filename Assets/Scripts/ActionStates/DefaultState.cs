using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._rHandAim.weight = 1f;
        actions._lHandAim.weight = 1f;
    }

    public override void UpdateState(ActionStateManager actions)
    {
        if(Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.SwitchState(actions.Reload);
        }
    }

    bool CanReload(ActionStateManager action)
    {
        if(action._ammo._currentAmmo == action._ammo._clipSize) return false;
        else if(action._ammo._extraAmmo == 0) return false;
        else return true;
    }
}

using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {

        actions._rHandAim.weight = 0f;
        actions._lHandAim.weight = 0f;
        actions._anim.SetTrigger("Reloading");
    }

    public override void UpdateState(ActionStateManager actions)
    {
       
    }
}

using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._anim.SetTrigger("Reload");
    }

    public override void UpdateState(ActionStateManager actions)
    {
        
    }
}

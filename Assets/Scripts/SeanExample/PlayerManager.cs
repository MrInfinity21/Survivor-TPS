using System;
using UnityEngine;

namespace SeanExample
{
    public class PlayerManager : Singleton<PlayerManager>//this keep track of the player and access to the player
    {
        [field: SerializeField]public MovementStateManager Player { get; private set; }

        private void Start()
        {
            if (!Player)
                Player = FindFirstObjectByType<MovementStateManager>(); //if we don't have a player assigned to us in the inspector, we can simply look for it in the scene.
        }
    }
}
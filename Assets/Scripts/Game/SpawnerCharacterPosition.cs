using System;
using UnityEngine;

namespace Game.Game.CharacterSpawner
{
    [Serializable]
    public class SpawnerCharacterPosition
    {
        public Transform Position;
        public bool IsFree = true;
    }
}

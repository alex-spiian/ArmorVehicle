using System;
using DefaultNamespace;
using UnityEngine;

namespace Death
{
    public class DeathHandler
    {
        public void OnSomeoneDied(IMortal character)
        {
            switch (character.CharacterType)
            {
                case CharacterType.Enemy:
                    Debug.Log("enemy is dead");
                    break;
                case CharacterType.Player:
                    Debug.Log("player is dead");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(character.CharacterType), character.CharacterType, null);
            }
        }

        public void Test()
        {
            Debug.Log("Test");
        }
    }
}
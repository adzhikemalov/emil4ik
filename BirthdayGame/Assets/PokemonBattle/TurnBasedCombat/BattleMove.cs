using UnityEngine;

namespace PokemonBattle.TurnBasedCombat
{
    [CreateAssetMenu(fileName = "BattleMove", menuName = "Create battle move", order = 0)]
    public class BattleMove : ScriptableObject
    {
        public enum MoveType
        {
            Common,
            Grass,
            Fire,
            Water,
            Battle
        }
        
        public enum AnimationName
        {
            Attack,
            Attack1
        }

        public string Name;
        public MoveType Type;
        public int Damage;
        public AnimationName Animation;
    }
}
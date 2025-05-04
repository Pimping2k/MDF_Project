using Enums;
using UnityEngine;

namespace ConfigsScripts
{
    [CreateAssetMenu(fileName = "CardCFG_New", menuName = "MDF/Configs/CardConfig")]
    public class CardConfig : ScriptableObject
    {
        [Header("Components")]
        [SerializeField] [Tooltip("Token is a upgrade for a card any passive effect or any stats")]
        private Token _token;

        [SerializeField] [Tooltip("Active ability with appliable effect")]
        private ActiveAbilities _activeAbility;

        [Header("Other")] 
        [SerializeField] [Min(0)] private int _id;

        public int ID => _id;
        public Token Token => _token;
        public ActiveAbilities ActiveAbility => _activeAbility;
    }
}
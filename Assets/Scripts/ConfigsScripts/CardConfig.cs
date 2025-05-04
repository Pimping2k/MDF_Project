using Enums;
using UnityEngine;

namespace ConfigsScripts
{
    [CreateAssetMenu(fileName = "CardCFG_New", menuName = "MDF/Configs/CardConfig")]
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private Token _token;
        [SerializeField] [Min(0)] private int _id;

        public int ID => _id;
        public Token Token => _token;
    }
}
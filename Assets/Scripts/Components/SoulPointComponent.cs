using UnityEngine;

namespace Components
{
    public class SoulPointComponent : MonoBehaviour
    {
        [SerializeField] private int soulPoint;

        public int SoulPoint
        {
            get => soulPoint;
            set => soulPoint = value;
        }

        public int IncreaseSoulPoint(int value)
        {
            soulPoint += value;
            return soulPoint;
        }

        public int DecreaseSoulPoint(int value)
        {
            soulPoint -= value;
            return value;
        }
    }
}
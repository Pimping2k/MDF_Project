using Interfaces;
using UnityEngine;

namespace Components
{
    public class PassiveCardComponent : MonoBehaviour, IPassivable
    {
        public void ApplyPassiveEffect()
        {
            throw new System.NotImplementedException();
        }

        public void PowerUpPassiveEffect(int value)
        {
            throw new System.NotImplementedException();
        }

        public void PowerUpPassiveEffect(float value)
        {
            throw new System.NotImplementedException();
        }
    }
}
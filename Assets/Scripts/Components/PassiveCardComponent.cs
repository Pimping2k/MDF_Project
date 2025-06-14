using Interfaces;
using UnityEngine;
using System.Collections.Generic;

namespace Components
{
    public class PassiveCardComponent : MonoBehaviour, IPassivable
    {
        public List<GameObject> summonedEntities;
        
        public void ApplyPassiveEffect()
        {
            //throw new System.NotImplementedException();
        }

        public void PowerUpPassiveEffect(int value)
        {
            //throw new System.NotImplementedException();
        }

        public void PowerUpPassiveEffect(float value)
        {
            //throw new System.NotImplementedException();
        }
    }
}
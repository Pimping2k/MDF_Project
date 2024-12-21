using System;
using System.Collections.Generic;
using CardScripts;
using DefaultNamespace;
using UnityEngine;

namespace CoreMechanic
{
    public class TableCardBehaviour : MonoBehaviour
    {
        [SerializeField] public List<GameObject> firstRow = new List<GameObject>();
        [SerializeField] public List<GameObject> secondRow = new List<GameObject>();
        [SerializeField] public List<GameObject> thirdRow = new List<GameObject>();

        public List<List<GameObject>> rows;

        private void Awake()
        {
            rows = new List<List<GameObject>>() { firstRow, secondRow, thirdRow };
        }
    }
}
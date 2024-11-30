using System;
using System.Collections.Generic;
using CardScripts;
using DefaultNamespace;
using UnityEngine;

namespace CoreMechanic
{
    public class TableCardBehaviour : MonoBehaviour
    {
        [SerializeField] private List<GameObject> firstRow = new List<GameObject>();
        [SerializeField] private List<GameObject> secondRow = new List<GameObject>();
        [SerializeField] private List<GameObject> thirdRow = new List<GameObject>();

        private List<List<GameObject>> rows;

        private void Awake()
        {
            rows = new List<List<GameObject>>() { firstRow, secondRow, thirdRow };
        }


        public void StartProcess()
        {
            foreach (var row in rows)
            {
                CheckRow(row);
            }
        }

        private void CheckRow(List<GameObject> row)
        {
            if (row == null)
                return;

            for (int i = 0; i < row.Count; i++)
            {
                if (row[i] != null)
                {
                }
            }
        }
    }
}
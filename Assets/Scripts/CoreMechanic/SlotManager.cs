using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoreMechanic
{
    public class SlotManager : MonoBehaviour
    {
        public static SlotManager Instance;
        private Dictionary<int, Slot> slots = new Dictionary<int, Slot>();
        [SerializeField] private Slot[] AllSlots = new Slot[9];
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            foreach(var slot in AllSlots)
            {
                slots.Add(slot.ID,slot);
            }
        }

        public Slot GetSlotByID(int slotID)
        {
            return slots.ContainsKey(slotID) ? slots[slotID] : null;
        }

        public void ClearSlot(int slotID)
        {
            if (slots.ContainsKey(slotID))
            {
                slots[slotID].ClearSlot();
            }
        }
    }
}
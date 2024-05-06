using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UI
{
    public struct DiceQueueItem
    {
        public GameObject DicePrefab;
        public int DiceQuantity;
    }
    
    public class SettingsPanelManager : MonoBehaviour
    {
        public Button rollButton;
        public DiceInputField[] diceInputFields;

        private void Start()
        {
            rollButton.onClick.AddListener(OnRollButtonClicked);
        }
        
        private void OnRollButtonClicked()
        {
            var diceQueue = new Queue<DiceQueueItem>();

            foreach (var diceType in diceInputFields)
            {
                if (!diceType.isActiveAndEnabled) continue;
                
                var diceOfThisType = diceType.value;
                for (var j = 0; j < diceOfThisType; j++)
                {
                    diceQueue.Enqueue(new DiceQueueItem()
                    {
                        DicePrefab = diceType.dicePrefab,
                        DiceQuantity = diceOfThisType
                    });
                }
            }

            // Roll the dice
            DiceRoller.Instance.AddToQueueAndRoll(diceQueue);
        }

        private void OnDestroy()
        {
            rollButton.onClick.RemoveListener(OnRollButtonClicked);
        }
    }
}

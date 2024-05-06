using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DiceInputField : MonoBehaviour
    {
        public int value = 0;
        
        [Header("Components")]
        public GameObject dicePrefab;
        
        [Header("UI elements")]
        public Button increaseButton;
        public Button decreaseButton;
        
        private TMP_InputField _inputField;
        
        private void Start()
        {
            _inputField = GetComponentInChildren<TMP_InputField>();
            
            _inputField.onEndEdit.AddListener(UpdateValue);
            
            increaseButton.onClick.AddListener(OnIncreaseValue);
            decreaseButton.onClick.AddListener(OnDecreaseValue);
        }

        private void OnEnable()
        {
            UpdateValue("1");
        }

        private void OnDisable()
        {
            value = 0;
        }
        
        private void OnIncreaseValue()
        {
            if (value >= 99)
            {
                return;
            }
            value++;
            
            _inputField.text = value.ToString();
        }
        
        private void OnDecreaseValue()
        {
            if (value <= 1)
            {
                return;
            }
            value--;
            
            _inputField.text = value.ToString();
        }

        private void UpdateValue(string newVal)
        {
            var parsedVal = int.Parse(newVal);
            value = Mathf.Clamp(parsedVal,1, 99);
        }

        private void OnDestroy()
        {
            increaseButton.onClick.RemoveAllListeners();
            decreaseButton.onClick.RemoveAllListeners();
            _inputField.onValueChanged.RemoveAllListeners();
        }
    }
}

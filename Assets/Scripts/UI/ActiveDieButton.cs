using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActiveDieButton : MonoBehaviour
    {
        public DiceInputField diceInputField;
        private Toggle _toggle;
        
        private void Start()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
        
        private void OnToggleValueChanged(bool isOn)
        {
            diceInputField.gameObject.SetActive(isOn);
        }
        //
    }
}

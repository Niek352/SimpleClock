using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Clock
{
    public class HoursInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _hoursInputField;
        private readonly Regex rgx = new Regex("[^0-9]");

        [SerializeField] private int _hourValue;
        public int HourValue { get => _hourValue; }


        private void Awake()
        {
            _hoursInputField.onSelect.AddListener(x => TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad, false));
            _hoursInputField.onValidateInput += HourValidate;
            _hoursInputField.onEndEdit.AddListener(OnEndHours);
        }

        private void OnEndHours(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                return;

            int parsed = int.Parse(arg);
            SetValue(Mathf.Clamp(parsed, 0, 23));
            
        }

        private char HourValidate(string text, int charIndex, char addedChar)
        {
            

            if (charIndex == 2)
                return '\0';

            var x = rgx.Match($"{addedChar}");

            return !x.Success ? addedChar : '\0';

        }

        public void SetValue(int value)
        {
            _hourValue = value;
            _hoursInputField.text = _hourValue.ToString("00");
        }
    }


}
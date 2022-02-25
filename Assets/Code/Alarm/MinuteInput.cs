using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Clock.Alarm
{

    public class MinuteInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _minuteInputField;

        [SerializeField] private int _minuteValue;

        private readonly Regex rgx = new Regex("[^0-9]");

        public int MinuteValue { get => _minuteValue; }

        private void Awake()
        {
            _minuteInputField.onSelect.AddListener(x => TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad, false));
            _minuteInputField.onValidateInput += MinuteValidate;
            _minuteInputField.onEndEdit.AddListener(OnEndMinutes);
        }

        private char MinuteValidate(string input, int charIndex, char addedChar)
        {
            if (charIndex == 2)
                return '\0';

            var x = rgx.Match($"{addedChar}");

            return !x.Success ? addedChar : '\0';
        }
        private void OnEndMinutes(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                return;

            int parsed = int.Parse(arg);
            SetValue(Mathf.Clamp(parsed, 0, 59));

        }

        public void SetValue(int value)
        {
            _minuteValue = value;
            _minuteInputField.text = _minuteValue.ToString("00");
        }

    }

}
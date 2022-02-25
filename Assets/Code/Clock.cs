using System.Collections;
using TMPro;
using UnityEngine;

namespace Clock
{
    public class Clock : MonoBehaviour
    {
        private const float hoursToDegrees = 360f / 12f;
        private const float minutesToDegrees = 360f / 60f;
        private const float secondsToDegrees = 360f / 60f;
        [SerializeField] private Transform _secondHand;
        [SerializeField] private Transform _minuteHand;
        [SerializeField] private Transform _hoursHand;

        [SerializeField] private TMP_Text _textClock;
        [SerializeField] private DateGetter _dateGetter;
        [SerializeField, Range(0, 10000)] private float _clockSpeed;
        [SerializeField] private float _currentTime;

        [Header("Debug")]
        [SerializeField] private bool _useLocalTime;
        [SerializeField] private float _startLocalTime;

        public float CurrentTime { get => _currentTime; }

        private void Start()
        {
            _dateGetter = new DateGetter();


            if (_useLocalTime)
            {
                SetDate(_startLocalTime);
            }
            else
            {
                StartCoroutine(SetupDate());
            }
        }

        private void FixedUpdate()
        {

            _currentTime += Time.fixedDeltaTime * _clockSpeed;

            UpdateClock(_currentTime);

        }

        private IEnumerator SetupDate()
        {
            yield return _dateGetter.GetCurrentDateTime();

            while (_dateGetter.IsComplete == false)
            {
                yield return null;
            }


            SetDate(_dateGetter.CurrentTimestamp);
        }


        private void UpdateClock(float timestamp)
        {
            CaclulateHands(timestamp);
            CalculateElectronicClock(timestamp);
        }

        private void SetDate(float timestamp)
        {
            _currentTime = timestamp % 86400;
        }
        private void CalculateElectronicClock(float timestamp)
            => _textClock.text = Mathf.Floor(GetHoursFromTimeSpan(timestamp)).ToString("00") + " : " + Mathf.Floor(GetMinutesFromTimeSpan(timestamp)).ToString("00");


        private void CaclulateHands(float timestamp)
        {
            _secondHand.localRotation = Quaternion.Euler(0f, 0f, timestamp * -secondsToDegrees);
            _minuteHand.localRotation = Quaternion.Euler(0f, 0f, GetMinutesFromTimeSpan(timestamp) * -minutesToDegrees);
            _hoursHand.localRotation = Quaternion.Euler(0f, 0f, GetHoursFromTimeSpan(timestamp) * -hoursToDegrees);
        }

        private float GetMinutesFromTimeSpan(float span)
            => span / 60 % 60;
        private float GetHoursFromTimeSpan(float span)
            => span / 60 / 60 % 24;



    }
}

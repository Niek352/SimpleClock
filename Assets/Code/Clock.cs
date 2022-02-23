using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;

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
        [SerializeField] private DateGetter dateGetter;
        
        
        [SerializeField, Range(0,10000)] private float _clockSpeed;
        [SerializeField] private float _currentTime;
        private DateTime _dateTime;


        private void Start()
        {
            StartCoroutine(SetupDate());
            
               
        }
        


        private void FixedUpdate()
        {
            _dateTime = DateTime.Now;

            _currentTime += Time.fixedDeltaTime * _clockSpeed;

            UpdateClock(_currentTime); 

        }

        private IEnumerator SetupDate()
        {
            yield return dateGetter.GetCurrentDateTime();

            while (dateGetter.IsComplete == false)
            {
                yield return null;
            }


            SetDate(dateGetter.CurrentTimestamp);
        }


        private void UpdateClock(float timestamp)
        {
            CaclulateHands(timestamp);
            CalculateElectronicClock(timestamp);
        }

        private void SetDate(float timestamp)
        {
            _currentTime = timestamp;
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

using UnityEngine;
using UnityEngine.UI;

namespace Clock.Alarm
{
    public partial class ClockAlarm : MonoBehaviour
    {
        [SerializeField] private Clock _clock;
        [SerializeField] private Button _addAlarmButton;
        [SerializeField] private Button _stopAlarmButton;

        [SerializeField] private AudioSource _alarmSource;

        [SerializeField] private float _alarmTimeStamp;

        [SerializeField] private MinuteInput _minuteInput;
        [SerializeField] private HoursInput _hourInput;


        [SerializeField] private GameObject _addAlarmWindow;

        public bool AlarmIsActiv;


        private void Awake()
        {
            _addAlarmButton.onClick.AddListener(AddAlarm);

            _stopAlarmButton.onClick.AddListener(StopAlarm);
        }

        private void FixedUpdate()
        {
            if (AlarmIsActiv)
            {
                CheckClockOnAlarm(_clock.CurrentTime);
            }
        }


        public void SetMinute(int value)
        {
            _minuteInput.SetValue(value);
        }
        public void SetHour(int value)
        {
            _hourInput.SetValue(value);
        }
        private void CheckClockOnAlarm(float timeStamp)
        {
            if (timeStamp % 86400 >= _alarmTimeStamp)
            {
                RaiseAlarm();
            }
        }

        private void RaiseAlarm()
        {
            AlarmIsActiv = false;
            _alarmSource.Play();
            _stopAlarmButton.gameObject.gameObject.SetActive(true);
        }

        public void StopAlarm()
        {
            _stopAlarmButton.gameObject.gameObject.SetActive(false);
            _alarmSource.Stop();
            _alarmTimeStamp = 0;
            AlarmIsActiv = false;
        }


        private void AddAlarm()
        {
            int secFromMin = GetSecondFromMinute(_minuteInput.MinuteValue);
            int secFromHour = GetSecondFromHours(_hourInput.HourValue);

            _alarmTimeStamp = secFromHour + secFromMin;
            if (_alarmTimeStamp < _clock.CurrentTime)
                _alarmTimeStamp += 86400;



            EnableAlarm();

            _addAlarmWindow.SetActive(false);

        }
        private void EnableAlarm()
            => AlarmIsActiv = true;

        private void DisableAlarm()
            => AlarmIsActiv = false;
        private int GetSecondFromMinute(int minuteValue)
            => minuteValue * 60;

        private int GetSecondFromHours(int hourValue)
            => hourValue * 60 * 60;
    }


}
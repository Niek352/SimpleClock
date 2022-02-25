using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Clock
{
    public class RadialSelectorHandler : MonoBehaviour, IDragHandler, IDropHandler
    {
        [SerializeField] private Transform _centerRadialInputs;
        [SerializeField] private RadialInput _minuteInp;
        [SerializeField] private RadialInput _hourInp;

        
        private RadialInput _currentInput;
        [SerializeField] private RadialItem _currentItem;

        [SerializeField] private ClockAlarm _clockAlarm;

        private void Awake()
        {

            _minuteInp.Off();
            _currentInput = _hourInp;
        }
        private void Update()
        {

        }
        private void ChangeInput(int value)
        {
            if (_minuteInp.IsActive)
            {
                _minuteInp.Off();
                _clockAlarm.SetMinute(value);



                _hourInp.On();
                _currentInput = _hourInp;


            }
            else
            {
                _hourInp.Off();
                _clockAlarm.SetHour(value);



                _minuteInp.On();
                _currentInput = _minuteInp;

            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            _currentItem?.StopHighlight();


            _currentItem = _currentInput.Items.Closest(Input.mousePosition);
            _currentItem.Hightlight();

            if (_minuteInp.IsActive)
                _clockAlarm.SetMinute(_currentItem.Value);
            else
                _clockAlarm.SetHour(_currentItem.Value);
        }

        public void OnDrop(PointerEventData eventData)
        {
            _currentItem?.StopHighlight();



            ChangeInput(_currentItem.Value);
        }
    }
}
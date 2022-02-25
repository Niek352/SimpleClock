using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Clock
{
    public partial class RadialInput : MonoBehaviour
    {


        public float Seconds = 0.2f;
        [Header("Settings")]
        [SerializeField] private float _radius = 167f;
        [SerializeField] private float _minuteStep; 
        [SerializeField] private float _angle = 90;
        [SerializeField] int _iterations = 60;
        [SerializeField] private int _check = 15;


        public bool IsActive = false;

        [SerializeField] private RadialItem _prefab;
        public Transform Root;
        public List<RadialItem> Items = new List<RadialItem>();

        private void Awake()
        {
            _minuteStep = Mathf.Deg2Rad * _minuteStep;
            _angle = Mathf.Deg2Rad * 90;

            

            StartCoroutine(InstantiateButtons());
            
        }
        public void Off()
        {
            IsActive = false;
            Root.gameObject.SetActive(false);
        }
        public void On()
        {
            IsActive = true;
            Root.gameObject.SetActive(true);
        }


        private IEnumerator InstantiateButtons()
        {
            yield return null;
            
            for (int i = 0; i < _iterations; i++)
            {
                Vector3 vector3 = CalculatePosition();

                CreateItem(i, vector3);

                _angle += _minuteStep;

                yield return new WaitForSeconds(Seconds);
            }
        }

        private Vector3 CalculatePosition()
        {
            Vector3 vector3 = new Vector3(
                                Mathf.Cos(_angle) * -_radius, //x
                                Mathf.Sin(_angle) * _radius, //y
                                0);
            return vector3;
        }

        private void CreateItem(int i, Vector3 vector3)
        {
            var newItem = Instantiate(_prefab, Root, false);
            newItem.Value = i;

            newItem.transform.localPosition = vector3;
            if (i % _check == 0)
            {
                newItem.IsTextShowing = true;
                newItem.Text.enabled = true;
                newItem.Text.text = i.ToString();
                newItem.Text.margin = Vector4.zero;
                newItem.Image.transform.localScale = new Vector3(2.2f,2.2f,2.2f);
            }

            Items.Add(newItem); 
        }
        
    }
}
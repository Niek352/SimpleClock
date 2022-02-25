using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clock.Alarm.RadialInput
{
    public class RadialItem : MonoBehaviour
    {
        public TMP_Text Text;
        public Image Image;

        public int Value;
        public bool IsTextShowing;

        public void Hightlight()
        {
            Image.enabled = true;
            Text.enabled = true;


        }
        public void StopHighlight()
        {
            Image.enabled = false;
            if (IsTextShowing == false)
            {
                Text.enabled = false;
            }
        }
    }
}
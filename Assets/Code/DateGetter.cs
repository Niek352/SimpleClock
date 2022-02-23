using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml;

namespace Clock
{
    public class DateGetter : MonoBehaviour
    {
        public DateTime CurrentDateTime;
        public float CurrentTimestamp;
        public bool IsComplete = false;

        public string Data;
        private const string Url = "http://worldtimeapi.org/api/ip";
        
        [SerializeField] private Responce resp;

        public IEnumerator GetCurrentDateTime()
        {
            return GetTimeFromWorldApi();
        }

        private IEnumerator GetTimeFromWorldApi()
        {
            var www = UnityWebRequest.Get(Url);
            yield return www.SendWebRequest();


            //Если worldApi ошибка то Rapid Api
            if (www.responseCode == 200)
            {
                DateTime.TryParse(www.GetResponseHeaders()["date"], out CurrentDateTime);
                CurrentTimestamp = (float)CurrentDateTime.TimeOfDay.TotalSeconds;
                IsComplete = true;
            }
            else
            {
                
                GetMessage();
            }

        }
        public async void GetMessage()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://world-clock.p.rapidapi.com/json/utc/now"),
                Headers =
                {
                    { "x-rapidapi-host", "world-clock.p.rapidapi.com" },
                    { "x-rapidapi-key", "7a1b4cfd56mshe9cca46f61348c9p158df6jsn449e6b93edb7" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync();




                Data = body.Result;
                resp = (Responce)JsonUtility.FromJson(Data, typeof(Responce));
                CurrentDateTime = DateTime.FromFileTime(resp.currentFileTime);
                CurrentTimestamp = (float)CurrentDateTime.TimeOfDay.TotalSeconds;
                IsComplete = true;
            }
            //{"$id":"1","currentDateTime":"2022-02-23T18:13Z","utcOffset":"00:00:00","isDayLightSavingsTime":false,"dayOfTheWeek":"Wednesday","timeZoneName":"UTC","currentFileTime":132901136079667670,"ordinalDate":"2022-54","serviceResponse":null}
        }
        [Serializable]
        public class Responce
        {
            public string id;
            public string currentDateTime;
            public string utcOffset;
            public bool isDayLightSavingsTime;
            public string dayOfTheWeek;
            public string timeZoneName;
            public long currentFileTime;
            public string ordinalDate;
            public string serviceResponse;
        }
    }
}

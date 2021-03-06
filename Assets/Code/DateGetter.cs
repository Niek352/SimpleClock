using System;
using System.Collections;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;


namespace Clock
{
    [Serializable]
    public class DateGetter
    {
        public DateTime CurrentDateTime;
        public float CurrentTimestamp;
        public bool IsComplete = false;

        public string Data;
        private const string Url = "http://worldtimeapi.org/api/ip";

        private Responce resp;

        public IEnumerator GetCurrentDateTime()
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

                TryGetCurrentTimeFromRapidApi();
            }

        }


        private async void TryGetCurrentTimeFromRapidApi()
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

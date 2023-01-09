using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

using Microsoft.VisualBasic;

using Newtonsoft.Json.Linq;

using RestSharp;

namespace UntisLib
{
    public class UntisProgram
    {
        public static Dictionary<string, int> AmountHoursFromTeachers(int dateAsInt) {
            //contains teachers and their hours on the specific day
            var resultDictionary = new Dictionary<string, int>();
            //var dateAsInt = 20230109;

            #region First Request -- Login
            var clientLogin = new RestClient("https://arche.webuntis.com");
            var requestLogin = new RestRequest("/WebUntis/jsonrpc.do")
                .AddQueryParameter("school", "htbla-grieskirchen");

            requestLogin.AddHeader("Content-Type", "application/json");

            var body = @"{" + "\n" +
            @"""id"": ""5""," + "\n" +
            @"                ""method"": ""authenticate""," + "\n" +
            @"                ""params"":" + "\n" +
            @"                    {" + "\n" +
            @"                        ""user"": ""syp_5C_sus01""," + "\n" +
            @"                        ""password"": ""sue$_Bnws_?*2""," + "\n" +
            @"                        ""client"": ""CLIENT""" + "\n" +
            @"                    },       " + "\n" +
            @"                ""jsonrpc"": ""2.0""" + "\n" +
            @"}";
            requestLogin.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = clientLogin.Execute(requestLogin);
            #endregion

            //info contains sessionID
            LoginInfo info = JsonSerializer.Deserialize<LoginInfo>(response.Content);

            #region Second Request -- Get all classes and their ID´s
            var clientClasses = new RestClient("https://arche.webuntis.com");
            var requestClasses = new RestRequest("/WebUntis/jsonrpc.do")
                .AddQueryParameter("school", "htbla-grieskirchen");

            requestClasses.AddHeader("Content-Type", "application/json");

            requestClasses.AddHeader("Cookie", "JSESSIONID=" + info.result.sessionId + "; schoolname=\"_aHRibGEtZ3JpZXNraXJjaGVu\"; traceId=1ad4729a53e338a65998486943bd45ff41d9a5e8");
            var body2 = @"{" + "\n" +
            @"""id"": ""5""," + "\n" +
            @"                ""method"": ""getKlassen""," + "\n" +
            @"                ""params"":" + "\n" +
            @"                    {" + "\n" +
            @"                    },       " + "\n" +
            @"                ""jsonrpc"": ""2.0""" + "\n" +
            @"}";

            requestClasses.AddParameter("application/json", body2, ParameterType.RequestBody);
            RestResponse responseClasses = clientClasses.Execute(requestClasses);
            #endregion

            //clazzInfo contains all classes and their Id´s
            ClazzResult clazzInfo = JsonSerializer.Deserialize<ClazzResult>(responseClasses.Content);

            foreach (var item in clazzInfo.result) {
                //item.id is the id of a class
                var result = AmountHoursFromTeacherFromTimetableOfClass(dateAsInt, item.id);

                foreach (KeyValuePair<string, int> entry in result) {
                    if (resultDictionary.ContainsKey(entry.Key)) resultDictionary[entry.Key] += entry.Value;
                    else resultDictionary.Add(entry.Key, entry.Value);
                }
            }


            ////beautify date for printing of result
            //DateTime dt;
            //CultureInfo culture = new CultureInfo("pt-BR");
            //DateTime.TryParseExact(dateAsInt.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            ////prints result
            //Console.WriteLine($"Date: {dt.ToString("d", culture)}");
            //foreach (KeyValuePair<string, int> result in resultDictionary)
            //{
            //    Console.WriteLine($"{result.Key} : {result.Value} h");
            //}

            return resultDictionary;
        }

        public static Dictionary<string, int> AmountHoursFromTeacherFromTimetableOfClass(int date, int id) {
            //Key = teacherId, Value = teacherAbbreviation
            var teachers = new Dictionary<int, string>();


            // transform date for Request -- 20221214 to 2022-12-14
            var dateString = date.ToString().Insert(4, "-").Insert(7, "-");

            #region Third Request -- Get timeTable from clazz
            var clientTimetable = new RestClient("https://arche.webuntis.com");
            var requestTimetable = new RestRequest("/WebUntis/api/public/timetable/weekly/data");


            requestTimetable
                    .AddQueryParameter("elementType", "1")
                        .AddQueryParameter("elementId", $"{id}")
                        .AddQueryParameter("date", dateString);
            requestTimetable.AddHeader("Cookie", "schoolname=\"_aHRibGEtZ3JpZXNraXJjaGVu\"; JSESSIONID=9AEBD71834B2B9C9EE9DBD6A86F8BD64; schoolname=\"_aHRibGEtZ3JpZXNraXJjaGVu\"; traceId=fab402d1e060694dc1c2e32ae9c77b632afacdc8");

            RestResponse responseTimetable = clientTimetable.Execute(requestTimetable);
            //var content = responseTimetable.Content;
            #endregion

            // contains timetable of the whole school week 
            LessonInfo lessonInformation = JsonSerializer.Deserialize<LessonInfo>(responseTimetable.Content);

            foreach (var lesson in lessonInformation.data.result.data.elements) {

                if (lesson.type == 2) {
                    if (!teachers.ContainsKey(lesson.id)) {
                        teachers.Add(lesson.id, lesson.name);
                    }
                }
            }

            #region Filters amount of hours from teacher on specific date
            var result = new Dictionary<string, int>();
            foreach (var it in lessonInformation.data.result.data.elementPeriods.Values) {
                try {
                    for (int i = 0; i <= it.Length; i++) {
                        if (it[i].date == date) {
                            foreach (var element in it[i].elements) {
                                if (element.type == 2) {
                                    foreach (KeyValuePair<int, string> entry in teachers) {
                                        if (!result.ContainsKey(teachers[element.id])) {
                                            result.Add(teachers[element.id], 0);
                                        }
                                    }
                                    //int currentCount;

                                    result.TryGetValue(teachers[element.id], out int currentCount);
                                    result[teachers[element.id]] = currentCount + 1;
                                }
                            }
                        }
                    }
                }
                catch (IndexOutOfRangeException) {
                }
            }
            #endregion

            return result;
        }

    }
}

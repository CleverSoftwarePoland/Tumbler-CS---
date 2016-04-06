using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace Clever.Win.Tumblr.Services
{
    /// <summary>
    /// WebRequestWrapper uses HttpWebRequest to send POST, PUT or GET data requests 
    /// POST/PUT data uses JSON parameters wit UTF-8 characters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WebRequestWrapper
    {
        public static int MaxPossibleRetryOnRequestCanceled = 2;

        public static async Task<T> PostAsync<T>(string url, object payload)
        {
            int maxPossibleRetry = MaxPossibleRetryOnRequestCanceled;

            return await Task.Run(async () =>
            {
                do
                {
                    try
                    {
                        return await InternalPostAsync<T>(url, payload);
                    }
                    catch (WebException ex)
                    {
                        if (ex.Status == WebExceptionStatus.RequestCanceled)
                        {
                            maxPossibleRetry--;
                            //Log.WriteLine(string.Format("POST OPERATION RETRY ON REQUEST CANCELED:{0}", url));
                        }
                        else
                            throw;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                while (maxPossibleRetry > 0);

                return default(T);
            });

        }

        private static async Task<T> InternalPostAsync<T>(string url, object payload)
        {
            //Log.WriteLine(string.Format("POST:{0}", url));

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";

            var requestStream = await req.GetRequestStreamAsync();
            var bytes = Encoding.UTF8.GetBytes(payload.ToString());
            requestStream.Write(bytes, 0, bytes.Length);

            requestStream.Close();

            var response = await req.GetResponseAsync();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var responseString = reader.ReadToEnd();
                reader.Close();

                if (typeof(T) == typeof(String))
                    return (T)(object)responseString;

                return JsonConvert.DeserializeObject<T>(responseString);
            }
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            int maxPossibleRetry = MaxPossibleRetryOnRequestCanceled;

            return await Task.Run(async () =>
            {
                do
                {
                    try
                    {
                        return await InternalGetAsync<T>(url);
                    }
                    catch (WebException ex)
                    {
                        if (ex.Status == WebExceptionStatus.RequestCanceled)
                        {
                            maxPossibleRetry--;
                            //Log.WriteLine(string.Format("GET OPERATION RETRY ON REQUEST CANCELED:{0}", url));
                        }
                        else
                            throw;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                while (maxPossibleRetry > 0);

                return default(T);
            });
        }

        private static async Task<T> InternalGetAsync<T>(string url)
        {
            //Log.WriteLine(string.Format("GET:{0}", url));
            Debug.WriteLine(string.Format("GET:{0}", url));
            //Log.WriteLine(Thread.CurrentThread.ManagedThreadId);

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.AllowAutoRedirect = true;

            var response = await req.GetResponseAsync();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                if (typeof(T) == typeof(String))
                {
                    var responseString = sr.ReadToEnd();
                    sr.Close();

                    return (T)(object)responseString;
                }
                else
                {
                    var responseString = sr.ReadToEnd();
                    sr.Close();



                    return JsonConvert.DeserializeObject<T>(FixUglyJson(responseString));

                    //using (JsonReader reader = new JsonTextReader(sr))
                    //{
                    //    JsonSerializer serializer = new JsonSerializer();
                    //    return serializer.Deserialize<T>(reader);
                    //}
                }
            }
        }

        private static string FixUglyJson(string source)
        {
            source = source.Replace("var tumblr_api_read = ", "");
            source = source.Remove(source.Length - 1, 1);
            return source;
        }

    }
}
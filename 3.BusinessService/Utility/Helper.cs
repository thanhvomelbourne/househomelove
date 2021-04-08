using BusinessService.Models;
using CoreLibrary.Data;
using Database.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace BusinessService.Utility
{
    public static class Helper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string ClientIpAddress
        {
            get
            {
                try
                {
                    HttpRequest request = HttpContext.Current.Request;

                    string ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (!string.IsNullOrEmpty(ipAddress) && ipAddress.ToLower() != "unknown")
                    {
                        string[] addresses = ipAddress.Split(',');
                        if (addresses.Length != 0)
                            return addresses[0];
                    }

                    return request.ServerVariables["REMOTE_ADDR"];
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public static class AustraliaPostEngine
        {
            public static string RetrieveAustraliaPostStandardparcelSizes()
            {
                // Set the URL for the Domestic Parcel Size service
                string urlPrefix = "digitalapi.auspost.com.au";
                string parcelTypesUrl = "https://" + urlPrefix + "/postage/parcel/domestic/size.json";

                WebRequest httpWebRequest = WebRequest.Create(parcelTypesUrl);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers["AUTH-KEY"] = Constant.AustraliaPost.Token;

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string result = streamReader.ReadToEnd();
                    Console.WriteLine(result);

                    return result;
                }
            }

            public static string RetrieveListOfAvailableDomesticPostageServices(ShoppingCart cart, string toPostcode, double length, double width, double height, double weight)
            {
                string urlPrefix = "digitalapi.auspost.com.au";
                string parcelTypesUrl = "https://" + urlPrefix + "/postage/parcel/domestic/service.json?";

                List<KeyValuePair<string, string>> ausParameter = new List<KeyValuePair<string, string>>();

                ausParameter.Add(new KeyValuePair<string, string>("from_postcode", cart.WarehousePostcode.HasValue ? cart.WarehousePostcode.Value.ToString() : Constant.AustraliaPost.CurrentWarehousePostcode));
                ausParameter.Add(new KeyValuePair<string, string>("to_postcode", "3000"));
                ausParameter.Add(new KeyValuePair<string, string>("length", "22"));
                ausParameter.Add(new KeyValuePair<string, string>("width", "16"));
                ausParameter.Add(new KeyValuePair<string, string>("height", "7.7"));
                ausParameter.Add(new KeyValuePair<string, string>("weight", "1.5"));

                string query = ConvertToQueryString(ausParameter);

                WebRequest httpWebRequest = WebRequest.Create(parcelTypesUrl + query);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Headers["AUTH-KEY"] = Constant.AustraliaPost.Token;

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    string result = streamReader.ReadToEnd();
                    Console.WriteLine(result);

                    return result;
                }
            }

            public static string CalculateTotalDelivery(ShoppingCart cart, string toPostcode, double length, double width, double height, double weight)
            {
                try
                {
                    string urlPrefix = "digitalapi.auspost.com.au";
                    string parcelTypesUrl = "https://" + urlPrefix + "/postage/parcel/domestic/calculate.json?";

                    List<KeyValuePair<string, string>> ausParameter = new List<KeyValuePair<string, string>>();

                    ausParameter.Add(new KeyValuePair<string, string>("from_postcode", cart.WarehousePostcode.HasValue ? cart.WarehousePostcode.Value.ToString() : Constant.AustraliaPost.CurrentWarehousePostcode));
                    ausParameter.Add(new KeyValuePair<string, string>("to_postcode", toPostcode));
                    ausParameter.Add(new KeyValuePair<string, string>("length", length.ToString(CultureInfo.InvariantCulture)));
                    ausParameter.Add(new KeyValuePair<string, string>("width", width.ToString(CultureInfo.InvariantCulture)));
                    ausParameter.Add(new KeyValuePair<string, string>("height", height.ToString(CultureInfo.InvariantCulture)));
                    ausParameter.Add(new KeyValuePair<string, string>("weight", weight.ToString(CultureInfo.InvariantCulture)));
                    ausParameter.Add(new KeyValuePair<string, string>("service_code", "AUS_PARCEL_REGULAR"));

                    string query = ConvertToQueryString(ausParameter);

                    WebRequest httpWebRequest = WebRequest.Create(parcelTypesUrl + query);

                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "GET";
                    httpWebRequest.Headers["AUTH-KEY"] = Constant.AustraliaPost.Token;

                    HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream() ?? throw new InvalidOperationException()))
                    {
                        string result = streamReader.ReadToEnd();
                        Console.WriteLine(result);

                        return result;
                    }
                }
                catch (Exception e)
                {
                    log.Error($"{ Constant.AustraliaPost.CallErrorMessage} / {e.Message}");
                    log.Error($"{e}");
                    return Constant.AustraliaPost.CallErrorMessage;
                }
            }

            private static string ConvertToQueryString(List<KeyValuePair<string, string>> dict)
            {
                var list = new List<string>();
                foreach (var item in dict)
                {
                    list.Add(item.Key + "=" + item.Value);
                }
                return string.Join("&", list);
            }
        }

        public static string PostForm(string postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Constant.Path.ShareLink);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = bytes.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            var result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();

            return result;
        }

        public static SearchFileModel GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            List<String> result = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            long totalSize = 0;

            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }

            if(filesFound.Count > 0)
            {
                foreach(var f in filesFound)
                {
                    totalSize += new System.IO.FileInfo(f).Length;
                    string[] getname = f.Split('\\');

                    result.Add(getname[getname.Length - 1]);
                }
            }

            SearchFileModel model = new SearchFileModel();
            model.FileNames = result.ToArray();
            model.TotalFileSize = BytesToString(totalSize);
            return model;
        }

        private static string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public static string ConvertDateTimeString(DateTime datetime)
        {
            return TimeZoneInfo.ConvertTime(datetime, Constant.CommonValue.AustralianEasternTimeZoneInfo).ToString();
        }

        public static DateTime ConvertDateTime(DateTime datetime)
        {
            return TimeZoneInfo.ConvertTime(datetime, Constant.CommonValue.AustralianEasternTimeZoneInfo);
        }

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        public static string GetSystemInformation()
        {
            StringBuilder StringBuilder1 = new StringBuilder(string.Empty);
            try
            {
                long memKb;
                GetPhysicallyInstalledSystemMemory(out memKb);

                StringBuilder1.AppendFormat("Operation System:  {0} - ", Environment.OSVersion);
                if (Environment.Is64BitOperatingSystem)
                    StringBuilder1.AppendFormat("64 Bit OS<br/>");
                else
                    StringBuilder1.AppendFormat("32 Bit OS<br/>");
                StringBuilder1.AppendFormat("System Directory:  {0}<br/>", Environment.SystemDirectory);
                StringBuilder1.AppendFormat("Processor Count:  {0}<br/>", Environment.ProcessorCount);
                StringBuilder1.AppendFormat("RAM:  {0}<br/>", (memKb / 1024 / 1024) + " GB of RAM installed.");
                StringBuilder1.AppendFormat("User Domain Name:  {0}<br/>", Environment.UserDomainName);
                StringBuilder1.AppendFormat("Username: {0}<br/><br/>", Environment.UserName);
                //Drives
                foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
                {
                    try
                    {
                        StringBuilder1.AppendFormat("Logical Drives: {0}<br/>Volume Label: " +
                          "{1}<br/>Drive Type: {2}<br/>Drive Format: {3}<br/>" +
                          "Total Size: {4}<br/>Available Free Space: {5}<br/><br/>",
                          DriveInfo1.Name, DriveInfo1.VolumeLabel, DriveInfo1.DriveType,
                          DriveInfo1.DriveFormat, BytesToString(DriveInfo1.TotalSize), BytesToString(DriveInfo1.AvailableFreeSpace));
                    }
                    catch
                    {
                    }
                }
                StringBuilder1.AppendFormat("System Page Size:  {0}<br/>", Environment.SystemPageSize);
                StringBuilder1.AppendFormat("Version:  {0}", Environment.Version);
            }
            catch
            {
            }
            return StringBuilder1.ToString();
        }
    }
}

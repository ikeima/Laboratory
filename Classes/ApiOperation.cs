using Laboratory.DbModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;

namespace Laboratory.Classes
{
    class ApiOperation
    {
        public static void Post(string json, string controller)
        {
            var body = Encoding.UTF8.GetBytes(json);
            
            WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/" + controller);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
            }

            using (WebResponse response = request.GetResponse()) { }
        }
        public static void Put(string json, string controller, string id)
        {
            var body = Encoding.UTF8.GetBytes(json);

            WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/" + controller + "/" + id);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.ContentLength = body.Length;

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
            }
            using (WebResponse response = request.GetResponse()) { }
               
        }
        public static void Delete(string controller, string id)
        {
            WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/" + controller + "/" + id);
            request.Method = "DELETE";

            using (WebResponse response = request.GetResponse()) { }
        }

        #region Get Methods
        public static List<Users> GetUsers()
        {
            try
            {
                WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/users");
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream()) // Получение ответа 
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return JsonConvert.DeserializeObject<List<Users>>(reader.ReadLine());
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Подключитесь к сети!");
                return null;
            }
        }
        public static List<History> GetHistory()
        {
            WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/history"); 
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream()) 
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<List<History>>(reader.ReadLine()); 
                    }
                }
            }
        }
        public static List<Services> GetServices()
        {
            WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/services"); // Создание запроса к API с указанием адреса сайта
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream()) // Получение ответа 
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<List<Services>>(reader.ReadLine()); // Считывание и десериализация полученных объектов
                    }
                }
            }
        }
        public static List<Patients> GetPatients()
        {
            WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/patients");
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<List<Patients>>(reader.ReadLine());
                    }
                }
            }
        }
        public static Patients GetPatient(string id)
        {
            WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/patients/" + id);
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<Patients>(reader.ReadLine());
                    }
                }
            }
        }
        public static List<Orders> GetOrders()
        {
            WebRequest request = WebRequest.Create(@"http://ikeima.somee.com/api/orders/");
            request.Timeout = 10000;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<List<Orders>>(reader.ReadToEnd());
                    }
                }
            }
        }
        public static List<Insurance_companies> GetCompanies()
        {
            WebRequest request = WebRequest.Create("http://ikeima.somee.com/api/insuranceCompanies");

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<List<Insurance_companies>>(reader.ReadToEnd());
                    }
                }
            }
        }
        public static List<Accounts> GetAccounts()
        {
            WebRequest request = WebRequest.Create("http://ikeima.somee.com/api/accounts");
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<List<Accounts>>(reader.ReadToEnd());
                    }
                }
            }
        }
        public static List<Patients_services> GetPatientsServices()
        {
            WebRequest request = WebRequest.Create("http://ikeima.somee.com/api/patientsServices");
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<List<Patients_services>>(reader.ReadToEnd());
                    }
                }
            }
        }
        public static List<Accountants> GetAccountants()
        {
            WebRequest request = WebRequest.Create("http://ikeima.somee.com/api/accountants");
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return JsonConvert.DeserializeObject<List<Accountants>>(reader.ReadToEnd());
                    }
                }
            }   
        }
        #endregion
    }
}

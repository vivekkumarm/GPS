using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using JeevanGPS.BO;
using Newtonsoft.Json.Linq;


namespace JeevanGPS.Web.Controllers
{
    public class PersonController : Controller
    {
        //
        // GET: /Person/

        public ActionResult Save()
        {        
         
         
            return View();
        }


        [HttpPost]
        public JsonResult Getall()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:61630/");


            //// Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            // List all products.
            HttpResponseMessage response = client.GetAsync("api/Person").Result;  // Blocking call!
            var products = response.Content.ReadAsAsync<IEnumerable<Person>>().Result;

            return Json(products);
        }


        [HttpPost]
        public ActionResult Save(Person person)         
        {
            
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:61630/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));       
                      
            HttpResponseMessage response = client.PostAsJsonAsync("api/Person", person).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Person Successfully Created.";
               
                ModelState.Clear();    
            }
            else
            {
                var error = (System.Web.Http.HttpError)response.Content.ReadAsAsync(typeof(System.Web.Http.HttpError)).Result;         

                object serialized;
                if (error.TryGetValue("ModelState", out serialized))
                {
                    var modelState = new ModelStateDictionary();

                    if (serialized is JObject)
                    {
                        GetModelStateFromJObject((JObject)serialized);
                    }
                }
            } 
            return View(); 
        }




        [HttpPost]
        public JsonResult SaveJSON(Person person)
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:61630/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync("api/Person", person).Result;

         
            if (response.IsSuccessStatusCode)
            {
                //TempData["Success"] = "Person Successfully Created.";
                //ModelState.Clear();
            }
            else
            {
                var error = (System.Web.Http.HttpError)response.Content.ReadAsAsync(typeof(System.Web.Http.HttpError)).Result;

                object serialized;
                if (error.TryGetValue("ModelState", out serialized))
                {
                    var modelState = new ModelStateDictionary();

                    if (serialized is JObject)
                    {
                        GetModelStateFromJObject((JObject)serialized);
                    }
                }

                var errorslist = new Hashtable();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errorslist[pair.Key] = pair.Value.Errors.Select(e => e.ErrorMessage).ToList();
                    }
                }

                return Json(new { success = false, errorslist }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
      
        }


        private void GetModelStateFromJObject(JObject container)
        {
            foreach (var error in container)
            {
                foreach (var message in error.Value.Values<string>())
                {
                    ModelState.AddModelError(error.Key, message);                  
                }
            }
        }

    }
}

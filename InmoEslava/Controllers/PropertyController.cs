using InmoEslava.Models;
using InmoEslava.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Text;

namespace InmoEslava.Controllers
{
    public class PropertyController : Controller
    {
        // GET: Property
        public async Task<ActionResult> PropertyView()
        {
            PropertyDetailData propertyData = null;
            string rootFolder = WebConfigurationManager.AppSettings["RootImageFolder"];
            string imageFolder = WebConfigurationManager.AppSettings["ImageFolder"];
            ViewBag.ApiUrl = WebConfigurationManager.AppSettings["ApiUrl"];

            PropertyViewModel propertyViewModel = new PropertyViewModel();
            int propertyId = Convert.ToInt32(Request.QueryString["Id"]);
            string token = Request.QueryString["token"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage httpResponse = await client.GetAsync("api/Property/GetPropertydetail/" + propertyId);
                string strResult = await httpResponse.Content.ReadAsStringAsync();
                string[] images;
                propertyData = Newtonsoft.Json.JsonConvert.DeserializeObject<PropertyDetailData>(strResult);
                if (httpResponse.IsSuccessStatusCode)
                {
                    if (Directory.Exists(rootFolder))
                    {
                        if (Directory.Exists(rootFolder + "\\" + propertyData.Property.ImagesPath))
                        {
                            images = Directory.GetFiles(rootFolder + "\\" + propertyData.Property.ImagesPath);
                            for (int i = 0; i < images.Length; i++)
                            {
                                images[i] = images[i].Substring(images[i].IndexOf("Uploads"));
                            }
                            propertyViewModel.Images = images;
                        }
                    }
                    propertyViewModel.Characterstic = propertyData.CharactersticData;
                    propertyViewModel.Location = propertyData.LocationData;
                    propertyViewModel.Material = propertyData.MaterialData;
                    propertyViewModel.Rooms = propertyData.RoomsData;
                    propertyViewModel.Toilet = propertyData.ToiletData;
                    propertyViewModel.NearTo = propertyData.NearToData;
                    propertyViewModel.Property = propertyData.Property;
                }

            }
            return View(propertyViewModel);
        }

        public ActionResult EditProperty()
        {
            return View();
        }

        public ActionResult Property()
        {
            return RedirectToAction("Property", "Home");
            // return View();
        }

        public ActionResult GetUploadedImages()
        {
            string folderName = Request.QueryString["Id"];
            string[] images = null;
            try
            {
                
                string rootFolder = WebConfigurationManager.AppSettings["RootImageFolder"];
                if (Directory.Exists(rootFolder))
                {
                    if (Directory.Exists(rootFolder + "\\" + folderName))
                    {
                        images = Directory.GetFiles(rootFolder + "\\" + folderName);
                        for (int i = 0; i < images.Length; i++)
                        {
                            string imageFullPath =  images[i].Substring(images[i].IndexOf("Uploads"));
                            images[i] = Path.GetFileName(imageFullPath);
                        }
                        
                    }
                }
                return Json(images);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult UploadFiles(string folderName)
        {
            try
            {
                string rootFolder = WebConfigurationManager.AppSettings["RootImageFolder"];
                string uname = Request["uploadername"];
                HttpFileCollectionBase files = Request.Files;

                if (files !=null && files.Count > 0)
                {
                    if (folderName == null)
                        folderName = Guid.NewGuid().ToString();
                    if (!Directory.Exists(rootFolder))
                    {
                        Directory.CreateDirectory(rootFolder);
                    }
                    if (!Directory.Exists(rootFolder + "\\" + folderName))
                    {
                        Directory.CreateDirectory(rootFolder + "\\" + folderName);
                    }
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        // Checking for Internet Explorer      
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        // Get the complete folder path and store the file inside it.      
                        //fname = Path.Combine(Server.MapPath("~/Uploads/" + folderName), fname);
                        fname = Path.Combine(rootFolder + "\\" + folderName, fname);

                        file.SaveAs(fname); 
                    }
                    return Json(folderName);
                }
                else
                {
                    return Json(true);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using TicketShopAdminApp.Models;

namespace TicketShopAdminApp.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(){
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
         }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44331/api/admin/GetAllActiveOrders";
            HttpResponseMessage msg=client.GetAsync(url).Result;
            var data = msg.Content.ReadAsAsync<List<Order>>().Result;
            return View(data);
        }
        public IActionResult Details(Guid id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44331/api/admin/GetDetailsForOrders";
            var model = new 
            {
                Id =id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model),Encoding.UTF8,"application/json");
            HttpResponseMessage msg = client.PostAsync(url,content).Result;
           
            var data = msg.Content.ReadAsAsync<Order>().Result;
            return View(data);
            
        }

        public FileContentResult CreateInvoice(Guid id)
        {
            HttpClient client = new HttpClient();
            string url = "https://localhost:44331/api/admin/GetDetailsForOrders";
            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage msg = client.PostAsync(url, content).Result;

            var data = msg.Content.ReadAsAsync<Order>().Result;
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "invoice");
            var pdfdocument = DocumentModel.Load(templatePath);
            pdfdocument.Content.Replace("{{OrderNumber}}", data.Id.ToString());
            pdfdocument.Content.Replace("{{UserName}}", data.User.UserName.ToString());

            StringBuilder sb = new StringBuilder();
            
            foreach (var item in data.Tickets)
            {
                
                sb.AppendLine(item.Ticket.MovieName+" "+item.Ticket.TicketPrice+ "den.");
            }
            pdfdocument.Content.Replace("{{TicketList}}", sb.ToString());
            // pdfdocument.Content.Replace("{{TotalPrice}}", data.id.ToString());

            var stream = new MemoryStream();
            pdfdocument.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(),new PdfSaveOptions().ContentType,"ExportInvoice.pdf");

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using MusicWebsite.Models;

namespace MusicWebsite.Helpers
{
    public class Emailhelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task EmailtoAdmin(Order order)
        {

            try
            {
                EmailService ems = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                msg.Body = $"A new order number {order.Id} has been placed on {order.PlacedOn} for {order.GrandTotal}";

                msg.Destination = "sharma.sahilsharma207@gmail.com";

                msg.Subject = "a new order has been placed on your music website";
                

                var fromemail = order.Email;
                await ems.SendMailAsync(msg, fromemail);
            }
            catch (Exception ex)
            {
                await Task.FromResult(0);
            }
        }
        public async Task EmailtoBuyer(Order order)
        {

            try
            {
                EmailService ems = new EmailService();
                IdentityMessage msg = new IdentityMessage();
                msg.Body = $"Your order is confirmed with order number {order.Id} placed on {order.PlacedOn}. You paid $ {order.GrandTotal}.";

                msg.Destination = order.Email;

                msg.Subject = "Order confirmation from music website";


                var fromemail = order.Email;
                await ems.SendMailAsync(msg, fromemail);
            }
            catch (Exception ex)
            {
                await Task.FromResult(0);
            }
        }
    }
}
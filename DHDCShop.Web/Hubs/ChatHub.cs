using AutoMapper;
using DHDCShop.Models;
using DHDCShop.Models.Model;
using DHDCShop.Models.ViewModel;
using DHDCShop.Web.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DHDCShop.Web.Hubs
{
    public class ChatHub : Hub
    {
        public readonly static List<ChatViewModel> _Users = new List<ChatViewModel>();

        public readonly static List<ChatViewModel> _Connections = new List<ChatViewModel>();

        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

        #region OnConnect/OnDisconnect
        public override Task OnConnected()
        {
            using (var db = new DHDCShopDbContext())
            {
                // First run?
                if (_Users.Count == 0)
                {
                    foreach (var user in db.Customers.ToList())
                    {
                        ChatViewModel userViewModel = Mapper.Map<Customer, ChatViewModel>(user);
                        _Users.Add(userViewModel);
                    }
                }
            }
            using (var db = new DHDCShopDbContext())
            {
                var user = db.Customers.Find(IdentityName);
                if (user != null)
                {
                    var userVM = Mapper.Map<Customer, ChatViewModel>(user);
                    if (!_Connections.Contains(userVM))
                    {
                        _Connections.Add(userVM);
                    }
                    Clients.All.UpdatePin(userVM);
                }
            }
            var userRole = Context.User.IsInRole("admin");
            if (userRole)
            {
                Groups.Add(Context.ConnectionId, "admin");
            }
            else
            {
                Groups.Add(Context.ConnectionId, "user");
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                var tempUser = _Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                if (tempUser != null)
                {
                    Clients.All.UpdatePin(tempUser);
                    var user = _Connections.Where(u => u.UserName == IdentityName).FirstOrDefault();
                    if (user != null)
                    {
                        _Connections.Remove(user);

                    }
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.onError("OnDisconnected: " + ex.Message);
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            //var tempUser = _Users.Where(u => u.Username == IdentityName).FirstOrDefault();
            //_Users.Remove(tempUser);


            //_Users.Add(user);
            return base.OnReconnected();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public int SendToAdmin(string message, string customerId)
        {
            try
            {
                using (var db = new DHDCShopDbContext())
                {
                    var userSender = db.Customers.Where(u => u.Username == customerId).FirstOrDefault();


                    // Create and save message in database
                    Message msg = new Message()
                    {
                        Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
                        Timestamp = DateTime.Now.Ticks.ToString(),
                        FromUser = userSender,
                        Type = 1,
                    };
                    db.Messages.Add(msg);
                    db.SaveChanges();
                    int idMess = msg.Id;
                   
                    var messageViewModel = Mapper.Map<Message, MessageViewModel>(msg);
                    try
                    {
                        Clients.OthersInGroup("admin").getNewMessage(messageViewModel);
                        Clients.Caller.pushNewMessage(messageViewModel);

                    }
                    catch (Exception)
                    {
                        Clients.Caller.pushNewMessage(messageViewModel);
                    }

                    return idMess;
                }
            }
            catch (Exception)
            {
                Clients.Caller.onError("Message not send!");
            }
            return 0;
        }

        public int ReplayToCustomer(string message, string customerId)
        {
            try
            {
                using (var db = new DHDCShopDbContext())
                {
                    var userSender = db.Customers.Where(u => u.Username == customerId).FirstOrDefault();


                    // Create and save message in database
                    Message msg = new Message()
                    {
                        Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", String.Empty),
                        Timestamp = DateTime.Now.Ticks.ToString(),
                        FromUser = userSender,
                        Type = 2,
                    };
                    db.Messages.Add(msg);
                    db.SaveChanges();
                    int idMess = msg.Id;
                    var messageViewModel = Mapper.Map<Message, MessageViewModel>(msg);
                    try
                    {
                        if (_Connections.Where(x => x.UserName == customerId).FirstOrDefault() != null)
                        {

                            // Send the message
                            Clients.User(customerId).getNewMessage(messageViewModel);
                            Clients.Caller.pushNewMessage(messageViewModel);
                        }
                        else
                        {
                            Clients.Caller.pushNewMessage(messageViewModel);
                        }
                    }
                    catch (Exception)
                    {
                        Clients.Caller.pushNewMessage(messageViewModel);
                    }

                    return idMess;
                }
            }
            catch (Exception)
            {
                Clients.Caller.onError("Message not send!");
            }
            return 0;
        }

        public IEnumerable<MessageViewModel> GetMessageHistory(string customerId)
        {
            using (var db = new DHDCShopDbContext())
            {
                if (customerId != "")
                {
                    var messageHistory = db.Messages.Where(m => (m.FromUserId == customerId))
                    .OrderByDescending(m => m.Timestamp)
                    .Take(20)
                    .AsEnumerable()
                    .Reverse()
                    .ToList();
                    return Mapper.Map<IEnumerable<Message>, IEnumerable<MessageViewModel>>(messageHistory);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public IEnumerable<ChatViewModel> GetListChatHistory()
        {
            using (var db = new DHDCShopDbContext())
            {
                var list = db.Messages.OrderByDescending(x => x.Timestamp)
                             .Select(x => x.FromUser)
                             .Distinct()
                             .ToList();
                if (list != null)
                    return Mapper.Map<IEnumerable<Customer>, IEnumerable<ChatViewModel>>(list);
                else 
                    return null;

            }
            return null;
        }
    }
}
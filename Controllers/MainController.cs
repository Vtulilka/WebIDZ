using SubscriberBase.Entities;
using SubscriberBase.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SubscriberBase.Controllers
{
    public class MainController : Controller
    {
        [AllowAnonymous]
        public ActionResult ListOfOperators()
        {
            List<Operators> operators = new List<Operators>();
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                operators = db.Operators.OrderByDescending(x => x.OperatorName).ToList();
            }
            return View(operators);
        }

        public ActionResult ListOfSubscribers()
        {
            List<Subscribers> people = new List<Subscribers>();
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                people = db.Subscribers.OrderByDescending(x => x.Surname).ThenBy(x => x.Name).ThenBy(x => x.Patronymic).ToList();
            }
            return View(people);
        }

        [AllowAnonymous]
        public ActionResult ListOfTariffs(Guid ID)
        {
            List<Tariffs> tariffs = new List<Tariffs>();
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                Tariffs model = new Tariffs();
                var op = db.Operators.FirstOrDefault(o => o.OperatorID == ID);
                if (op != null)
                {
                    foreach (var tariff in op.Tariffs)
                    {
                        tariffs.Add(tariff);
                    }
                    return View(tariffs);
                }
                return HttpNotFound();
            }
        }

        List<Tuple<string, string>> GetOperatorsList()
        {
            List<Tuple<string, string>> OperatorsList = new List<Tuple<string, string>>();
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                foreach (Operators op in db.Operators)
                {
                    Tuple<string, string> operatorTuple = Tuple.Create(op.OperatorID.ToString(), op.OperatorName);
                    OperatorsList.Add(operatorTuple);
                }
            }
            return OperatorsList;
        }

        List<Tuple<string, string>> GetTariffsList()
        {
            List<Tuple<string, string>> TariffsList = new List<Tuple<string, string>>();
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                foreach (Tariffs tariffs in db.Tariffs)
                {
                    Tuple<string, string> tariffTuple = Tuple.Create(tariffs.TariffID.ToString(), tariffs.TariffName);
                    TariffsList.Add(tariffTuple);
                }
            }
            return TariffsList;
        }
        List<Tuple<string, string>> GetSubscribersList()
        {
            List<Tuple<string, string>> SubscribersList = new List<Tuple<string, string>>();
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                foreach (Subscribers people in db.Subscribers)
                {
                    Tuple<string, string> subscriberTuple = Tuple.Create(people.SubscriberID.ToString(), people.Surname + " " + people.Name + " " + people.Patronymic);
                    SubscribersList.Add(subscriberTuple);
                }
            }
            return SubscribersList;
        }

        [HttpGet]
        public ActionResult OperatorDetails(Guid ID)
        {
            Operators model = new Operators();
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                model = db.Operators.Find(ID);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult TariffDetails(Guid ID)
        {
            Tariffs model = new Tariffs();
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                model = db.Tariffs.Find(ID);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateConnection()
        {
            ViewBag.OperatorsList = new SelectList(GetOperatorsList(), "Item1", "Item2");
            ViewBag.TariffsList = new SelectList(GetTariffsList(), "Item1", "Item2");
            ViewBag.SubscribersList = new SelectList(GetSubscribersList(), "Item1", "Item2");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult CreateConnection(ConnectionVM newConnect)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Chizhik_IDZ_WebEntities())
                {
                    Connection connect = new Connection
                    {
                        ConnectionID = Guid.NewGuid(),
                        OperatorID = newConnect.OperatorID,
                        TariffID = newConnect.TariffID,
                        ConnectionDate = newConnect.ConnectionDate,
                        PhoneNumb = newConnect.PhoneNumb,
                        SubscriberID = newConnect.SubscriberID,
                    };
                    db.Connection.Add(connect);
                    db.SaveChanges();
                }
                return RedirectToAction("ListOfOperators");
            }
            ViewBag.OperatorsList = new SelectList(GetOperatorsList(), "Item1", "Item2");
            ViewBag.TariffsList = new SelectList(GetTariffsList(), "Item1", "Item2");
            ViewBag.SubscribersList = new SelectList(GetSubscribersList(), "Item1", "Item2");
            return View(newConnect);
        }

        [HttpGet]
        public ActionResult CreatePerson()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult CreatePerson(SubscribersVM newPerson)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Chizhik_IDZ_WebEntities())
                {
                    Subscribers person = new Subscribers
                    {
                        SubscriberID = Guid.NewGuid(),
                        Surname = newPerson.Surname,
                        Name = newPerson.Name,
                        Patronymic = newPerson.Patronymic,
                        BirthDay = newPerson.BirthDay,
                    };
                    db.Subscribers.Add(person);
                    db.SaveChanges();
                }
                return RedirectToAction("ListOfOperators");
            }
            return View(newPerson);
        }

        [HttpGet]
        public ActionResult CreatePerConnect()
        {
            ViewBag.OperatorsList = new SelectList(GetOperatorsList(), "Item1", "Item2");
            ViewBag.TariffsList = new SelectList(GetTariffsList(), "Item1", "Item2");
            return View();
        }

        [HttpPost]
        public ActionResult CreatePerConnect(PerConnectVM VM)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Chizhik_IDZ_WebEntities())
                {
                    Subscribers person = new Subscribers
                    {
                        SubscriberID = Guid.NewGuid(),
                        Surname = VM.Surname,
                        Name = VM.Name,
                        Patronymic = VM.Patronymic,
                        BirthDay = VM.BirthDay,
                    };
                    Connection connect = new Connection
                    {
                        ConnectionID = Guid.NewGuid(),
                        OperatorID = VM.OperatorID,
                        TariffID = VM.TariffID,
                        ConnectionDate = VM.ConnectionDate,
                        PhoneNumb = VM.PhoneNumb,
                        SubscriberID = person.SubscriberID,
                    };
                    db.Subscribers.Add(person);
                    db.Connection.Add(connect);
                    db.SaveChanges();
                }
                return RedirectToAction("ListOfOperators");
            }

            ViewBag.OperatorsList = new SelectList(GetOperatorsList(), "Item1", "Item2");
            ViewBag.TariffsList = new SelectList(GetTariffsList(), "Item1", "Item2");
            ViewBag.SubscribersList = new SelectList(GetSubscribersList(), "Item1", "Item2");
            return View(VM);
        }

        [HttpGet]
        public ActionResult EditPerson(Guid ID)
        {
            SubscriberPhone model;
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                Subscribers person = db.Subscribers.Find(ID);
                Connection number = db.Connection.SingleOrDefault(x => x.SubscriberID == ID);
                model = new SubscriberPhone
                {
                    SubscriberID = person.SubscriberID,
                    Surname = person.Surname,
                    Name = person.Name,
                    Patronymic = person.Patronymic,
                    BirthDay = person.BirthDay,
                    PhoneNumb = number.PhoneNumb,
                };
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditPerson(SubscriberPhone model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Chizhik_IDZ_WebEntities())
                {
                    Subscribers editedPerson = new Subscribers
                    {
                        SubscriberID = model.SubscriberID,
                        Surname = model.Surname,
                        Name = model.Name,
                        Patronymic = model.Patronymic,
                        BirthDay = model.BirthDay,
                    };
                    Connection editedNumb = new Connection
                    {
                        SubscriberID = model.SubscriberID,
                        PhoneNumb = model.PhoneNumb,
                    };
                    db.Subscribers.Attach(editedPerson);
                    db.Entry(editedPerson).State = System.Data.Entity.EntityState.Modified;

                    var existingConnection = db.Connection.FirstOrDefault(c => c.SubscriberID == model.SubscriberID);
                    if (existingConnection != null)
                    {
                        existingConnection.PhoneNumb = model.PhoneNumb;
                        db.Entry(existingConnection).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Connection.Add(editedNumb);
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("ListOfSubscribers");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeletePerson(Guid ID)
        {
            Subscribers DelPerson;
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                DelPerson = db.Subscribers.Find(ID);
            }
            return View(DelPerson);
        }

        [HttpPost, ActionName("DeletePerson")]
        public ActionResult DeleteConfirmed(Guid ID)
        {
            using (var db = new Chizhik_IDZ_WebEntities())
            {
                Connection delConnection = db.Connection.FirstOrDefault(x => x.SubscriberID == ID);
                if (delConnection != null)
                {
                    db.Connection.Remove(delConnection);
                }
                db.Subscribers.Remove(db.Subscribers.Find(ID));
                db.SaveChanges();
            }
            return RedirectToAction("ListOfSubscribers");
        }

    }
}
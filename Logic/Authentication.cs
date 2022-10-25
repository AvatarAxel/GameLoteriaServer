using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Authentication
    {
        public bool IsAuthenticated(string username, string password)
        {
            Boolean status = false;
            using (var context = new GameLoteriaDataBasesEntities())
            {
                var players = (from Player in context.player where Player.username == username && Player.password == password select Player).Count();
                status = players > 0;
            }
            return status;
        }

        public bool ReponseAuthenticated (string username, string email, string password01)
        {
            bool status = false;
            using(var context = new GameLoteriaDataBasesEntities())
            {
                var newPlayerDB = context.player.Add(new player() { email= email, username = username, password = password01 });
                var resultado = context.SaveChanges();
                if (resultado > 0)
                {
                    status = true;
                }
            }
            return status;
        }
    }
}

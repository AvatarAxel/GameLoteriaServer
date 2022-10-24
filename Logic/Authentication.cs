using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Authentication
    {
        public bool IsAuthenticated(String username, String password)
        {
            Boolean status = false;
            using (var context = new GameLoteriaDataBasesEntities())
            {
                var players = (from Player in context.player where Player.username == username && Player.password == password select Player).Count();
                if (players > 0)
                {
                    status = true;
                }
            }
            return status;
        }
    }
}

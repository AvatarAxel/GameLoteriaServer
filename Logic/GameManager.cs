using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GameManager
    {
        public bool Betting(string username, int bet)
        {
            ValidateData validate = new ValidateData();
            if (validate.ValidationUsernameFormat(username))
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var player = context.player.Where(x => x.username == username).FirstOrDefault();
                    if (player != null)
                    {
                        if (player.coins >= bet)
                        {
                            player.coins = player.coins - bet;
                            if (context.SaveChanges() > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public bool ReceiveCoinsEarned(string username, int totalCoins)
        {
            ValidateData validate = new ValidateData();
            if (validate.ValidationUsernameFormat(username))
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var player = context.player.Where(x => x.username == username).FirstOrDefault();
                    if (player != null)
                    {
                        player.coins = player.coins + totalCoins;
                        if (context.SaveChanges() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public int GetCoins(string username)
        {
            ValidateData validate = new ValidateData();
            if (validate.ValidationUsernameFormat(username))
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var player = context.player.Where(x => x.username == username).FirstOrDefault();
                    if (player != null)
                    {
                        return player.coins;
                    }
                }
            }
            return 0;
        }
    }
}

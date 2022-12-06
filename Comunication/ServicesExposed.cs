using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Logic;
using System.Threading;
using System.Text.RegularExpressions;
using System.ServiceModel.Channels;

namespace Comunication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public partial class ServicesExposed : IAuthenticationService
    {
        public PlayerDTO AuthenticationLogin(string name, string password)
        {
            UserManager authentication = new UserManager();
            PlayerDTO playerDTO = authentication.AuthenticationLogin(name, password);
            return playerDTO;
        }
    }


    public partial class ServicesExposed : IChatService
    {
        List<GameRoundDTO> lobbyChat = new List<GameRoundDTO>();
        public void CreateChat(string verificationCode)
        {
            List<PlayerDTO> playerDTOs = new List<PlayerDTO>();
            GameRoundDTO gameRoundDTO = new GameRoundDTO()
            {
                VerificationCode = verificationCode,
                PlayerDTOs = playerDTOs

            };
            lobbyChat.Add(gameRoundDTO);
        }

        public void JoinChat(string username, string codeVerification)
        {
            var ChatNew = lobbyChat.FirstOrDefault(iteration => iteration.VerificationCode == codeVerification);
            if (ChatNew != null)
            {
                PlayerDTO players = new PlayerDTO()
                {
                    Username = username,
                };
                players.Connection = OperationContext.Current;
                ChatNew.PlayerDTOs.Add(players);
                SendMessage("Join chat", username, codeVerification);

            }
        }

        public void SendMessage(string message, string userChat, string codeVerification)
        {
            var ChatExisting = lobbyChat.FirstOrDefault(iteration => iteration.VerificationCode == codeVerification);
            if (ChatExisting != null)
            {
                Task task = new Task(() => {
                    for (int i = 0; i < ChatExisting.PlayerDTOs.Count; i++)
                    {
                        try
                        {
                            ChatExisting.PlayerDTOs[i].Connection.GetCallbackChannel<IChatServiceCallBack>().ReciveMessage(userChat, message);
                        }
                        catch (CommunicationObjectAbortedException)
                        {
                            ChatExisting.PlayerDTOs.Remove(ChatExisting.PlayerDTOs[i]);
                        }
                    }
                });
                task.Start();
            }
        } 
        public void ExitChat(string userName, string codeVerification)
        {
            var ChatExisting = lobbyChat.FirstOrDefault(iteration => iteration.VerificationCode == codeVerification);
            if (ChatExisting != null)
            {
                var player = ChatExisting.PlayerDTOs.FirstOrDefault(iteration => iteration.Username == userName);
                if (player != null)
                {
                    player.Connection.GetCallbackChannel<IChatServiceCallBack>().ReciveMessage(player.Username, "Exit Chat");
                    ChatExisting.PlayerDTOs.Remove(player);
                    SendMessage("Exited the chat", userName, codeVerification);
                }
            }
        }
        public void DeleteChat(string verificationCode)
        {
            var ChatExisting = lobbyChat.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (ChatExisting != null)
            {
                lobbyChat.Remove(ChatExisting);
            }
        }
    }
    public partial class ServicesExposed : IChangePasswordService
    {
        public bool ChangePassword(string email, string password)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ChangePassword(email, password);
            return status;
        }
    }
    public partial class ServicesExposed : IEmailService
    {
        public bool ValidationEmail(string email, string codeVerification)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ReceiveEmail(email, codeVerification);
            return status;
        }
    }
    public partial class ServicesExposed : IUserRegistrationService
    {
        public bool RegistrerUserDataBase(PlayerDTO player)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.RegisterUser(player);
            return status;
        }

        public bool ValidationEmailDataBase(string email)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ValidationEmail(email);
            return status;
        }

        public bool ValidationUsernameDataBase(string username)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ValidationUsername(username);
            return status;
        }
    }
    public partial class ServicesExposed : IGameService
    {
        List<GameRoundDTO> lobbyList = new List<GameRoundDTO>();
        public void CreateGame(GameRoundDTO game)
        {
            List<PlayerDTO> ListPlayerDTOs = new List<PlayerDTO>();
            game.PlayerDTOs = ListPlayerDTOs;
            lobbyList.Add(game);
        }

        public void EliminateGame(string verificationCode)
        {
            var game = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                lobbyList.Remove(game);
            }
        }

        public void ExitGame(string userName, string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                var player = lobby.PlayerDTOs.FirstOrDefault(iteration => iteration.Username == userName);
                if (player != null)
                {
                    lobby.PlayerDTOs.Remove(player);
                }
            }
        }

        public void JoinGame(string username, string verificationCode)
        {
            var newConnection = OperationContext.Current;
            var game = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                PlayerDTO player = new PlayerDTO()
                {
                    Username = username,
                };
                player.Connection = newConnection;
                game.PlayerDTOs.Add(player);
                newConnection.GetCallbackChannel<IGameServiceCallBack>().ResponseTotalPlayers(game.PlayerDTOs.Count);
            }
        }

        public void SendNextHostGame(string verificationCode)
        {
            var game = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                foreach (PlayerDTO user in game.PlayerDTOs)
                {
                    if (!Regex.IsMatch(user.Username, "Invitado") || !Regex.IsMatch(user.Username, "Guest"))
                    {
                        user.Connection.GetCallbackChannel<IGameServiceCallBack>().SendNextHostGameResponse(true);
                        return;
                    }
                }
                EliminateGame(verificationCode);
                DeleteChat(verificationCode);
            }
        }

        public void GoToGame(string verificationCode)
        {
            var game = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                for (int i = 0; i < game.PlayerDTOs.Count; i++)
                {
                    game.PlayerDTOs[i].Connection.GetCallbackChannel<IGameServiceCallBack>().GoToPlay(true);
                }
            }
        }

    }
    public partial class ServicesExposed : IJoinGameService
    {
        public bool ResponseCodeExist(string verificationCode)
        {
            var game = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                return true;
            }
            return false;
        }

        public bool ResponseCompleteLobby(string verificationCode)
        {
            var game = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                if (game.PlayerDTOs.Count >= game.LimitPlayer)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public partial class ServicesExposed : IChangeUsernameService
    {
        public bool ChangeUsername(string email, string username)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ChangeUsername(email, username);
            return status;
        }

        public bool ValidateAvailabilityUsername(string username)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ValidationUsername(username);
            return status;
        }
    }

    public partial class ServicesExposed : ILoteriaService
    {
        List<GameRoundDTO> loteriaList = new List<GameRoundDTO>();
        public void CreateLoteria(string verificationCode)
        {
            List<PlayerDTO> playerLoteria = new List<PlayerDTO>();
            GameRoundDTO loteria = new GameRoundDTO()
            {
                VerificationCode = verificationCode,
                PlayerDTOs = playerLoteria
            };
            loteriaList.Add(loteria);
        }

        public void DeleteLoteria(string verificationCode)
        {
            var loteria = loteriaList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (loteria != null)
            {
                loteriaList.Remove(loteria);
            }
        }

        public void ExitLoteria(string username, string verificationCode)
        {
            var loteria = loteriaList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (loteria != null)
            {
                var player = loteria.PlayerDTOs.FirstOrDefault(iteration => iteration.Username == username);
                if (player != null)
                {
                    loteria.PlayerDTOs.Remove(player);
                }
            }
        }

        public void JoinLoteria(string username, string verificationCode)
        {
            var newConnection = OperationContext.Current;
            var loteria = loteriaList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (loteria != null)
            {
                PlayerDTO player = new PlayerDTO()
                {
                    Username = username,
                };
                player.Connection = newConnection;
                loteria.PlayerDTOs.Add(player);
                //TODO Bets
            }
        }

        public void ReciveWinner(string username, string verificationCode)
        {
            var loteria = loteriaList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (loteria != null)
            {
                Task task = new Task(() => {
                    for (int i = 0; i < loteria.PlayerDTOs.Count; i++)
                    {
                        try
                        {
                            loteria.PlayerDTOs[i].Connection.GetCallbackChannel<ILoteriaServiceCallBack>().SendWinner(username);
                        }
                        catch (CommunicationObjectAbortedException)
                        {
                            loteria.PlayerDTOs.Remove(loteria.PlayerDTOs[i]);
                        }
                    }
                });
                task.Start();
            }
        }

        public void StartGameLoteria(string verificationCode)
        {
            var loteria = loteriaList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            var lobby = lobbyList.FirstOrDefault(i => i.VerificationCode == verificationCode);
            if (loteria != null && lobby != null)
            {
                loteria.Bet = lobby.Bet;
                loteria.Speed = lobby.Speed;
                Task task = new Task(() =>
                {
                    Thread.Sleep(6000);
                    RandomNumbers DeckCardRandom = new RandomNumbers();
                    List<int> DeckOfCards = DeckCardRandom.FillDeck();
                    for (int i = 0; i < 54; i++)
                    {
                        Thread.Sleep(loteria.Speed);
                        for (int j = 0; j < loteria.PlayerDTOs.Count; j++)
                        {
                            try
                            {
                                loteria.PlayerDTOs[j].Connection.GetCallbackChannel<ILoteriaServiceCallBack>().SendCard(DeckOfCards[i]);
                            }
                            catch (CommunicationObjectAbortedException)
                            {
                                loteria.PlayerDTOs.Remove(loteria.PlayerDTOs[j]);
                            }
                        }
                    }
                });
                task.Start();
            }
        }
    }

}

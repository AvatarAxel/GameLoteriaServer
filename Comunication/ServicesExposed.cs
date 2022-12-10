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
                SendMessage("TXx02Ejgy03aPLbqJ/yr6g==", username, codeVerification);

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
                    player.Connection.GetCallbackChannel<IChatServiceCallBack>().ReciveMessage(player.Username, "vSwpaapgnALaPLbqJ/yr6g==");
                    ChatExisting.PlayerDTOs.Remove(player);
                    SendMessage("NDujuDiTG6lKFB7H0TNLqg==", userName, codeVerification);
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
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                lobbyList.Remove(lobby);
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
                    UpdateTotalPlayers(verificationCode);
                }
            }
        }

        public void JoinGame(string username, string verificationCode)
        {
            var newConnection = OperationContext.Current;
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                PlayerDTO player = new PlayerDTO()
                {
                    Username = username,
                };
                player.Connection = newConnection;
                game.PlayerDTOs.Add(player);
                newConnection.GetCallbackChannel<IGameServiceCallBack>().ResponseTotalPlayers(game.PlayerDTOs.Count);
                UpdateListPlayers(verificationCode);
            }
        }

       private void UpdateListPlayers(string verificationCode)
       {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                Task task = new Task(() => {
                    List<string> JoinedPlayer = new List<string>();
                    JoinedPlayer = FillLists(verificationCode);

                    for (int i = 0; i < game.PlayerDTOs.Count; i++)
                    {
                        try
                        {
                            game.PlayerDTOs[i].Connection.GetCallbackChannel<IGameServiceCallBack>().GetListPlayer(JoinedPlayer);
                        }
                        catch (CommunicationObjectAbortedException)
                        {
                            game.PlayerDTOs.Remove(game.PlayerDTOs[i]);
                        }
                    }
                });
                task.Start();
            }
        }

        private List<string> FillLists(string verificationCode)
        {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            List<string> ListPlayers = new List<string>();
            if (game != null)
            {
                for (int i=0; i<game.PlayerDTOs.Count; i++)
                {
                    ListPlayers.Add(game.PlayerDTOs[i].Username);
                }
                return ListPlayers;
            }
            return ListPlayers;
        }

        public void SendNextHostGame(string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                for (int i = 0; i < lobby.PlayerDTOs.Count; i++)
                {
                    try
                    {
                        if (!Regex.IsMatch(lobby.PlayerDTOs[i].Username, "Invitado") && !Regex.IsMatch(lobby.PlayerDTOs[i].Username, "Guest"))
                        {
                            lobby.PlayerDTOs[i].Connection.GetCallbackChannel<IGameServiceCallBack>().SendNextHostGameResponse(true);
                            return;
                        }
                    }
                    catch (CommunicationObjectAbortedException)
                    {
                        lobby.PlayerDTOs.Remove(lobby.PlayerDTOs[i]);
                    }
                }
                EliminateGame(verificationCode);
                DeleteChat(verificationCode);
            }
        }

        public void GoToGame(string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                for (int i = 0; i < lobby.PlayerDTOs.Count; i++)
                {
                    try
                    {
                        GameManager gameManager = new GameManager();
                        int CoinOfPlayer = gameManager.GetCoins(lobby.PlayerDTOs[i].Username);
                        if (CoinOfPlayer >= lobby.Bet)
                        {
                            lobby.PlayerDTOs[i].Connection.GetCallbackChannel<IGameServiceCallBack>().GoToPlay(true);
                        }
                        else
                        {
                            lobby.PlayerDTOs[i].Connection.GetCallbackChannel<IGameServiceCallBack>().GoToPlay(false);
                        }
                    }
                    catch (CommunicationObjectAbortedException)
                    {
                        lobby.PlayerDTOs.Remove(lobby.PlayerDTOs[i]);
                    }
                }
            }
        }

        public void UpdateTotalPlayers(string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                for (int i = 0; i < lobby.PlayerDTOs.Count; i++)
                {
                    try
                    {
                        lobby.PlayerDTOs[i].Connection.GetCallbackChannel<IGameServiceCallBack>().ResponseTotalPlayers(lobby.PlayerDTOs.Count);
                    }
                    catch (CommunicationObjectAbortedException)
                    {
                        lobby.PlayerDTOs.Remove(lobby.PlayerDTOs[i]);
                    }                    
                }
            }
        }

        public void UpdateBetCoins(string username, string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                var player = lobby.PlayerDTOs.FirstOrDefault(iteration => iteration.Username == username);
                if (player != null)
                {
                    GameManager gameManager = new GameManager();
                    int coins = gameManager.GetCoins(username);
                    int bet = lobby.Bet;
                    try
                    {
                        player.Connection.GetCallbackChannel<IGameServiceCallBack>().UpdateBetCoinsResponse(coins, bet);
                    }
                    catch(CommunicationObjectAbortedException)
                    {
                        return;
                    }
                }
            }
        }

        public void BanPlayer(string verificationCode, string username)
        {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                var player = game.PlayerDTOs.FirstOrDefault(iteration => iteration.Username == username);
                if (player != null)
                {
                    player.Connection.GetCallbackChannel<IGameServiceCallBack>().BanPlayerResponse(true);

                    game.PlayerDTOs.Remove(player);
                    ExitChat(username, verificationCode);

                    UpdateListPlayers(verificationCode);
                }
            }
        }
    }

    public partial class ServicesExposed : IJoinGameService
    {
        public bool ResponseCodeExist(string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                return true;
            }
            return false;
        }

        public bool ResponseUsernameExist(string verificationCode, string username)
        {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                var player = game.PlayerDTOs.FirstOrDefault(iteration => iteration.Username == username);
                if(player != null)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool ResponseCompleteLobby(string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                if (lobby.PlayerDTOs.Count >= lobby.LimitPlayer)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateCoinsRegistered(string username, string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            GameManager gameManager = new GameManager();
            if (lobby != null)
            {
                if (gameManager.GetCoins(username) >= lobby.Bet)
                { 
                    return true;
                }
            }
            return false;
        }

        public bool ValidateCoinsUnregistered(int coins, string verificationCode)
        {
            var lobby = lobbyList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            GameManager gameManager = new GameManager();
            if (lobby != null)
            {
                if (coins >= lobby.Bet)
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
            var lobby = lobbyList.FirstOrDefault(i => i.VerificationCode == verificationCode);
            if (loteria != null)
            {
                loteria.Bet = lobby.Bet;               
                GameManager gameManager = new GameManager();            
                if(gameManager.Betting(username, loteria.Bet))
                {
                    PlayerDTO player = new PlayerDTO()
                    {
                        Username = username,
                    };
                    player.Connection = newConnection;
                    loteria.PlayerDTOs.Add(player);
                }
            }
        }

        public void ReciveWinner(string username, string verificationCode, int totalCoins)
        {
            GameManager gameManager = new GameManager();
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
                gameManager.ReceiveCoinsEarned(username, totalCoins);
                task.Start();
            }
        }

        public void StartGameLoteria(string verificationCode)
        {
            var loteria = loteriaList.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            var lobby = lobbyList.FirstOrDefault(i => i.VerificationCode == verificationCode);
            if (loteria != null && lobby != null)
            {
                loteria.Speed = lobby.Speed;
                Task task = new Task(() =>
                {
                    Thread.Sleep(3000);
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

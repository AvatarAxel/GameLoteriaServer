using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Logic;
using System.Collections.Generic;

namespace Testing
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegisterUser_Successful()
        {
            bool expectedResult = true; 
            bool actualResult; 

            PlayerDTO playerDTO = new PlayerDTO();
            playerDTO.Birthday = DateTime.Now;
            playerDTO.Coin = 500;
            playerDTO.Email = "lupito987@gmail.com";
            playerDTO.Username = "ElOsoLoco";
            playerDTO.Password = "27262";

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.RegisterUser(playerDTO);

            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void RegisterUserRepeat_Unsuccessful()
        {
            bool expectedResult = false; //Esperamos que sea verdadero la prueba
            bool actualResult; //GUardar lo que me trae el metodo
                               
            PlayerDTO playerDTO = new PlayerDTO();
            playerDTO.Birthday = DateTime.Now;
            playerDTO.Coin = 500;
            playerDTO.Email = "lupito987@gmail.com";
            playerDTO.Username = "ElOsoLoco";
            playerDTO.Password = "27262";


            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.RegisterUser(playerDTO);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RegisterUserFalseEmail_Unsuccessful()
        {
            bool expectedResult = false;
            bool actualResult;

            PlayerDTO playerDTO = new PlayerDTO();
            playerDTO.Birthday = DateTime.Now;
            playerDTO.Coin = 500;
            playerDTO.Email = "Blabla21124x3r";
            playerDTO.Username = "GatoDeRancho";
            playerDTO.Password = "UwU";


            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.RegisterUser(playerDTO);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RegisterUserDataNull_Successful()
        {
            bool expectedResult = false;
            bool actualResult;

            PlayerDTO playerDTO = new PlayerDTO();
            playerDTO.Birthday = DateTime.Now;
            playerDTO.Coin = 500;
            playerDTO.Email = "";
            playerDTO.Username = "GatoDeRancho";
            playerDTO.Password = "UwU";


            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.RegisterUser(playerDTO);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RegisterUserDataIncorrect_Successful()
        {
            bool expectedResult = false;
            bool actualResult;

            PlayerDTO playerDTO = new PlayerDTO();
            playerDTO.Birthday = DateTime.Now;
            playerDTO.Coin = 500;
            playerDTO.Email = "aavp1603@hotmail.com";
            playerDTO.Username = "🤩🤩🤩🤩🤩";
            playerDTO.Password = "UwU";


            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.RegisterUser(playerDTO);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidationEmail_Successful()
        {
            bool expectedResult = true;
            bool actualResult;

            string email = "aavp1603@hotmail.com";

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.ValidationEmail(email);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidationUsernameDataThereIs_Unsuccessful()
        {
            bool expectedResult = true;
            bool actualResult;

            string username = "Ale";

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.ValidationUsername(username);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ValidationUsernameNotDataThereIs_Unsuccessful()
        {
            bool expectedResult = false;
            bool actualResult;

            string username = "SanJuanDeUlua";

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.ValidationUsername(username);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UserExist_Unsuccessful()
        {
            bool expectedResult = false;
            bool actualResult;

            string username = "SanJuanDeUlua";
            string password = "GggNoexiste";

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.UserExist(username, password);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UserExistDataOneThereIs_Unsuccessful()
        {
            bool expectedResult = true;
            bool actualResult;

            string username = "EsteNoExiste";
            string email = "aavp1603@hotmail.com";

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.UserExist(username, email);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ReceiveEmail_Unsuccessful()
        {
            bool expectedResult = false;
            bool actualResult;

            string codeVerification = "EsteNoExiste";
            string email = null;

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.ReceiveEmail(email, codeVerification);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ReceiveEmail_Successful()
        {
            bool expectedResult = true;
            bool actualResult;

            string codeVerification = "13243423";
            string email = "aavp1603@hotmail.com";

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.ReceiveEmail(email, codeVerification);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ChangePassword_Successful()
        {
            bool expectedResult = false;
            bool actualResult;

            string password = null;
            string email = "aavp1603@hotmail.com";

            UserManager userManagerTest = new UserManager();
            actualResult = userManagerTest.ReceiveEmail(email, password);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void AuthenticationLogin_Successful()
        {

            PlayerDTO playerDTO = new PlayerDTO();
            playerDTO.Username = "Ale";
            playerDTO.Password = "987";

            PlayerDTO playerDTO1 = new PlayerDTO();
            playerDTO1.Username = "Ale";
            playerDTO1.Password = "789";

            UserManager userManagerTest = new UserManager();
            playerDTO1 = userManagerTest.AuthenticationLogin(playerDTO1.Username, playerDTO1.Password);

            Assert.Equals(playerDTO, playerDTO1);
        }

        [TestMethod]
        public void Betting_Successful()
        {

           GameManager gameManagerTest = new GameManager();

            bool expectedResult = true;
            bool actualResult;

            string username = "Ale16Pucheta";
            int bet = 500;

            actualResult = gameManagerTest.Betting(username, bet);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void Betting_Unsuccessful()
        {

            GameManager gameManagerTest = new GameManager();

            bool expectedResult = false;
            bool actualResult;

            string username = "Ale16Pucheta";
            int bet = 3000;

            actualResult = gameManagerTest.Betting(username, bet);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ReceiveCoinsEarned_Successful()
        {

            GameManager gameManagerTest = new GameManager();

            bool expectedResult = true;
            bool actualResult;

            string username = "Ale16Pucheta";
            int bet = 2300;

            actualResult = gameManagerTest.ReceiveCoinsEarned(username, bet);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void ReceiveCoinsEarned_Unsuccessful()
        {

            GameManager gameManagerTest = new GameManager();

            bool expectedResult = false;
            bool actualResult;

            string username = "Ale16Pucheta";
            int bet = -20;

            actualResult = gameManagerTest.ReceiveCoinsEarned(username, bet);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetCoins_Successful()
        {

            GameManager gameManagerTest = new GameManager();

            int expectedResult = 2300;
            int actualResult;

            string username = "Ale16Pucheta";

            actualResult = gameManagerTest.GetCoins(username);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetFriendList_Unccessful()
        {

            UserManager userManagerTest = new UserManager();

            int expectedResult = 1;
            int actualResult;

            actualResult = userManagerTest.CheckNumberOfFriends("aavp1603@hotmail.com");

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}

using Chapter.Controllers;
using Chapter.Interfaces;
using Chapter.Models;
using Chapter.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteChapter.Controller
{
    public class LoginControllerTeste
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            //Arrange - Preparação
            var repositoryFake = new Mock<IUsuarioRepository>();

            repositoryFake.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            var controller = new LoginController(repositoryFake.Object);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.Email = "batata@email.com";
            dadosUsuario.Senha = "batitinha";

            //Act - Açã0
            var resultado = controller.Login(dadosUsuario);

            //Assert - Verificação
            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }


        [Fact]
        public void LoginController_Retornar_Token()
        {
            //Arrange - Preparação
            Usuario usuarioRetornado = new Usuario();
            usuarioRetornado.Email = "email@email.com";
            usuarioRetornado.Senha = "1234";
            usuarioRetornado.Tipo = "0";
            usuarioRetornado.Id = 1;

            var repositoryFake = new Mock<IUsuarioRepository>();

            repositoryFake.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetornado);

            string issuerValidacao = "chapter-chave-autenticacao";

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.Email = "batata@email.com";
            dadosUsuario.Senha = "batitinha";

            var controller = new LoginController(repositoryFake.Object);

            //Act - Ação
            OkObjectResult resultado = (OkObjectResult)controller.Login(dadosUsuario);

            string tokenString = resultado.Value.ToString().Split(' ')[3];

            var JwtHandler = new JwtSecurityTokenHandler();
            var tokenJwt = JwtHandler.ReadJwtToken(tokenString);

            //Assert - Verificação
            Assert.Equal(issuerValidacao, tokenJwt.Issuer);
        }
    }
}

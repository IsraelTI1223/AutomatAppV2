using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using AutomatApp.Data.Login;
using AutomatApp.Entities.Models;
using AutomatApp.Utilities.Core.Interceptors;
using AutomatApp.Utilities.Core.Responses;

namespace AutomatApp.Business.Login
{
    public class LoginBusines
    {
        private readonly LoginData loginData = new LoginData();

        public ResponseSimple<UsuarioModel> Login(string correo)
        => CoreInterceptor.Trace(loginData.Login,correo);


    }
}

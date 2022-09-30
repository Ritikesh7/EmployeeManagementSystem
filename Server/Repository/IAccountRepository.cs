using Microsoft.AspNetCore.Identity;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> UserSignup(SignUp signUp);

        Task<string> LoginAsync(SignIn signIn);
    }
}

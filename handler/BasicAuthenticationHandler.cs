using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text;
using System.Net.Http.Headers;
using TodoApi.Models;
using System.Security.Claims;
namespace ProductAPIVS.Handler
{
  public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>{
    private readonly TodoContext _DBContext;
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> option, ILoggerFactory logger,
    UrlEncoder encoder, ISystemClock clock, TodoContext dbContext):base(option,logger,encoder,clock){
      _DBContext = dbContext;
    }
    protected async override Task<AuthenticateResult> HandleAuthenticateAsync(){
      if(!Request.Headers.ContainsKey("Authorization")){
        return AuthenticateResult.Fail("No header found");
      }

      var _headervalue=AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
      var bytes=Convert.FromBase64String(_headervalue.Parameter);
      string credentials=Encoding.UTF8.GetString(bytes);
      if(!string.IsNullOrEmpty(credentials)){
        string[] array = credentials.Split(":");
        string username = array[0];
        string password = array[1];

        Console.WriteLine(username);
        Console.WriteLine(password);

        var user = this._DBContext.Users.First();
        Console.WriteLine(user);
        Console.WriteLine(user.Username);
        Console.WriteLine(user.Password);

        if(user.Username == username && user.Password == password){
          return AuthenticateResult.Fail("UnAuthrorized");
        }

        var claim=new[]{new Claim(ClaimTypes.Name,username)};
        var identity=new ClaimsIdentity(claim,Scheme.Name);
        var principal=new ClaimsPrincipal(identity);
        var ticket=new AuthenticationTicket(principal,Scheme.Name);
        return AuthenticateResult.Success(ticket);
      }else{
        return AuthenticateResult.Fail("Header blank or null");
      }
    }

  }
}
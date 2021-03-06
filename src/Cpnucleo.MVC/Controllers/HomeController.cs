﻿using Cpnucleo.MVC.Services;
using Cpnucleo.Shared.Queries.AuthUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Cpnucleo.MVC.Controllers;

public class HomeController : BaseController
{
    private readonly IAuthUserGrpcService _authUserGrpcService;
    private readonly IConfiguration _configuration;

    private HomeViewModel _viewModel;

    public HomeController(IConfiguration configuration)
        : base(configuration)
    {
        _authUserGrpcService = MagicOnionClient.Create<IAuthUserGrpcService>(CreateAuthenticatedChannel());
        _configuration = configuration;
    }

    public HomeViewModel ViewModel
    {
        get
        {
            if (_viewModel == null)
            {
                _viewModel = new HomeViewModel();
            }

            return _viewModel;
        }
        set => _viewModel = value;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string? returnUrl = null, bool logout = false)
    {
        try
        {
            if (logout)
            {
                await HttpContext.SignOutAsync();

                return RedirectToAction("Login");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(HomeViewModel obj, string? returnUrl = null)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _authUserGrpcService.AuthUser(new AuthUserQuery(obj.Auth.Usuario, obj.Auth.Senha));

            if (result.OperationResult == OperationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                return View();
            }
            else
            {
                IEnumerable<Claim> claims = new[]
                {
                    new Claim(ClaimTypes.PrimarySid, result.Recurso.Id.ToString()),
                    new Claim(ClaimTypes.Hash, result.Token)
                };

                var principal = ClaimsService.CreateClaimsPrincipal(claims);

                int.TryParse(_configuration["Cookie:Expires"], out var expiresUtc);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(expiresUtc)
                    });

                return RedirectToLocal(returnUrl);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View();
        }
    }

    public IActionResult Erro()
    {
        return View();
    }

    public IActionResult Negado()
    {
        return View();
    }

    public IActionResult NaoEncontrado()
    {
        return View();
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Listar", "Apontamento");
        }
    }
}

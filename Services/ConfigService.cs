﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistAwards.Services
{
  public class ConfigService
  {
    public CookieOptions AccessCookieOptions { get; set; }
    public CookieOptions RefreshCookieOptions { get; set; }

    //  public const CookieOptions LogoutAccessCookieOptions { get; set; }
    //public const CookieOptions LogoutRefreshCookieOptions { get; set; }
    public IWebHostEnvironment Env { get; set; }
    private bool CookieIsSecure = false;

    public ConfigService(IWebHostEnvironment env)
    {
      Env = env;

      if (Env.IsProduction()) CookieIsSecure = true;


    }


    public void ConfigureAccessCookies(DateTimeOffset expires, bool httpOnly = false, string domain = "davidojes.dev", SameSiteMode sameSite = SameSiteMode.None)
    {
      AccessCookieOptions = new CookieOptions
      {
        HttpOnly = httpOnly,
        Expires = expires,
        SameSite = sameSite,
        Secure = CookieIsSecure
      };

      if (Env.IsProduction())
      {
        AccessCookieOptions.Domain = domain;
      }

    }

    public void ConfigureRefreshCookies(DateTimeOffset expires, bool secure = false, bool httpOnly = false, string domain = "davidojes.dev", SameSiteMode sameSite = SameSiteMode.None)
    {
      RefreshCookieOptions = new CookieOptions
      {
        HttpOnly = true,
        Expires = expires,
        SameSite = sameSite,
        Secure = CookieIsSecure
      };

      if (Env.IsProduction())
      {
        RefreshCookieOptions.Domain = domain;
      }
    }

    public CookieOptions GetAccessCookieOptions()
    {
      ConfigureAccessCookies(DateTime.UtcNow.AddDays(1));
      return AccessCookieOptions;
    }

    public CookieOptions GetLogoutAccessCookieOptions()
    {
      ConfigureAccessCookies(DateTime.UtcNow.AddDays(-1));
      return AccessCookieOptions;
    }

    public CookieOptions GetRefreshCookieOptions()
    {
      ConfigureRefreshCookies(DateTime.UtcNow.AddDays(30));
      return RefreshCookieOptions;
    }

    public CookieOptions GetLogoutRefreshCookieOptions()
    {
      ConfigureRefreshCookies(DateTime.UtcNow.AddDays(-1));
      return RefreshCookieOptions;
    }
  }
}

---
services: active-directory
platforms: dotnet
author: Nigel Belham
---

# Integrating an ASP.NET 4.5 MVC 5 Web Application against an Azure AD tenant using WS-Federation

This sample shows how to build a .Net MVC web application that uses WS-Federation to sign-in users from a single Azure Active Directory tenant, using the ASP.Net WS-Federation OWIN middleware.

The use of WS-Federation is appropriate when you want to maintain a single app codebase that can be deployed either against Azure AD or an on-premises provider such as an Active Directory Federation Services (ADFS) instance. For scenarios in which the app targets exclusively Azure AD (or an OpenID Connect compliant provider) please refer to [the WebApp-OpenIdConnect-DotNet sample](https://github.com/Azure-Samples/active-directory-dotnet-webapp-openidconnect).  

For more information about how the protocols work in this scenario and other scenarios, see [Authentication Scenarios for Azure AD](http://go.microsoft.com/fwlink/?LinkId=394414).

## How To Run This Sample

Getting started is simple!  To run this sample you will need:
- Visual Studio 2015
- An Internet connection
- An Azure subscription (a free trial is sufficient)

Every Azure subscription has an associated Azure Active Directory tenant.  If you don't already have an Azure subscription, you can get a free subscription by signing up at [https://azure.microsoft.com](https://azure.microsoft.com).  All of the Azure AD features used by this sample are available free of charge.

### Step 1:  Clone or download this repository

### Step 2:  Create a user account in your Azure Active Directory tenant

If you already have a user account in your Azure Active Directory tenant, you can skip to the next step.  This sample will not work with a Microsoft account, so if you signed in to the Azure portal with a Microsoft account and have never created a user account in your directory before, you need to do that now.  If you create an account and want to use it to sign-in to the Azure portal, don't forget to add the user account as a co-administrator of your Azure subscription.

### Step 3:  Register this sample with your Azure Active Directory tenant

1. Sign in to the [Azure management portal](https://manage.windowsazure.com).
2. Click on Active Directory in the left hand nav.
3. Click the directory tenant where you wish to register the sample application.
4. Click the Applications tab.
5. In the drawer, click Add.
6. Click "Add an application my organization is developing".
7. Enter a friendly name for the application, for example "WsFedAADAuth", select "Web Application and/or Web API", and click next.
8. For the sign-on URL, enter the base URL for the sample, which is by default `https://localhost:44320`.
9. For the App ID URI, enter `https://<your_tenant_name>/WsFedAADAuth`, replacing `<your_tenant_name>` with the name of your Azure AD tenant. Make sure to remember this value, as you will need it later on when configuring your app in Visual Studio.
10. While still in the Azure portal, click the Configure tab of your application.
11. Find the Client ID value and copy it to the clipboard.

### Step 4:  Configure the sample to use your Azure Active Directory tenant

1. Open the solution in Visual Studio.
2. Open the `web.config` file.
3. Find the app key `ida:Tenant` and replace the value with your AAD tenant name.
4. Find the app key `ida:Wtrealm` and replace the value with the App ID URI from the Azure portal.

### Step 5:  Run the sample

Click the sign-in link on the homepage of the application to sign-in.  On the Azure AD sign-in page, enter the username and password of a user account that is defined in your Azure AD tenant.


## About The Code

This sample shows how to use the WS-Federation ASP.Net OWIN middleware to sign-in users from a single Azure AD tenant.  The middleware is initialized in the `Startup.Auth.cs` file, by passing it the App ID URI of the application and the URL of the Azure AD tenant where the application is registered.  The middleware then takes care of:
- Downloading the Azure AD metadata, finding the signing keys, and finding the issuer name for the tenant.
- Processing WS-Federation sign-in responses by validating the signature and issuer in an incoming SAML token, extracting the user's claims, and putting them on ClaimsPrincipal.Current.
- Integrating with the session cookie ASP.Net OWIN middleware to establish a session for the user. 

You can trigger the middleware to send a WS-Federation sign-in request by decorating a class or method with the `[Authorize]` attribute, or by issuing a challenge,
```C#
HttpContext.GetOwinContext().Authentication.Challenge(
	new AuthenticationProperties { RedirectUri = "/" },
	WSFederationAuthenticationDefaults.AuthenticationType);
```
Similarly you can send a signout request,
```C#
HttpContext.GetOwinContext().Authentication.SignOut(
	WSFederationAuthenticationDefaults.AuthenticationType,
	CookieAuthenticationDefaults.AuthenticationType);
```

All of the OWIN middleware in this project is created as a part of the open source [Katana project](http://katanaproject.codeplex.com).  You can read more about OWIN [here](http://owin.org).

# Before Starting Development

Run The Following Commands on "Package Manager Console";

```
dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>" --project Bloggie

dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>" --project Bloggie

dotnet user-secrets set "EmailUsername" "<email-username>" --project Bloggie

dotnet user-secrets set "EmailPassword" "<email-password>" --project Bloggie
```

//package manager consola bunlarý yapýþtýrdýk
dotnet user-secrets set "EmailUsername" "test@burcinilker.com" --project Bloggie

dotnet user-secrets set "EmailPassword" "*******." --project Bloggie

##useful links

https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows

https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-6.0&tabs=visual-studio


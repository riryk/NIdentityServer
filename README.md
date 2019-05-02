### Prerequisites
There is only one prerequisite for this solution - IdentityServer requires self-signed certificate to encrypt access tokens

*To Install Token Sign Certificate*

1. Open Developer Command Prompt for *VS2015* as Administrator
2. Execute: `cd /d <path to repo>\tools`
3. Execute: `makesigncert N_Identity_Sign_Certificate`. 
	If `makecert` doesn't exist (usually on Windows 10) [Windows SDK](https://developer.microsoft.com/en-us/windows/downloads/windows-10-sdk) needs to be installed
4. If IdentityServer runs on IIS: Grant read access to this certificate to IdentityServer's AppPool's identity [instruction](http://stackoverflow.com/questions/2609859/how-to-give-asp-net-access-to-a-private-key-in-a-certificate-in-the-certificate)

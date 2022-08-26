# TodoApi

noter:

bruger dotnet core

laver certificat på pc'en
dotnet dev-certs https --trust


for at lave et nyt certificat. i powershell some admin
 $cert = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dns jacknet.io
 $pwd = ConvertTo-SecureString -String "password" -Force -AsPlainText
 $certpath ="Cert:\localmachine\my\$($cert.Thumbprint)"

(exports til filepath)
 Export-PfxCertificate -Cert $certpath -FilePath c:\jack.pfx -Password $pwd

 (i certmgr importer ny fil til trusted certificater)

 generate user secret
 dotnet user-secrets set "CertPassword" "password"# TodoApi

Jeg har forsøgt at tilføje base64 encryption/decoding jeg har haft en del problemer med database osv. Men koncepterne er i orden.

# totp-net
a little totp c# library + GUI with google authenticator conform QR-Code generation. 

## Quickstart

Generate the Actual code for the Secret GEZDGNBVGY3TQOJQGEZDGNBVGY3TQOJQ. The secret haxe to be base32 encodet. 
In this Example t1 is set to 30sec and we will get 8 digit long code. 

```c#
string secret = "GEZDGNBVGY3TQOJQGEZDGNBVGY3TQOJQ";
Totp totp = new Totp(secret,30,8);
string totpCode = totp.getCodeString();
```


## Resorces
* [rfc6238](https://tools.ietf.org/html/rfc6238)

## Used Librarys and Classes

* [QrCode.Net](http://qrcodenet.codeplex.com/)
* [Circular Progress Bar](https://visualstudiogallery.msdn.microsoft.com/9d2fe9ac-dea4-4551-a015-6c59500c7779)
* [Base32](http://scottless.com/blog/archive/2014/02/15/base32-encoder-and-decoder-in-c.aspx) 

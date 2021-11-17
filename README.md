# Sam.ReCaptcha

`Sam.ReCaptcha` is a captcha generator and validator for ASP.NET Core applications.

## Install via NuGet

To install Sam.ReCaptcha, run the following command in the Package Manager Console:

```
PM> Install-Package Sam.ReCaptcha 
```

You can also view the [package page](https://www.nuget.org/packages/Sam.ReCaptcha/) on NuGet.

## Usage:

- To register its default providers, call `services.AddReCaptchaServices();` method in your [Startup class](/src/Presentation/Startup.cs).

```csharp

using Sam.ReCaptcha;

namespace Presentation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddReCaptchaServices(o =>
            {
                o.CodeCharacter = "0123456789";
                o.ForeColor = Brushes.DarkBlue;
                o.BackColor = Color.White;
                o.HatchColor = Color.DarkCyan;
            });
        }
    }
}

```
- After use the Sam.ReCaptcha package, add foo property in your model 
```csharp

public class MyModel
{
    public string foo { get; set; }
}

```

- Then to use it, add its new Htmlhelper to [your view](/src/Presentation/Views/Home/Index.cshtml):
```csharp

@using Sam.ReCaptcha

@model MyModel

<form method="post">

    @Html.AddReCaptcha()
    
    <input asp-for="foo" />
    <span asp-validation-for="foo"></span>
    
    <input type="submit" value="submit" />

</form>

```


- Now you can add the `ValidateCaptcha` attribute [to your action method](/src/Presentation/Controllers/HomeController.cs) to verify the entered security code:

```csharp

[HttpPost]
[ReChaptchaValidator(inputName: "foo",errorMessage: "Chaptcha is not Valid")]
public IActionResult Index(MyModel model)
{
    return View(model);
}

```


## Note:

To run this project on non-Windows-based operating systems, you will need to install `libgdiplus` too:

- Ubuntu :

```bash

  apt-get install libgdiplus

```

- Fedora :

```bash

  dnf install libgdiplus

```

- CentOS 7 and above:


```bash

  yum install autoconf automake libtool
  yum install freetype-devel fontconfig libXft-devel
  yum install libjpeg-turbo-devel libpng-devel giflib-devel libtiff-devel libexif-devel
  yum install glib2-devel cairo-devel
  git clone https://github.com/mono/libgdiplus
  cd libgdiplus
  ./autogen.sh
  make
  make install
  cd /usr/lib64/
  ln -s /usr/local/lib/libgdiplus.so libgdiplus.so
  
```

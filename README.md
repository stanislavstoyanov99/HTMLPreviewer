# ASP.NET Core HTML Previewer

[![GitHub license](https://img.shields.io/github/license/stanislavstoyanov99/HTMLPreviewer?color=brightgreen)](https://github.com/stanislavstoyanov99/HTMLPreviewer/blob/main/LICENSE)

## :point_right: Project Introduction :point_left:

**HTML Previewer** is a ready-to-use ASP.NET Core 3.1 web application made for a practical assignment. The application can render HTML code written by the user using the provided form. The user also can save the HTML code, edit it and check if the content is the same as original one. There is also a functionality, which allows the user to preview the already saved html code. In the browser adress bar the link can be shared with other users. The application does not require any registration. 

## Sample images
**Input textarea**
![Image 1](https://github.com/stanislavstoyanov99/HTMLPreviewer/blob/main/images/1.jpg)
**Rendered HTML content**
![Image 2](https://github.com/stanislavstoyanov99/HTMLPreviewer/blob/main/images/2.jpg)
**Notifications popup**
![Image 3](https://github.com/stanislavstoyanov99/HTMLPreviewer/blob/main/images/3.jpg)
**Check with original popup (the same content)**
![Image 4](https://github.com/stanislavstoyanov99/HTMLPreviewer/blob/main/images/4.jpg)
**Check with original popup (different content)**
![Image 5](https://github.com/stanislavstoyanov99/HTMLPreviewer/blob/main/images/5.jpg)
**Preview page with link in the address bar**
![Image 6](https://github.com/stanislavstoyanov99/HTMLPreviewer/blob/main/images/6.jpg)

## :floppy_disk: Database Setup
HTML Previewer uses MSSQL Database with the name "HTMLPreviewer", which is automatically created on application startup. You should change the server name in **appsettings.Development.json** (located in HTMLPreviewer.Web) with yours. The provided one is "localhost\\SQLEXPRESS".

## Unit tests Code coverage

![Code coverage](https://github.com/stanislavstoyanov99/HTMLPreviewer/blob/main/images/tests-code-coverage.jpg)

## :hammer: Used technologies
* ASP.NET [CORE 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1 "CORE 3.1") MVC
* Entity Framework [CORE 3.1](https://docs.microsoft.com/en-us/ef/core/ "CORE 3.1")
* [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/ "Newtonsoft.Json")
* [HtmlSanitizer](https://github.com/mganss/HtmlSanitizer)
* [TinyMCE](https://github.com/tinymce/)
* [Bootstrap](https://github.com/twbs/bootstrap)
* [Notyf](https://github.com/aspnetcorehero/ToastNotification)
* [Toastify](https://github.com/apvarun/toastify-js)
* AJAX real-time Requests
* [jQuery](https://github.com/jquery/jquery)
* [xUnit](https://github.com/xunit/xunit)
* In-Memmory Database for Unit tests

## Author

[Stanislav Stoyanov](https://github.com/stanislavstoyanov99)
- Facebook: [@Станислав Стоянов](https://www.facebook.com/profile.php?id=100000714808058)
- LinkedIn: [@stanislavstoyanov99](https://www.linkedin.com/in/stanislavstoyanov99/)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

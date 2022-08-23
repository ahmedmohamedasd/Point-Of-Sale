#pragma checksum "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6217afde147c084b9bc006165d11b4bd6ff021b3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Track\Project\PointOfSale\PointOfSale\Views\_ViewImports.cshtml"
using PointOfSale;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Track\Project\PointOfSale\PointOfSale\Views\_ViewImports.cshtml"
using PointOfSale.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6217afde147c084b9bc006165d11b4bd6ff021b3", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec2866b5d071513424b12070d79d2940b042d2b5", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!--<div class=""main-content"">
    <div class=""content-wrapper"">
        <div class=""container-fluid"">-->
<!-- Zero configuration table -->
<!--<section id=""configuration"">
                <h1 class=""text-danger"">Error.</h1>
                <h2 class=""text-danger"">An error occurred while processing your request.</h2>
                <h3>Exception Details</h3>
                <div class=""alert alert-danger"">
                    <h5>Exception Path</h5>
                    <hr />
                    <p>");
#nullable restore
#line 17 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
                  Write(ViewBag.Path);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"alert alert-danger\">\r\n                    <h5>Exception Message</h5>\r\n                    <hr />\r\n                    <p>");
#nullable restore
#line 22 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
                  Write(ViewBag.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n                <div class=\"alert alert-danger\">\r\n                    <h5>Exception StackTrace</h5>\r\n                    <hr />\r\n                    <p>");
#nullable restore
#line 27 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
                  Write(ViewBag.StackTrace);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n\r\n\r\n                <a asp-controller=\"Home\" asp-action=\"Index\" class=\"btn btn-outline-success\">Back to Home</a>\r\n            </section>\r\n        </div>\r\n    </div>\r\n</div>-->\r\n\r\n\r\n");
#nullable restore
#line 38 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
 if (ViewBag.ErrorTitle == null)
{


    

#line default
#line hidden
#nullable disable
#nullable restore
#line 42 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
     if (Model.ShowRequestId)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>\r\n            <strong>Request ID:</strong> <code>");
#nullable restore
#line 45 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
                                          Write(Model.RequestId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</code>\r\n        </p>\r\n");
#nullable restore
#line 47 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <h3>Development Mode</h3><p>
        Swapping to <strong>Development</strong> environment will display more detailed information about the error that occurred.
    </p><p>
        <strong>The Development environment shouldn't be enabled for deployed applications.</strong>
        It can result in displaying sensitive information from exceptions to end users.
        For local debugging, enable the <strong>Development</strong> environment by setting the <strong>ASPNETCORE_ENVIRONMENT</strong> environment variable to <strong>Development</strong>
        and restarting the app.
    </p> ");
#nullable restore
#line 56 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
         }

else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h1 class=\"text-danger\">");
#nullable restore
#line 60 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n    <h3 class=\"text-danger\">");
#nullable restore
#line 61 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
                       Write(ViewBag.ErrorMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n");
            WriteLiteral("    <a");
            BeginWriteAttribute("href", " href=\"", 2133, "\"", 2158, 1);
#nullable restore
#line 65 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
WriteAttributeValue("", 2140, ViewBag.returnUrl, 2140, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-outline-dark\">Back</a>\r\n");
#nullable restore
#line 66 "D:\Track\Project\PointOfSale\PointOfSale\Views\Shared\Error.cshtml"
                                                                                                 
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
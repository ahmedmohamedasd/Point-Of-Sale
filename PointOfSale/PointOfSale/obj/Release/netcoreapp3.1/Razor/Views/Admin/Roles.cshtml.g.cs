#pragma checksum "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "61950228f7cf557ed94b637cd51ebc08a61817b5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Roles), @"mvc.1.0.view", @"/Views/Admin/Roles.cshtml")]
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
#line 1 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\_ViewImports.cshtml"
using PointOfSale;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\_ViewImports.cshtml"
using PointOfSale.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"61950228f7cf557ed94b637cd51ebc08a61817b5", @"/Views/Admin/Roles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec2866b5d071513424b12070d79d2940b042d2b5", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Roles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<IdentityRole>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddNewRole", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary mb-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditRole", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteRole", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/site.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
  
    ViewData["Title"] = "Roles";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""row"">
    <div class=""col-12"">
        <div class=""card"">
            <div class=""card-header"">
                <div class=""card-title-wrap bar-success"">
                    <h4 class=""card-title"">Group of Roles</h4>
                </div>
            </div>
            <div class=""card-body collapse show"">
                <div class=""card-block card-dashboard"">

                    <div class=""row"">
                        <div class=""col-lg-3"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61950228f7cf557ed94b637cd51ebc08a61817b57070", async() => {
                WriteLiteral("<i class=\"fa fa-plus\" aria-hidden=\"true\"></i>Add New Role");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        </div>
                    </div>
                    <hr />
                    <table class=""table table-striped table-bordered zero-configuration"" id=""datatablesSimple"">
                        <thead>
                            <tr>
                                <th>Name</th>
                                

                                <th>Edit </th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 35 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                             if (Model.Any())
                            {
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                                 foreach (var role in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr class=\"user\">\r\n\r\n                                <td>");
#nullable restore
#line 41 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                               Write(role.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                                \r\n\r\n\r\n                                <td>\r\n");
#nullable restore
#line 46 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                                     if (role.Name != "Admin")
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61950228f7cf557ed94b637cd51ebc08a61817b510240", async() => {
                WriteLiteral("<i class=\"fa fa-pencil\" aria-hidden=\"true\"></i>edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-roleId", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 48 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                                                                                                                           WriteLiteral(role.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["roleId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-roleId", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["roleId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 49 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </td>\r\n                                <td>\r\n");
#nullable restore
#line 52 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                                     if (role.Name != "Admin")
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61950228f7cf557ed94b637cd51ebc08a61817b513475", async() => {
                WriteLiteral("\r\n                                            <span");
                BeginWriteAttribute("id", " id=\"", 2392, "\"", 2423, 2);
                WriteAttributeValue("", 2397, "ConfirmDeleteSpan_", 2397, 18, true);
#nullable restore
#line 55 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
WriteAttributeValue("", 2415, role.Id, 2415, 8, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" style=""display:none"">
                                                <span>Are You sure You want to Delete</span>
                                                <button type=""submit"" class=""btn btn-danger mb-2"">Yes</button>
                                                <a href=""#"" class=""btn btn-primary mb-2""");
                BeginWriteAttribute("onclick", " onclick=\"", 2742, "\"", 2783, 3);
                WriteAttributeValue("", 2752, "ConfirmDelete(\'", 2752, 15, true);
#nullable restore
#line 58 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
WriteAttributeValue("", 2767, role.Id, 2767, 8, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 2775, "\',false)", 2775, 8, true);
                EndWriteAttribute();
                WriteLiteral(">No</a>\r\n                                            </span>\r\n                                            <span");
                BeginWriteAttribute("id", " id=\"", 2895, "\"", 2919, 2);
                WriteAttributeValue("", 2900, "DeleteSpan_", 2900, 11, true);
#nullable restore
#line 60 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
WriteAttributeValue("", 2911, role.Id, 2911, 8, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n                                                <a href=\"#\" class=\"btn btn-danger\"");
                BeginWriteAttribute("onclick", " onclick=\"", 3005, "\"", 3045, 3);
                WriteAttributeValue("", 3015, "ConfirmDelete(\'", 3015, 15, true);
#nullable restore
#line 61 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
WriteAttributeValue("", 3030, role.Id, 3030, 8, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 3038, "\',true)", 3038, 7, true);
                EndWriteAttribute();
                WriteLiteral("><i class=\"fa fa-trash\" aria-hidden=\"true\"></i> Delete</a>\r\n                                            </span>\r\n                                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-roleId", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 54 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                                                                                                                 WriteLiteral(role.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["roleId"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-roleId", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["roleId"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 64 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 69 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"

                                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                                 



                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <h3>There is not Any Roles Yet </h3>\r\n");
#nullable restore
#line 78 "D:\Track\Project\Point-Of-Sale\PointOfSale\PointOfSale\Views\Admin\Roles.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </yr>\r\n                        </tbody>\r\n\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "61950228f7cf557ed94b637cd51ebc08a61817b520544", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<IdentityRole>> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3433bc9e2929e9af67a46a79ba46872db87bdb64"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Request_Edit), @"mvc.1.0.view", @"/Views/Request/Edit.cshtml")]
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
#line 1 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\_ViewImports.cshtml"
using eProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\_ViewImports.cshtml"
using eProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
using Newtonsoft.Json.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 32 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3433bc9e2929e9af67a46a79ba46872db87bdb64", @"/Views/Request/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f10561b1c200766559e800ea7446594538b9cebb", @"/Views/_ViewImports.cshtml")]
    public class Views_Request_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/step-wizard.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
  
    string json = HttpContextAccessor.HttpContext.Session.GetString("cart");
    string json_user_detail = HttpContextAccessor.HttpContext.Session.GetString("user_session");
    Users user = null;
    List<CartItem> listCart = new List<CartItem>();
    decimal total_money = 0;
    decimal a = 0;
    if (json != null)
    {
        JArray jsonResponse = JArray.Parse(json);

        foreach (var item in jsonResponse)
        {
            JObject cartResult = (JObject)item;
            listCart.Add(JsonConvert.DeserializeObject<CartItem>(cartResult.ToString()));
        }
    }
    if (json_user_detail != null)
    {
        JObject jsonResponse = JObject.Parse(json_user_detail);
        user = JsonConvert.DeserializeObject<Users>(json_user_detail.ToString());
    }

#line default
#line hidden
#nullable disable
            DefineSection("css", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "3433bc9e2929e9af67a46a79ba46872db87bdb645641", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
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
#nullable restore
#line 33 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
  
    ViewData["Title"] = "EditDetail";
    var pageList = (IPagedList)ViewBag.itemList;
    Layout = "~/Views/Shared/_Layout_User.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""wrapper"">
    <div class=""gambo-Breadcrumb"">
        <div class=""container-fluit"">
            <div class=""row"">
                <div class=""col-md-12"">
                    <nav aria-label=""breadcrumb"">
                        <ol class=""breadcrumb"">
                            <li class=""breadcrumb-item""><a");
            BeginWriteAttribute("href", " href=\"", 1545, "\"", 1583, 1);
#nullable restore
#line 45 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
WriteAttributeValue("", 1552, Url.Action("Index", "Request"), 1552, 31, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">Index</a></li>
                            <li class=""breadcrumb-item active"" aria-current=""page"">Edit Request</li>
                        </ol>
                    </nav>
                </div>
            </div>
        </div>
    </div>
    <div style=""margin-left: 10px; margin-right: 10px"">
        <div class=""container"">
            <div class=""text-center"" style=""padding:10px"">
");
#nullable restore
#line 56 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                 using (Html.BeginForm("Edit", "Request", FormMethod.Get))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <input type=\"text\" style=\"height:30px\" name=\"itemName\" placeholder=\"Enter item description...\" />\r\n                    <input type=\"submit\" value=\"Search\" class=\"btn-info\" />\r\n");
#nullable restore
#line 60 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n");
#nullable restore
#line 63 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
         if (ViewBag.itemList != null && ViewBag.itemList.Count > 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <table class=""table table-bordered"" border=""1"">
                <thead>
                    <tr>
                        <th>Request ID</th>
                        <th>Item Code</th>
                        <th>Quantity</th>
                        <th>Price</th>
                        <th>Total</th>
                        <th>Action</th>
                    </tr>
                </thead>

                <tbody>
");
#nullable restore
#line 78 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                     foreach (var item in ViewBag.itemList)
                    {
                        total_money += item.Price * item.Quantity;

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 82 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                           Write(item.Request_Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 83 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                           Write(item.ItemCode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 84 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                           Write(item.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 85 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                           Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 86 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                           Write(item.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 88 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                           Write(Html.ActionLink("Update", "Update", "Request", new { req = item.Request_Id, id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                ");
#nullable restore
#line 89 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                           Write(Html.ActionLink("Delete", "DelItem", "Request", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 92 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n\r\n                <tfoot>\r\n                    <tr>\r\n                        <td colspan=\"4\">Total amount of Request </td>\r\n                        <td style=\"font-weight:bolder;color:coral\">");
#nullable restore
#line 98 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                                                              Write(ViewBag.requestTotal);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n                </tfoot>\r\n            </table>\r\n");
#nullable restore
#line 102 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p style=\"text-align:center\">There is no Result</p>\r\n");
#nullable restore
#line 106 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""col-md-12"">
            <div class=""blog-more-btn"">
                <div class=""pagination-area text-center"">
                    <nav aria-label=""navigation"">
                        <div>
                            <h3 style=""color:darkblue; font-size:14px"">

                                ");
#nullable restore
#line 114 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Request\Edit.cshtml"
                           Write(Html.PagedListPager(pageList, page => Url.Action("Edit", new { page })));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                            </h3>\r\n                        </div>\r\n                    </nav>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    </div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591

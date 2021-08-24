#pragma checksum "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bd03134697a41392f16b5459a7fa86e702c682e7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Request_Index), @"mvc.1.0.view", @"/Views/Request/Index.cshtml")]
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
#line 1 "D:\dowload\StationaryMS-chi-branch\Views\_ViewImports.cshtml"
using eProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\dowload\StationaryMS-chi-branch\Views\_ViewImports.cshtml"
using eProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
using Newtonsoft.Json.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 28 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 29 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bd03134697a41392f16b5459a7fa86e702c682e7", @"/Views/Request/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f10561b1c200766559e800ea7446594538b9cebb", @"/Views/_ViewImports.cshtml")]
    public class Views_Request_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
  
    string json = HttpContextAccessor.HttpContext.Session.GetString("cart");
    string json_user_detail = HttpContextAccessor.HttpContext.Session.GetString("user_detail");
    Users user = null;
    List<CartItem> listCart = new List<CartItem>();
    decimal total_money = 0;
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
            WriteLiteral("\n");
#nullable restore
#line 30 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
  
    ViewData["Title"] = "Index";
    var pageList = (IPagedList)ViewBag.itemList;
    Layout = "~/Views/Shared/_Layout_User.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n<h2>Request List</h2>\n\n<p>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bd03134697a41392f16b5459a7fa86e702c682e75410", async() => {
                WriteLiteral("Create New");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</p>\n<div>\n");
#nullable restore
#line 43 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
     using (Html.BeginForm())
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <input type=\"text\" name=\"keyword\" placeholder=\"Enter status...\" />\n        <input type=\"submit\" value=\"Search\" class=\"btn-info\" />\n");
#nullable restore
#line 47 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\n\nPage ");
#nullable restore
#line 50 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
Write(Html.PagedListPager(pageList, page => Url.Action("Index", new { page })));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n");
#nullable restore
#line 52 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
 if (@ViewBag.Alert != null && @ViewBag.Alert.ToString().Length > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div");
            BeginWriteAttribute("class", " class=\"", 1511, "\"", 1545, 3);
            WriteAttributeValue("", 1519, "alert", 1519, 5, true);
            WriteAttributeValue(" ", 1524, "alert-", 1525, 7, true);
#nullable restore
#line 54 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
WriteAttributeValue("", 1531, ViewBag.Alert, 1531, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n        <button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">×</button>\n        <ul>\n");
#nullable restore
#line 57 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
             foreach (var message in @ViewBag.Message)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li>");
#nullable restore
#line 59 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(Html.Raw(@message));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n");
#nullable restore
#line 60 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n        </ul>\n    </div>\n");
#nullable restore
#line 65 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div>Hello</div>");
#nullable restore
#line 67 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
                 }

#line default
#line hidden
#nullable disable
            WriteLiteral("<table class=\"table\">\n    <tr>\n        <th>Request ID</th>\n        <th>Date Request</th>\n        <th>Approved Date</th>\n        <th>Status</th>\n        <th>Reason</th>\n        <th>Action</th>\n\n    </tr>\n");
#nullable restore
#line 78 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
     if (ViewBag.itemList != null && ViewBag.itemList.Count > 0)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 80 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
         foreach (var item in ViewBag.itemList)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>");
#nullable restore
#line 83 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(item.Request_Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 84 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(item.DateRequest);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 85 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(item.ApprovedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 86 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(item.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>");
#nullable restore
#line 87 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(item.Reason);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>\n                    ");
#nullable restore
#line 89 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(Html.ActionLink("Edit", "Edit", "CreateRequest", new { id = item.Request_Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    ");
#nullable restore
#line 90 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(Html.ActionLink("Details", "Details", "Request", new { id = item.Request_Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    ");
#nullable restore
#line 91 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
               Write(Html.ActionLink("Withdraw", "Details", "Request", new { id = item.Request_Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n            </tr>\n");
#nullable restore
#line 94 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 94 "D:\dowload\StationaryMS-chi-branch\Views\Request\Index.cshtml"
         
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n\n</table>\n");
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

#pragma checksum "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "35f61b5f62bb89ea073bb219ed985377e8a43465"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Report_Report), @"mvc.1.0.view", @"/Views/Report/Report.cshtml")]
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
#line 1 "C:\Users\sgn-ginnyn\StationaryMS\Views\_ViewImports.cshtml"
using eProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sgn-ginnyn\StationaryMS\Views\_ViewImports.cshtml"
using eProject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35f61b5f62bb89ea073bb219ed985377e8a43465", @"/Views/Report/Report.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f10561b1c200766559e800ea7446594538b9cebb", @"/Views/_ViewImports.cshtml")]
    public class Views_Report_Report : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<eProject.Models.vRequestItem>>
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
  
    ViewData["Title"] = "Report";
    Layout = "~/Views/Shared/_Layout_User.cshtml";

#line default
#line hidden
#nullable disable
            DefineSection("css", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "35f61b5f62bb89ea073bb219ed985377e8a434654102", async() => {
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
            WriteLiteral(@"<div class=""wrapper"">
    <div class=""gambo-Breadcrumb"">
        <div class=""container-fluit"">
            <div class=""row"">
                <div class=""col-md-10"">
                    <nav aria-label=""breadcrumb"">
                        <ol class=""breadcrumb"">
                            <li class=""breadcrumb-item active"" aria-current=""page""><a href=""#"">Report </a></li>
                        </ol>
                    </nav>
                </div>

            </div>
        </div>
    </div>

    <div style=""margin-left: 10px; margin-right: 10px"">

        <div class=""text-center"" style=""padding:10px"">
");
#nullable restore
#line 29 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
             using (Html.BeginForm())
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
           Write(Html.Label("From:"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 31 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                                    ;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"datetime-local\" name=\"fromDate\" />\r\n");
#nullable restore
#line 33 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
           Write(Html.Label("To:"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                                  ;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"datetime-local\" name=\"toDate\" />\r\n                <input type=\"submit\" value=\"Filter\" name=\"Submit\" class=\"btn-info\" />\r\n");
#nullable restore
#line 36 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
           Write(Html.Label("Get All Report:"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                                              ;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input type=\"submit\" value=\"Search\" name=\"Submit\" class=\"btn-info\" />\r\n");
#nullable restore
#line 38 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n\r\n        <table class=\"table table-bordered\" border=\"1\">\r\n            <thead>\r\n                <tr>\r\n                    <th>\r\n                        ");
#nullable restore
#line 45 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.Request_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 48 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.Reason));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 51 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.User_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 54 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.ItemCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ItemName\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 60 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 63 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.Unit));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 66 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.Category_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 69 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.Stock));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 72 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        Total\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 78 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                   Write(Html.DisplayNameFor(model => model.DateRequest));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 83 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 87 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Request_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 90 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Reason));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 93 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.User_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 96 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.ItemCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 99 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 102 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 105 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Unit));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 108 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Category_Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 111 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Stock));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 114 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 117 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Total));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 120 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                       Write(Html.DisplayFor(modelItem => item.DateRequest));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n\r\n                    </tr>\r\n");
#nullable restore
#line 124 "C:\Users\sgn-ginnyn\StationaryMS\Views\Report\Report.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n</div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<eProject.Models.vRequestItem>> Html { get; private set; }
    }
}
#pragma warning restore 1591

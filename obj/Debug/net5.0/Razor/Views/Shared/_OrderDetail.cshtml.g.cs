#pragma checksum "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5f0c5e3a4c561f491e6d66a2076c3eb37c679e43"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__OrderDetail), @"mvc.1.0.view", @"/Views/Shared/_OrderDetail.cshtml")]
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
#line 1 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
using Newtonsoft.Json.Linq;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5f0c5e3a4c561f491e6d66a2076c3eb37c679e43", @"/Views/Shared/_OrderDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f10561b1c200766559e800ea7446594538b9cebb", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__OrderDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("w-100"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
  
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
            WriteLiteral("\r\n<div class=\"bs-canvas bs-canvas-left position-fixed bg-cart h-100\">\r\n    <div class=\"bs-canvas-header side-cart-header p-3 \">\r\n        <div class=\"d-inline-block  main-cart-title\">My Cart <span>(");
#nullable restore
#line 30 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
                                                               Write(listCart.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Items)</span></div>\r\n        <button type=\"button\" class=\"bs-canvas-close close\" aria-label=\"Close\"><i class=\"uil uil-multiply\"></i></button>\r\n    </div>\r\n    <div class=\"bs-canvas-body\">\r\n        <div class=\"side-cart-items\">\r\n");
#nullable restore
#line 35 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
             if (listCart.Count > 0)
            {
                

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
                 foreach (CartItem cart in listCart)
                {
                    total_money += cart.Item.Price * cart.Quantity;

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"cart-item\">\r\n                        <div class=\"cart-product-img\">\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 1688, "\"", 1711, 1);
#nullable restore
#line 42 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
WriteAttributeValue("", 1694, cart.Item.Images, 1694, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"88\" height=\"88\">\r\n                        </div>\r\n                        <div class=\"cart-text\">\r\n                            <h4>");
#nullable restore
#line 45 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
                           Write(cart.Item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5f0c5e3a4c561f491e6d66a2076c3eb37c679e437554", async() => {
                WriteLiteral("\r\n                                <div class=\"qty-group\">\r\n                                    <div class=\"quantity buttons_added\">\r\n                                        <input type=\"hidden\" name=\"id\"");
                BeginWriteAttribute("value", " value=\"", 2191, "\"", 2221, 1);
#nullable restore
#line 50 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
WriteAttributeValue("", 2199, cart.Item.Description, 2199, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                                        <input type=\"button\" value=\"-\" class=\"minus minus-btn\">\r\n                                        <input type=\"number\" step=\"1\" name=\"quantity\"");
                BeginWriteAttribute("value", " value=\"", 2409, "\"", 2431, 1);
#nullable restore
#line 52 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
WriteAttributeValue("", 2417, cart.Quantity, 2417, 14, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"input-text qty text\">\r\n                                        <input type=\"button\" value=\"+\" class=\"plus plus-btn\">\r\n                                    </div>\r\n                                    <div class=\"cart-item-price\">$");
#nullable restore
#line 55 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
                                                             Write(cart.Item.Price);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" </div>
                                </div>

                                <button type=""submit"" name=""button"" value=""delete"" class=""cart-close-btn""><i class=""uil uil-multiply""></i></button>
                                <button type=""submit"" name=""button"" value=""update"" class=""btn btn-link btn-xs float-right""><span class=""cart-icon""><i class=""uil uil-shopping-cart-alt""></i></span></button>
                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 47 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
AddHtmlAttributeValue("", 1924, Url.Action("AddToCart", "Request"), 1924, 35, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 63 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
                }

#line default
#line hidden
#nullable disable
#nullable restore
#line 63 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
                 
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n    <div class=\"bs-canvas-footer\">\r\n        <div class=\"main-total-cart\">\r\n            <h2>Total</h2>\r\n            <span>$");
#nullable restore
#line 70 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
              Write(total_money);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n        </div>\r\n        <div class=\"checkout-cart\">\r\n            <a");
            BeginWriteAttribute("href", " href=\"", 3459, "\"", 3498, 1);
#nullable restore
#line 73 "C:\Users\sgn-ginnyn\Desktop\StationaryMS\Views\Shared\_OrderDetail.cshtml"
WriteAttributeValue("", 3466, Url.Action("Submit", "Request"), 3466, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"cart-checkout-btn hover-btn\">Proceed to Submit</a>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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

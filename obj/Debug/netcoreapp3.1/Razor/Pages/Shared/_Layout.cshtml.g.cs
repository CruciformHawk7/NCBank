#pragma checksum "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7ccaef0d60da023a6b233176d3d829430be07a64"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(NCBank.Pages.Shared.Pages_Shared__Layout), @"mvc.1.0.view", @"/Pages/Shared/_Layout.cshtml")]
namespace NCBank.Pages.Shared
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
#line 1 "/home/nikhil/Code/NCBank/Pages/_ViewImports.cshtml"
using NCBank;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7ccaef0d60da023a6b233176d3d829430be07a64", @"/Pages/Shared/_Layout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"28c65c74088641389e5f58179d2802b024e4070d", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Shared__Layout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\n<html lang=\"en\">\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ccaef0d60da023a6b233176d3d829430be07a642981", async() => {
                WriteLiteral("\n\t<meta charset=\"utf-8\" />\n\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\n\t<title>");
#nullable restore
#line 6 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
      Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(@" - N&amp;C Bank</title>
	<link rel=""stylesheet"" href=""css\design.css"">
	<link rel = ""stylesheet"" type = ""text/css"" href = ""css\pretty-checkbox.min.css"">
	<link rel=""stylesheet"" 
	      href=""FontAwesome\css\all.css"" />
	<script type = ""text/javascript"" src = ""scripts/indexScript.js""></script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ccaef0d60da023a6b233176d3d829430be07a644554", async() => {
                WriteLiteral(@"
<nav class=""Navigation navbar"">
	<ul>
		<!--suppress CssUnknownTarget -->
		<div class=""logoPlaceHolder"" style=""background-image:url('css/bank.svg'); color: #555""></div>
		<li>
			<a href = ""#top"">
				<span style = ""color: black; font-size: large; width: 10%; 
		       display: inline-block;"">
					<b>");
#nullable restore
#line 22 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
                  Write(ViewData["CustomTitle"]);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - NC</b>\n\t\t\t\t</span>\n\t\t\t</a>\n\t\t</li>\n\t\t<div style = \"display: inline-block; margin: 0 auto; text-align: center; width: 60%\">\n");
#nullable restore
#line 27 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
              
				if ((string) ViewData["PageID"] == "Privacy") {

#line default
#line hidden
#nullable disable
                WriteLiteral("\t\t\t\t\t<li><span class = \"navigator\">Privacy</span></li>\n");
#nullable restore
#line 30 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
				} else {

#line default
#line hidden
#nullable disable
                WriteLiteral("\t\t\t\t\t<li><a href = \"/Privacy\" class = \"navigator\">Privacy</a></li>\n");
#nullable restore
#line 32 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
				}
				if ((string) ViewData["PageID"] == "Home") {

#line default
#line hidden
#nullable disable
                WriteLiteral("\t\t\t\t\t<li><span style = \"font-size: 24px;\">\n\t\t\t\t\t\t<i class=\"fa fa-home\"></i>\n\t\t\t\t\t</span></li>\n");
#nullable restore
#line 37 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
				} else {

#line default
#line hidden
#nullable disable
                WriteLiteral("\t\t\t\t\t<li><a href = \"/Index\" class = \"navigator\" style = \"font-size: 24px !important;\">\n\t\t\t\t\t\t<i class=\"fa fa-home\"></i>\n\t\t\t\t\t</a></li>\n");
#nullable restore
#line 41 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
				}
				if ((string) ViewData["PageID"] == "About") {

#line default
#line hidden
#nullable disable
                WriteLiteral("\t\t\t\t\t<li><span class = \"navigator\">About</span></li>\n");
#nullable restore
#line 44 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
				} else {

#line default
#line hidden
#nullable disable
                WriteLiteral("\t\t\t\t\t<li><a href = \"/About\" class = \"navigator\">About</a></li>\n");
#nullable restore
#line 46 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
				}
			

#line default
#line hidden
#nullable disable
                WriteLiteral("\t\t</div>\n\t</ul>\n\t<div class=\"userdetails\">\n\t\t<a href=\"/Login\" class=\"navigator\">Welcome, Guest!</a>\n\t</div>\n</nav>\n<div class=\"iframepage\">\n\t<main role = \"main\" style=\"position: relative; padding-top: 30px;\">\n\t\t");
#nullable restore
#line 56 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
   Write(RenderBody());

#line default
#line hidden
#nullable disable
                WriteLiteral("\n\t</main>\n</div>\n");
#nullable restore
#line 59 "/home/nikhil/Code/NCBank/Pages/Shared/_Layout.cshtml"
Write(RenderSection("Scripts", required: false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
#pragma checksum "E:\TUNI\TUNI 6to semestre\TUNIWEB\TUNIWEB\Views\Principal\IndexUniversidad.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bc5cd322735bbc78fd3409e75c2667e90cc5184d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Principal_IndexUniversidad), @"mvc.1.0.view", @"/Views/Principal/IndexUniversidad.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Principal/IndexUniversidad.cshtml", typeof(AspNetCore.Views_Principal_IndexUniversidad))]
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
#line 1 "E:\TUNI\TUNI 6to semestre\TUNIWEB\TUNIWEB\Views\_ViewImports.cshtml"
using TUNIWEB;

#line default
#line hidden
#line 2 "E:\TUNI\TUNI 6to semestre\TUNIWEB\TUNIWEB\Views\_ViewImports.cshtml"
using TUNIWEB.Models;

#line default
#line hidden
#line 3 "E:\TUNI\TUNI 6to semestre\TUNIWEB\TUNIWEB\Views\_ViewImports.cshtml"
using TUNIWEB.ClassValidation;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bc5cd322735bbc78fd3409e75c2667e90cc5184d", @"/Views/Principal/IndexUniversidad.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f508d3aaa41fa0e928a0b9f2802a3b4d455cc5ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Principal_IndexUniversidad : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "cerrarsecion", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Principal", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
            BeginContext(0, 144, true);
            WriteLiteral("<nav class=\"navi\">\r\n    <ul>\r\n        <li>\r\n            <i id=\"iconito\" class=\"fas fa-mail-bulk\"></i>\r\n        </li>\r\n        <li>\r\n            ");
            EndContext();
            BeginContext(144, 155, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1e2003126c5c4464b2e4f32f6e9edf34", async() => {
                BeginContext(216, 76, true);
                WriteLiteral("\r\n                <button type=\"submit\">Cerrar sesion</button>\r\n            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(299, 571, true);
            WriteLiteral(@"
        </li>
    </ul>
</nav>
<div id=""bandejadeentrada"" hidden>
    <div id=""entrada""></div>
</div>
<div id=""vista"" hidden></div>
<script>
    var iconito = document.getElementById('iconito')
    iconito.addEventListener('click', function () {
        if (iconito.hasAttribute('cerrado')) {
            document.getElementById('bandejadeentrada').setAttribute('hidden', '');
            iconito.removeAttribute('cerrado');
            document.getElementById('mensajito').remove();
        }
        else {
            $.ajax({
                url: '");
            EndContext();
            BeginContext(871, 40, false);
#line 27 "E:\TUNI\TUNI 6to semestre\TUNIWEB\TUNIWEB\Views\Principal\IndexUniversidad.cshtml"
                 Write(Url.Action("versolicitudes","Principal"));

#line default
#line hidden
            EndContext();
            BeginContext(911, 2384, true);
            WriteLiteral(@"',
                type: 'get',
                success: function (succ) {
                    var div = document.createElement('div');
                    div.className = 'divmesaje';
                    if (succ == null) {
                        var div2 = document.createElement('div');
                        div.id = 'mensajito';
                        div2.className = 'mesaje';
                        var spn = document.createElement('span');
                        spn.textContent = 'Todavia no tiene Solicitudes';
                        div2.appendChild(spn);
                        div.appendChild(div2);
                        document.getElementById('entrada').appendChild(div);
                        iconito.setAttribute('cerrado', true);
                        document.getElementById('bandejadeentrada').removeAttribute('hidden');
                    }
                    else {
                        iconito.setAttribute('cerrado', true);
                         var div = d");
            WriteLiteral(@"ocument.createElement('div');
                         div.className = 'divmesaje';
                        succ.forEach(function (objt) {
                                var div2 = document.createElement('div');

                            div.id = 'mensajito';
                            div2.className = 'mesaje';
                            var input = document.createElement('button');
                            input.setAttribute('btms', '');
                            input.textContent = 'Ver alumno';
                            input.value = objt.id;
                            var spn = document.createElement('span');
                            spn.textContent = objt.nombre;
                            div2.appendChild(spn);
                            div2.appendChild(input);
                            div.appendChild(div2);
                            document.getElementById('entrada').appendChild(div);
                                document.getElementById('bandejadeentrada').");
            WriteLiteral(@"removeAttribute('hidden');
                            
                        });
                        document.querySelectorAll('[btms]').forEach(function (objeto) {
                            objeto.addEventListener('click', function () {
                                $.ajax({
                                    url: '");
            EndContext();
            BeginContext(3296, 36, false);
#line 69 "E:\TUNI\TUNI 6to semestre\TUNIWEB\TUNIWEB\Views\Principal\IndexUniversidad.cshtml"
                                     Write(Url.Action("verAlumno", "Principal"));

#line default
#line hidden
            EndContext();
            BeginContext(3332, 796, true);
            WriteLiteral(@"',
                                    type: 'post',
                                    async: true,
                                    timeout: 0,
                                    data: { id: objeto.value },
                                    success: function (succ) {
                                        $('#vista').removeAttr('hidden');
                                        $('#vista').append(succ);
                                    },
                                    error: function (err) { console.log(err); }
                                });
                            });
                        });
                    }
                },
                error: function (err) { console.log(err) }
            });
        }
    });
</script>
");
            EndContext();
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

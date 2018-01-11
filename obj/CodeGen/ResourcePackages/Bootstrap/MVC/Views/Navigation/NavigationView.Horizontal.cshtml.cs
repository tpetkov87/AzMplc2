#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SitefinityWebApp.ResourcePackages.Bootstrap.MVC.Views.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 3 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
    using Telerik.Sitefinity.Frontend.Mvc.Helpers;
    
    #line default
    #line hidden
    
    #line 4 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
    using Telerik.Sitefinity.Frontend.Navigation.Mvc.Models;
    
    #line default
    #line hidden
    
    #line 5 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
    using Telerik.Sitefinity.Modules.Pages;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/ResourcePackages/Bootstrap/MVC/Views/Navigation/NavigationView.Horizontal.cshtm" +
        "l")]
    public partial class NavigationView_Horizontal : System.Web.Mvc.WebViewPage<Telerik.Sitefinity.Frontend.Navigation.Mvc.Models.INavigationModel>
    {

#line 40 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
public System.Web.WebPages.HelperResult RenderRootLevelNode(NodeViewModel node)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 41 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
 
    if (node.ChildNodes.Count > 0)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <li");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown\"");

WriteLiteralTo(__razor_helper_writer, ">\n            <a");

WriteLiteralTo(__razor_helper_writer, " href=\"#\"");

WriteLiteralTo(__razor_helper_writer, " data-toggle=\"dropdown\"");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown-toggle\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 45 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                                         WriteTo(__razor_helper_writer, node.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\n                <span");

WriteLiteralTo(__razor_helper_writer, " class=\"caret\"");

WriteLiteralTo(__razor_helper_writer, "></span>\n            </a>\n            <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown-menu\"");

WriteLiteralTo(__razor_helper_writer, ">\n");

WriteLiteralTo(__razor_helper_writer, "                ");


#line 49 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
WriteTo(__razor_helper_writer, RenderSubLevelsRecursive(node));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\n            </ul>\n        </li>\n");


#line 52 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
    }
    else
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <li");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 1939), Tuple.Create("\"", 1962)

#line 55 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 1947), Tuple.Create<System.Object, System.Int32>(GetClass(node)

#line default
#line hidden
, 1947), false)
);

WriteLiteralTo(__razor_helper_writer, "><a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 1966), Tuple.Create("\"", 1982)

#line 55 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 1973), Tuple.Create<System.Object, System.Int32>(node.Url

#line default
#line hidden
, 1973), false)
);

WriteAttributeTo(__razor_helper_writer, "target", Tuple.Create(" target=\"", 1983), Tuple.Create("\"", 2008)

#line 55 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 1992), Tuple.Create<System.Object, System.Int32>(node.LinkTarget

#line default
#line hidden
, 1992), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 55 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                                                    WriteTo(__razor_helper_writer, node.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a></li>\n");


#line 56 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
    }


#line default
#line hidden
});

#line 57 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
}
#line default
#line hidden

#line 60 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
public System.Web.WebPages.HelperResult RenderSubLevelsRecursive(NodeViewModel node)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 61 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
 
    foreach (var childNode in node.ChildNodes)
    {
        if(childNode.ChildNodes.Count > 0)
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "             <li");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown-submenu\"");

WriteLiteralTo(__razor_helper_writer, ">\n                <a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 2319), Tuple.Create("\"", 2340)

#line 67 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 2326), Tuple.Create<System.Object, System.Int32>(childNode.Url

#line default
#line hidden
, 2326), false)
);

WriteAttributeTo(__razor_helper_writer, "target", Tuple.Create(" target=\"", 2341), Tuple.Create("\"", 2371)

#line 67 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 2350), Tuple.Create<System.Object, System.Int32>(childNode.LinkTarget

#line default
#line hidden
, 2350), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 67 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                                          WriteTo(__razor_helper_writer, childNode.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\n                    <span");

WriteLiteralTo(__razor_helper_writer, " class=\"right-caret\"");

WriteLiteralTo(__razor_helper_writer, "></span>\n                </a>\n                <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"dropdown-menu\"");

WriteLiteralTo(__razor_helper_writer, ">\n");

WriteLiteralTo(__razor_helper_writer, "                    ");


#line 71 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
WriteTo(__razor_helper_writer, RenderSubLevelsRecursive(childNode));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\n                </ul>\n            </li>\n");


#line 74 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
        }
        else
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <li");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 2653), Tuple.Create("\"", 2681)

#line 77 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 2661), Tuple.Create<System.Object, System.Int32>(GetClass(childNode)

#line default
#line hidden
, 2661), false)
);

WriteLiteralTo(__razor_helper_writer, ">\n                <a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 2702), Tuple.Create("\"", 2723)

#line 78 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 2709), Tuple.Create<System.Object, System.Int32>(childNode.Url

#line default
#line hidden
, 2709), false)
);

WriteAttributeTo(__razor_helper_writer, "target", Tuple.Create(" target=\"", 2724), Tuple.Create("\"", 2754)

#line 78 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 2733), Tuple.Create<System.Object, System.Int32>(childNode.LinkTarget

#line default
#line hidden
, 2733), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 78 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                                          WriteTo(__razor_helper_writer, childNode.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a>\n            </li>\n");


#line 80 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
        }
    }


#line default
#line hidden
});

#line 82 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
}
#line default
#line hidden

#line 85 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
public System.Web.WebPages.HelperResult GetClass(NodeViewModel node)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 86 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
 

    if (node.IsCurrentlyOpened)
    {
        

#line default
#line hidden

#line 90 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
WriteTo(__razor_helper_writer, Html.Raw("active"));


#line default
#line hidden

#line 90 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                           ;
    }
    else if (node.HasChildOpen)
    {
        

#line default
#line hidden

#line 94 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
WriteTo(__razor_helper_writer, Html.Raw("active"));


#line default
#line hidden

#line 94 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                           ;
    }


#line default
#line hidden
});

#line 96 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
}
#line default
#line hidden

        public NavigationView_Horizontal()
        {
        }
        public override void Execute()
        {
WriteLiteral("\n");

WriteLiteral("\n");

            
            #line 8 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
Write(Html.Script(ScriptRef.JQuery, "top"));

            
            #line default
            #line hidden
WriteLiteral("\n");

            
            #line 9 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
Write(Html.Script(Url.WidgetContent("Mvc/Scripts/Bootstrap/js/bootstrap.min.js"), "top", false));

            
            #line default
            #line hidden
WriteLiteral("\n\n<div");

WriteAttribute("class", Tuple.Create(" class=\"", 389), Tuple.Create("\"", 412)
            
            #line 11 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
, Tuple.Create(Tuple.Create("", 397), Tuple.Create<System.Object, System.Int32>(Model.CssClass
            
            #line default
            #line hidden
, 397), false)
);

WriteLiteral(">\n    ");

WriteLiteral("\n\n    <nav");

WriteLiteral(" class=\"navbar navbar-default\"");

WriteLiteral(" role=\"navigation\"");

WriteLiteral(">\n\n        <div");

WriteLiteral(" class=\"container-fluid\"");

WriteLiteral(">\n            <div");

WriteLiteral(" class=\"navbar-header\"");

WriteLiteral(">\n              <button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"navbar-toggle\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" data-target=\"#bs-example-navbar-collapse-1\"");

WriteLiteral(">\n                <span");

WriteLiteral(" class=\"sr-only\"");

WriteLiteral(">Toggle navigation</span>\n                <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\n                <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\n                <span");

WriteLiteral(" class=\"icon-bar\"");

WriteLiteral("></span>\n              </button>\n              <a");

WriteLiteral(" class=\"navbar-brand\"");

WriteLiteral(" href=\"#\"");

WriteLiteral(">Brand</a>\n            </div>\n\n            <div");

WriteLiteral(" class=\"collapse navbar-collapse\"");

WriteLiteral(" id=\"bs-example-navbar-collapse-1\"");

WriteLiteral(">\n                <ul");

WriteLiteral(" class=\"nav navbar-nav\"");

WriteLiteral(">\n");

            
            #line 29 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 29 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                     foreach (var node in Model.Nodes)
                    {
                        
            
            #line default
            #line hidden
            
            #line 31 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                   Write(RenderRootLevelNode(node));

            
            #line default
            #line hidden
            
            #line 31 "..\..\ResourcePackages\Bootstrap\MVC\Views\Navigation\NavigationView.Horizontal.cshtml"
                                                  ;
                    }

            
            #line default
            #line hidden
WriteLiteral("              </ul>\n            </div><!-- /.navbar-collapse -->\n        </div><!" +
"-- /.container-fluid -->\n    </nav>\n</div>\n\n");

WriteLiteral("\n");

WriteLiteral("\n");

WriteLiteral("\n");

WriteLiteral("\n");

WriteLiteral("\n");

        }
    }
}
#pragma warning restore 1591

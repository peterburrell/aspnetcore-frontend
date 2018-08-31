using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Demo.Web.Framework.TagHelpers
{
    /// <summary>
    /// Renders the HTML markup from an editor template for the specified model expression.
    /// </summary>
    [HtmlTargetElement(TagHelperName, Attributes = "for", TagStructure = TagStructure.WithoutEndTag)]
    public class EditorTagHelper : KronosTagHelper
    {
        public const string TagHelperName = TagHelperPrefix + "editor";

        private readonly IHtmlHelper _htmlHelper;

        public EditorTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ((IViewContextAware)_htmlHelper).Contextualize(ViewContext);

            output.Content.SetHtmlContent(_htmlHelper.Editor(ExpressionHelper.GetExpressionText(For.Name)));

            output.TagName = null;
        }
    }
}
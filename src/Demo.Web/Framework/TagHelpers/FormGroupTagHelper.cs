using Demo.Web.Framework.HtmlHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Demo.Web.Framework.TagHelpers
{
    public abstract class KronosTagHelper : TagHelper
    {
        public const string TagHelperPrefix = "k-";
    }

    [HtmlTargetElement(TagHelperName, Attributes = "for", TagStructure = TagStructure.WithoutEndTag)]
    public class FormGroupTagHelper : KronosTagHelper
    {
        public const string TagHelperName = TagHelperPrefix + "form-group";

        private readonly IHtmlHelper _htmlHelper;

        public FormGroupTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        /// <summary>
        /// An expression to be evaluated against the current model.
        /// </summary>
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ((IViewContextAware)_htmlHelper).Contextualize(ViewContext);

            output.Content.SetHtmlContent(_htmlHelper.FormGroup(ExpressionHelper.GetExpressionText(For.Name)));

            output.TagName = null;
        }
    }
}
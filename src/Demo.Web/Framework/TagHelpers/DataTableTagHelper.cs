using System.Collections;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Demo.Web.Framework.TagHelpers
{
    [HtmlTargetElement(TagHelperName, Attributes = "for", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DataTableTagHelper : KronosTagHelper
    {
        public const string TagHelperName = TagHelperPrefix + "data-table";

        private readonly IHtmlHelper _htmlHelper;

        public DataTableTagHelper(IHtmlHelper htmlHelper)
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

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var items = For.Model as IEnumerable;

            if(items == null)
                return Task.CompletedTask;

            var props = For.Metadata.ElementMetadata.Properties.OrderBy(p => p.Order).ToArray();

            var builder = new HtmlContentBuilder()
                .AppendHtml("<thead>")
                .AppendHtml(GenerateHeaderRow(props))
                .AppendHtml("</thead>")
                .AppendHtml("<tbody>")
                .AppendHtml(GenerateBodyRows(items, props))
                .AppendHtml("</tbody>");

            output.TagName = "table";
            output.AddClass("table", HtmlEncoder.Default);

            output.Content.SetHtmlContent(builder);

            return Task.CompletedTask;
        }

        private IHtmlContent GenerateBodyRows(IEnumerable items, ModelMetadata[] props)
        {
            var builder = new HtmlContentBuilder();

            foreach (var item in items)
            {
                builder.AppendHtml("<tr>");
                foreach (var prop in props)
                {
                    // todo: use HtmlHelper.DisplpayFor
                    var value = prop.PropertyGetter(item);
                    builder.AppendFormat("<td>{0}</td>", value);
                }
                builder.AppendHtml("</tr>");
            }

            return builder;
        }

        private IHtmlContent GenerateHeaderRow(ModelMetadata[] props)
        {
            var builder = new HtmlContentBuilder();
            builder.AppendHtml("<tr>");

            foreach (var prop in props)
            {
                builder.AppendFormat("<th>{0}</th>", prop.DisplayName);
            }

            builder.AppendHtml("</tr>");
            return builder;
        }
    }
}
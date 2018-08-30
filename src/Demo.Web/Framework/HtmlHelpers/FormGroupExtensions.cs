using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Web.Framework.HtmlHelpers
{
    public static class FormGroupExtensions
    {
        public static IHtmlContent FormGroupFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return GenerateFormGroup(htmlHelper, ExpressionHelper.GetExpressionText(expression));
        }

        public static IHtmlContent FormGroup(this IHtmlHelper htmlHelper, string expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return GenerateFormGroup(htmlHelper, expression);
        }

        private static IHtmlContent GenerateFormGroup(this IHtmlHelper htmlHelper, string expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var modelExplorer = ExpressionMetadataProvider.FromStringExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);

            if (modelExplorer.Metadata.HideSurroundingHtml)
                return htmlHelper.Editor(expression);

            var content = new HtmlContentBuilder()
                .AppendHtml("<div class='form-group'>")
                .AppendHtml(htmlHelper.Label(expression))
                .AppendHtml(htmlHelper.Editor(expression, new { htmlAttributes = new { @class = "form-control" } }));

            if (!string.IsNullOrEmpty(modelExplorer.Metadata.Description))
                content.AppendHtml(string.Format("<small class='form-text text-muted'>{0}</small>", modelExplorer.Metadata.Description));

            content
                .AppendHtml(htmlHelper.ValidationMessage(expression, null, new { @class = "text-danger" }))
                .AppendHtml("</div>");

            return content;
        }

        public static IHtmlContent CheckboxListFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return GenerateInputList(htmlHelper, ExpressionHelper.GetExpressionText(expression), selectList);
        }

        public static IHtmlContent CheckboxList(this IHtmlHelper htmlHelper, string expression, IEnumerable<SelectListItem> selectList)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return GenerateInputList(htmlHelper, expression, selectList);
        }

        public static IHtmlContent RadioButtonListFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression, IEnumerable<SelectListItem> selectList)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return GenerateInputList(htmlHelper, ExpressionHelper.GetExpressionText(expression), selectList, type:"radio");
        }

        public static IHtmlContent RadioButtonList(this IHtmlHelper htmlHelper, string expression, IEnumerable<SelectListItem> selectList)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return GenerateInputList(htmlHelper, expression, selectList, type: "radio");
        }

        private static IHtmlContent GenerateInputList(this IHtmlHelper htmlHelper, string expression, IEnumerable<SelectListItem> selectList, string type="checkbox")
        {
            var generator = htmlHelper.ViewContext.HttpContext.RequestServices.GetService<IHtmlGenerator>();

            var modelExplorer = ExpressionMetadataProvider.FromStringExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider);

            var currentValues = generator.GetCurrentValues(htmlHelper.ViewContext, modelExplorer, expression, type == "checkbox");

            var fieldIdPrefix = htmlHelper.Id(expression);
            var counter = 0;

            var content = new HtmlContentBuilder();

            foreach (var selectListItem in selectList)
            {
                var fieldId = fieldIdPrefix + "__" + counter++;

                content.AppendHtml("<div class='form-check'>");

                var input = new TagBuilder("input");
                input.TagRenderMode = TagRenderMode.SelfClosing;
                input.MergeAttribute("id", fieldId);
                input.MergeAttribute("type", type);
                input.MergeAttribute("name", htmlHelper.Name(expression));
                input.MergeAttribute("value", selectListItem.Value);

                var selected = selectListItem.Selected;

                if (currentValues != null)
                {
                    var value = selectListItem.Value ?? selectListItem.Text;
                    selected = currentValues.Contains(value);
                }

                if (selected)
                    input.MergeAttribute("checked", "checked");

                if (selectListItem.Disabled)
                    input.MergeAttribute("disabled", "disabled");

                input.MergeAttribute("class", "form-check-input");

                content
                    .AppendHtml(input)
                    .AppendHtml(string.Format("<label for='{0}' class='form-check-label'>{1}</label>", fieldId, selectListItem.Text))
                    .AppendHtml("</div>");
            }

            return content;
        }
    }
}

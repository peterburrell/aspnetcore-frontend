using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Demo.Web.Framework.HtmlHelpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class ListAttribute : Attribute
    {
        private string SelectList { get; set; }

        protected ListAttribute(string selectList)
        {
            SelectList = selectList;
        }

        public virtual void SetMetadata(DisplayMetadata displayMetadata)
        {
            displayMetadata.AdditionalValues["SelectList"] = SelectList;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CheckboxListAttribute : ListAttribute
    {
        public CheckboxListAttribute(string selectList)
            : base(selectList)
        {
        }

        public override void SetMetadata(DisplayMetadata displayMetadata)
        {
            base.SetMetadata(displayMetadata);
            displayMetadata.TemplateHint = "CheckboxList";
        }
    }
}

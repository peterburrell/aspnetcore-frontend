using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Demo.Web.Framework.HtmlHelpers.Attributes
{
    public interface IDisplayMetadataProviderAttribute
    {
        void SetDisplayMetadata(DisplayMetadata displayMetadata);
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class ListAttribute : Attribute, IDisplayMetadataProviderAttribute
    {
        private string SelectList { get; set; }

        protected ListAttribute(string selectList)
        {
            SelectList = selectList;
        }

        public virtual void SetDisplayMetadata(DisplayMetadata displayMetadata)
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

        public override void SetDisplayMetadata(DisplayMetadata displayMetadata)
        {
            base.SetDisplayMetadata(displayMetadata);
            displayMetadata.TemplateHint = "CheckboxList";
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RadioButtonListAttribute : ListAttribute
    {
        public RadioButtonListAttribute(string selectList)
            : base(selectList)
        {
        }

        public override void SetDisplayMetadata(DisplayMetadata displayMetadata)
        {
            base.SetDisplayMetadata(displayMetadata);
            displayMetadata.TemplateHint = "RadioButtonList";
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DropDownListAttribute : ListAttribute
    {
        private string OptionLabel { get; set; }

        public DropDownListAttribute(string listName, string optionLabel = "")
            : base(listName)
        {
            OptionLabel = optionLabel;
        }

        public override void SetDisplayMetadata(DisplayMetadata displayMetadata)
        {
            base.SetDisplayMetadata(displayMetadata);

            displayMetadata.TemplateHint = "DropDownList";
            displayMetadata.AdditionalValues["OptionLabel"] = OptionLabel;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DisplayInTemplates : Attribute, IDisplayMetadataProviderAttribute
    {
        public bool ShowForDisplay { get; set; }

        public bool ShowForEdit { get; set; }

        public DisplayInTemplates() : this(true)
        {
            
        }

        public DisplayInTemplates(bool show)
        {
            ShowForDisplay = show;
            ShowForEdit = show;
        }

        public void SetDisplayMetadata(DisplayMetadata displayMetadata)
        {
            displayMetadata.ShowForDisplay = ShowForDisplay;
            displayMetadata.ShowForEdit = ShowForEdit;
        }
    }
}

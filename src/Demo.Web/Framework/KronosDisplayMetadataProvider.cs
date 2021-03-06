﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Demo.Web.Framework.HtmlHelpers.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Demo.Web.Framework
{
    public class KronosDisplayMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var propertyAttributes = context.Attributes;
            var displayMetadata = context.DisplayMetadata;
            var propertyName = context.Key.Name;

            if (IsTransformRequired(propertyName, displayMetadata, propertyAttributes))
            {
                displayMetadata.DisplayName = () => Transform(propertyName);
            }

            var attributes = propertyAttributes.OfType<IDisplayMetadataProviderAttribute>();
            foreach (var attribute in attributes)
            {
                attribute.SetDisplayMetadata(context.DisplayMetadata);
            }
        }

        private static bool IsTransformRequired(string propertyName, DisplayMetadata modelMetadata, IReadOnlyList<object> propertyAttributes)
        {
            if (!string.IsNullOrEmpty(modelMetadata.SimpleDisplayProperty))
                return false;

            var displayAttribute = propertyAttributes.OfType<DisplayAttribute>().FirstOrDefault();
            var displayNameAttribute = propertyAttributes.OfType<DisplayNameAttribute>().FirstOrDefault();

            if (displayNameAttribute != null)
                return false;

            if (displayAttribute?.GetName() != null)
                return false;

            if (string.IsNullOrEmpty(propertyName))
                return false;

            return true;
        }

        private static string Transform(string propertyName)
        {
            return Regex.Replace(propertyName, "([A-Z])", " $1", RegexOptions.Compiled).Trim(); ;
        }
    }
}
﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Demo.Web.Framework
{
    public class DisplayNameMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var propertyAttributes = context.Attributes;
            var modelMetadata = context.DisplayMetadata;
            var propertyName = context.Key.Name;

            if (IsTransformRequired(propertyName, modelMetadata, propertyAttributes))
            {
                modelMetadata.DisplayName = () => Transform(propertyName);
            }
        }

        private static bool IsTransformRequired(string propertyName, DisplayMetadata modelMetadata, IReadOnlyList<object> propertyAttributes)
        {
            if (!string.IsNullOrEmpty(modelMetadata.SimpleDisplayProperty))
                return false;

            if (propertyAttributes.OfType<DisplayNameAttribute>().Any())
                return false;

            if (propertyAttributes.OfType<DisplayAttribute>().Any())
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
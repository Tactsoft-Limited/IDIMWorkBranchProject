﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Extentions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
            return attribute?.Name ?? value.ToString();
        }

        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            return from Enum e in Enum.GetValues(enumValue.GetType())
                   select new SelectListItem
                   {
                       Selected = e.Equals(enumValue),
                       Text = e.GetDisplayValue(),
                       Value = Convert.ToInt32(e).ToString()
                   };
        }

        public static string GetDisplayValue(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        private static string LookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }
    }
}
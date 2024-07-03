﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ManageMe.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var result = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()
                            ?.GetCustomAttribute<DisplayAttribute>()
                            ?.GetName();

            return result ?? String.Empty;
        }
    }
}

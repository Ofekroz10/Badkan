using Microsoft.AspNetCore.Http;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public enum UserRollsType
    {
        [Display(Name ="User")]
        User = 0,

        [Display(Name = "Lecturer")]
        Lecturer = 1,

        [Display(Name = "Admin")]
        Admin = 2
    }
}

public static class UserRollsTypeExtentions
{
    public static IEnumerable<UserRollsType> GetOthers(this UserRollsType type)
    {
        var lst = Enum.GetValues(typeof(UserRollsType)).Cast<UserRollsType>().Where(i => i != type);
        return lst;
    }

    public static IEnumerable<UserRollsType> AsCollection()
    {
        var lst = Enum.GetValues(typeof(UserRollsType)).Cast<UserRollsType>();
        return lst;
    }

    public static string IntValueAsString(this UserRollsType item)
    {
        return ((int)(item)).ToString();
    }

    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType().GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DisplayAttribute>()
                       .Name;
    }

    public static bool AdminOrLecturer(ClaimsPrincipal cp)
    {
        return cp.IsInRole(UserRollsType.Admin.IntValueAsString()) ||
               cp.IsInRole(UserRollsType.Lecturer.IntValueAsString());
    }
}
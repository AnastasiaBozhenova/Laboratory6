using System;
using System.Collections.Generic;
using System.Linq;
using Customs.DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Customs.WebAPI.Helpers
{
    public static class SelectLists
    {
        public static List<SelectListItem> GetStoragesSelectedList(this IList<Storage> storages,
            Func<Storage, bool> selectedFunc = null)
        {
            return storages.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = selectedFunc != null && selectedFunc(x)
                }).ToList();
        }

        public static List<SelectListItem> GetProductsSelectedList(this IList<Product> products,
            Func<Product, bool> selectedFunc = null)
        {
            return products.Select(x =>
                new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = selectedFunc != null && selectedFunc(x)
                }).ToList();
        }

        public static List<SelectListItem> GetEmployeesSelectedList(this IList<Employee> employees,
            Func<Employee, bool> selectedFunc = null)
        {
            return employees.Select(x =>
                new SelectListItem
                {
                    Text = $"{x.LastName} {x.FirstName} {x.MiddleName}",
                    Value = x.Id.ToString(),
                    Selected = selectedFunc != null && selectedFunc(x)
                }).ToList();
        }
    }
}
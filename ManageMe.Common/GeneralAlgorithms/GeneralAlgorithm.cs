using ManageMe.Common.Interfaces;
using System.Linq.Expressions;

namespace ManageMe.Common
{
    public class GeneralAlgorithm
    {
        public (string, /*string,*/ IQueryable<TEntity>, double, string) PaginateResults<TEntity>
            (int _perPage, IQueryable<TEntity> databaseItems, Expression<Func<TEntity, object>> orderCriteria,
            string searchQuery, int pageQuery)
            where TEntity : class, IIdentifiable, new()
        {
            var templateObject = new TEntity();

            databaseItems = databaseItems.OrderBy(orderCriteria);

            var returnSearchString = searchQuery;

            int totalItems = databaseItems.Count();

            var currentPage = pageQuery;

            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * _perPage;
            }

            var returnPaginatedRecords = databaseItems.Skip(offset).Take(_perPage);

            var returnLastPage = Math.Ceiling((float)totalItems / (float)_perPage);

            var returnPaginationBaseUrl = "";

            if (returnSearchString != "")
            {
                returnPaginationBaseUrl = $"/{templateObject.GetType().Name}s/Index?search=" + returnSearchString + "&page";
            }
            else
            {
                returnPaginationBaseUrl = $"/{templateObject.GetType().Name}s/Index?page";
            }

            return (returnSearchString, returnPaginatedRecords, returnLastPage, returnPaginationBaseUrl);
        }

        //public void GetModelStateErrors (ModelStateDictionary ModelState)
        //{
        //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        //    {
        //        Console.WriteLine(error.ErrorMessage);
        //    }
        //}
    }
}

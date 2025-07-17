using System;

namespace com.ih.util.generic.Pagination;

public class DatabasePagination
{
    public int Skip { get; set; }
    public int Take { get; set; }
}

public static class PaginationUtil
{
    public static int DefaultItemPerPage = 25;
    
    public static DatabasePagination GetPagination(
        int page,
        int resultsCount)
    {
        var realPage = page <= 0 ? page :  page - 1;

        if (resultsCount < 1)
        {
            resultsCount = DefaultItemPerPage;
        }
        
        return new DatabasePagination()
        {
            Skip = realPage * resultsCount,
            Take = resultsCount
        };
    }

    public static int PageCount(int itemsCount, int itemsPerPage)
    {
        if (itemsCount <= itemsPerPage)
        {
            return 1;
        }

        var totalPages = Math.Ceiling(Convert.ToDouble(itemsCount) / Convert.ToDouble(itemsPerPage));

        return Convert.ToInt32(totalPages);
    }
    
    public static PaginationDataRequest PaginationDataProcess(int page, int itemsPerPage)
    {
        return new PaginationDataRequest()
        {
            Page = page <= 1 ? 1 : page,
            ItemsPerPage = itemsPerPage <= 0 ? DefaultItemPerPage : itemsPerPage
        };
    }
}

public class PaginationDataRequest
{
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
}
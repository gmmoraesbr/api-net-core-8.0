using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application.Common.Pagination
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }
        public int CurrentPage { get; }

        private PaginatedList(List<T> items, int count, int page, int size)
        {
            Items = items;
            TotalItems = count;
            CurrentPage = page;
            TotalPages = (int)Math.Ceiling(count / (double)size);
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int page, int size)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * size).Take(size).ToListAsync();
            return new PaginatedList<T>(items, count, page, size);
        }
    }
}

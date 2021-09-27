using System.Collections.Generic;
using System.Linq;

namespace Assessment
{
    public class PaginationString : IPagination<string>
    {
        private IEnumerable<string> data;
        private readonly int pageSize;
        private int currentPage;

        public PaginationString(string source, int pageSize, IElementsProvider<string> provider)
        {
            data = provider.ProcessData(source);
            currentPage = 1;
            this.pageSize = pageSize;
        }
        public PaginationString(List<int> source, int pageSize, IElementsProvider<string> provider)
        {
            string newSource = string.Join(",", source.ToArray());
            data = provider.ProcessData(newSource);
            currentPage = 1;
            this.pageSize = pageSize;
        }
        public IPagination<string> FirstPage()
        {
            currentPage = 1;
            return this;
        }

        public IPagination<string> GoToPage(int page)
        {
            if(page <= 0 || page > Pages())
            {
                throw new System.InvalidOperationException();
            }
            else
            {
                currentPage = page;
            }
            return this;
        }

        public IPagination<string> LastPage()
        {
            currentPage = Pages();
            return this;
        }

        public IPagination<string> NextPage()
        {
            if (currentPage == Pages())
            {
                throw new System.InvalidOperationException();
            }
            else
            {
                currentPage++;
            }
            return this;
        }

        public IPagination<string> PrevPage()
        {
            if(currentPage == 1)
            {
                throw new System.InvalidOperationException();
            }
            else
            {
                currentPage--;
            }
            return this;
        }

        public IEnumerable<string> GetVisibleItems()
        {
            return data.Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        public int CurrentPage()
        {
            return currentPage;
        }

        public int Pages()
        {
            int numberOfPages = data.Count() / pageSize;
            if (data.Count() % pageSize != 0)
            {
                numberOfPages++;
            }
            return numberOfPages;
        }
        public void SortAsc()
        {
            data = data.OrderBy(s => s);
        }
        public void SortDesc()
        {
            data = data.OrderByDescending(s => s);
        }
    }
}
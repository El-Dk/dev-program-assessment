using System.Collections.Generic;
using System.Linq;
using System;

namespace Assessment
{
    public class PaginationString : IPagination<string>
    {
        private readonly IEnumerable<string> data;
        private readonly int pageSize;
        private int currentPage;

        public PaginationString(string source, int pageSize, IElementsProvider<string> provider)
        {
            data = provider.ProcessData(source);
            currentPage = 1;
            this.pageSize = pageSize;
        }
        public void FirstPage()
        {
            currentPage = 1;
        }

        public void GoToPage(int page)
        {
            if(page <= 0 || page > Pages())
            {
                throw new System.InvalidOperationException();
            }
            else
            {
                currentPage = page;
            }
        }

        public void LastPage()
        {
            currentPage = Pages();
        }

        public void NextPage()
        {
            if (currentPage == Pages())
            {
                throw new System.InvalidOperationException();
            }
            else
            {
                currentPage++;
            }
        }

        public void PrevPage()
        {
            if(currentPage == 1)
            {
                throw new System.InvalidOperationException();
            }
            else
            {
                currentPage--;
            }
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
    }
}
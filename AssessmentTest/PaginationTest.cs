using System.Linq;
using System;
using Assessment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssessmentTest
{
    [TestClass]
    public class PaginationTest
    {
        private const string COMMA_SAMPLE = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        private const string PIPE_SAMPLE = "a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z";
        
        [TestMethod]
        public void TestFirstPage()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.FirstPage();
            string [] expectedElements = {"a", "b", "c", "d", "e"};
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestNextPage()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.NextPage();
            string [] expectedElements = {"f", "g", "h", "i", "j"};
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestPreviousPage()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.LastPage();
            pagination.PrevPage();
            string[] expectedElements = { "u", "v", "w", "x", "y"  };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestLastPage()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.LastPage();
            string [] expectedElements = {"z"};
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestFirstPageWith10PageSize()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 10, provider);
            pagination.FirstPage();
            string [] expectedElements = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j"};
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestLastPageWith10PageSize()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 10, provider);
            pagination.LastPage();
            string[] expectedElements = { "u", "v", "w", "x", "y", "z" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestGoToPageWith10PageSize()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 10, provider);
            pagination.GoToPage(2);
            string[] expectedElements = { "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

         [TestMethod]
        public void TestFirstPageWithPipeSample()
        {
            IElementsProvider<string> provider = new StringProvider("|");
            IPagination<string> pagination = new PaginationString(PIPE_SAMPLE, 5, provider);
            pagination.FirstPage();
            string [] expectedElements = {"a", "b", "c", "d", "e"};
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList()); 
        }
        
        [TestMethod]
        public void TestListOfNumbers()
        {
            IElementsProvider<string> provider = new StringProvider();
            int[] sample = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            IPagination<string> pagination = new PaginationString(sample.ToList(), 5, provider);
            pagination.FirstPage();
            string[] expectedElements = { "1", "2", "3", "4", "5" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }
        
        [TestMethod]
        public void TestListGoToPage()
        {
            IElementsProvider<string> provider = new StringProvider();
            int[] sample = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            IPagination<string> pagination = new PaginationString(sample.ToList(), 4, provider);
            pagination.GoToPage(2);
            string[] expectedElements = { "5", "6", "7", "8" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestListNextPage()
        {
            IElementsProvider<string> provider = new StringProvider();
            int[] sample = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            IPagination<string> pagination = new PaginationString(sample.ToList(), 5, provider);
            pagination.NextPage();
            string[] expectedElements = { "6", "7", "8", "9", "10" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }
        
        [TestMethod]
        public void TestListGetVisibleItems()
        {
            IElementsProvider<string> provider = new StringProvider();
            int[] sample = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            IPagination<string> pagination = new PaginationString(sample.ToList(), 5, provider);
            string[] expectedElements = { "1", "2", "3", "4", "5" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestChainableMethods()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 4, provider);
            pagination.FirstPage().LastPage().GoToPage(3).NextPage().NextPage().PrevPage();
            string[] expectedElements = { "m", "n", "o", "p" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestSortAsc()
        {
            IElementsProvider<string> provider = new StringProvider();
            string sample = "z,x,y,a,d,f,c,e";
            IPagination<string> pagination = new PaginationString(sample, 4, provider);
            pagination.SortAsc();
            string[] expectedElements = { "a", "c", "d", "e" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestSortDesc()
        {
            IElementsProvider<string> provider = new StringProvider();
            string sample = "z,x,y,a,d,f,c,e";
            IPagination<string> pagination = new PaginationString(sample, 4, provider);
            pagination.SortDesc();
            string[] expectedElements = { "z", "y", "x", "f" };
            CollectionAssert.AreEqual(expectedElements, pagination.GetVisibleItems().ToList());
        }

        [TestMethod]
        public void TestPreviousPageException()
        {

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Invalid page number.")]
        public void TestGoToPageException()
        {
            IElementsProvider<string> provider = new StringProvider();
            IPagination<string> pagination = new PaginationString(COMMA_SAMPLE, 5, provider);
            pagination.GoToPage(1000000);
        }
    }
}

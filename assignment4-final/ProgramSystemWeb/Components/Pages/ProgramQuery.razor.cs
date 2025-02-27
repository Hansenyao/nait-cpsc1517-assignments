using Microsoft.AspNetCore.Components;
using ProgramSystemLibrary.BLL;
using PE = ProgramSystemLibrary.Entities;

namespace ProgramSystemWeb.Components.Pages
{
    public partial class ProgramQuery
    {
        private List<string> errorMsgs = [];
        private List<PE.Program> programs = [];
        private string searchName = string.Empty;
        private bool noPrograms = false;
        //
        private int currentPage = 0;
        private int totalPages = 0;
        private const int COUNT_PER_PAGE = 5;

        [Inject]
        ProgramServices programServices { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        // Search programs by inputed program name or portion of the name
        private void SearchProgramByName()
        {
            errorMsgs.Clear();
            noPrograms = false;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(searchName))
            {
                errorMsgs.Add("Please enter a full or partial program name to search.");
            }
            else
            {
                try
                {
                    // Get programs by inputed name
                    programs = programServices.GetPrograms_ByName(searchName.Trim());
                    if (programs.Count == 0)
                    {
                        noPrograms = true;
                    }

                    // Reset current page number and the total pages number
                    currentPage = 0;
                    totalPages = (int)Math.Ceiling((double)programs.Count / (double)COUNT_PER_PAGE);
                }
                catch (Exception ex)
                {
                    errorMsgs.Add(GetInnerException(ex).Message);
                }
            }
        }
        // To display the first page
        private void DispayFirstPage()
        {
            currentPage = 0;
        }
        // To display the previous page
        private void DispayPrevPage()
        {
            currentPage = (currentPage > 0) ? --currentPage : 0;
        }
        // To display the next page
        private void DispayNextPage()
        {
            currentPage = (currentPage < totalPages - 1) ? ++currentPage : totalPages - 1;
        }
        // To display the last page
        private void DispayLastPage()
        {
            currentPage = totalPages - 1;
        }
        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}

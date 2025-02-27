using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using ProgramSystemLibrary.BLL;
using PE = ProgramSystemLibrary.Entities;

namespace ProgramSystemWeb.Components.Pages
{
    public partial class ProgramCRUD
    {
        private string feedback = string.Empty;
        private List<string> errorMsgs = [];

        private EditContext editContext;
        private PE.Program program = new PE.Program();
        private List<PE.School> schools = [];
        private bool isNew = false;

        [Inject]
        private SchoolServices schoolServices { get; set; }
        [Inject]
        private ProgramServices programServices { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        // Page parameter
        [Parameter]
        public int? programId {get; set;}

        protected override void OnInitialized()
        {
            try
            {
                schools = schoolServices.GetAllSchools();

                // Modify a existed item
                if (programId.HasValue)
                {
                    program = programServices.GetPrograms_ByID(programId.Value);
                    if (program == null)
                    {
                        errorMsgs.Add($"The program ID @{programId} does not match any items in the database.");
                        program = new PE.Program();
                        isNew = true;
                    }
                }
                // Add a new item
                else
                {
                    isNew = true;
                }

                editContext = new EditContext(program);
            }
            catch(Exception ex)
            {
                errorMsgs.Add(GetInnerException(ex).Message);
            }

            base.OnInitialized();
        }

        public void OnSubmit()
        {
            // Reset status messages
            feedback = string.Empty;
            errorMsgs.Clear();

            // Validations
            if (program.SchoolCode.IsNullOrEmpty())
            {
                errorMsgs.Add("You must select a school name.");
            }
            if (program.Tuition < 0)
            {
                errorMsgs.Add("Tuition should be greater or equal 0.");
            }
            if (program.InternationalTuition < 0)
            {
                errorMsgs.Add("International Tuition should be greater or equal 0.");
            }

            if (errorMsgs.Count == 0)
            {
                if (isNew)
                {
                    AddProgram();
                }
                else
                {
                    UpdateProgram();
                }
            }
        }
        private void AddProgram()
        {
            try
            {
                int newProgramId = programServices.Programs_Add(program);
                feedback = $"Program {program.ProgramName} (ID: {newProgramId}) has be added to the database.";
                isNew = false;
            }
            catch (Exception ex)
            {
                errorMsgs.Add(GetInnerException(ex).Message);
            }
        }

        private void UpdateProgram()
        {
            try
            {
                int rowAffected = programServices.Programs_Update(program);
                if (rowAffected > 0)
                {
                    feedback = $"Program {program.ProgramName} (ID: {program.ProgramID}) has been successfully updated.";
                }
                else
                {
                    errorMsgs.Add($"Program {program.ProgramName} (ID: {program.ProgramID}) has not been updated. Plese check to see if this program still exists in the database.");
                }
            }
            catch (Exception ex)
            {
                errorMsgs.Add(GetInnerException(ex).Message);
            }
        }

        private async void DeleteProgram()
        {
            // Reset status messages
            feedback = string.Empty;
            errorMsgs.Clear();

            object messageLine = new object[] {$"Are you sure to delete program {program.ProgramName} (ID: {program.ProgramID})?" };
            if (await JSRuntime.InvokeAsync<bool>("confirm", messageLine))
            {
                // Try to delete from database physically
                try
                {
                    int rowAffected = programServices.Programs_PhysicalDelete(program);
                    if (rowAffected == 0)
                    {
                        errorMsgs.Add($"Program {program.ProgramName} (ID: {program.ProgramID}) has not been deleted. Please check to see if it still exists in the database.");
                    }
                    else
                    {
                        await JSRuntime.InvokeAsync<object>("alert", $"Program {program.ProgramName} (ID: {program.ProgramID}) has been successfully deleted.");
                        navigationManager.NavigateTo("/query", true);
                    }
                }
                catch (Exception ex)
                {
                    errorMsgs.Add(GetInnerException(ex).Message);
                }
            }
        }

        private async void ClearForm()
        {
            object[] messageLine = new object[] { "All unsaved data will loss. Are you sure you want to clear the form?" };
            if (await JSRuntime.InvokeAsync<bool>("confirm", messageLine))
            {
                feedback = string.Empty;
                errorMsgs.Clear();
                program = new PE.Program();
                isNew = true;

                // Update elements in UI
                await InvokeAsync(StateHasChanged);
            }
        }

        private async void GoToQuery()
        {
            object[] messageLine = new object[] { "All unsaved data will loss. Are you sure you want to leave this page?" };
            if (await JSRuntime.InvokeAsync<bool>("confirm", messageLine))
            {
                feedback = string.Empty;
                errorMsgs.Clear();
                navigationManager.NavigateTo("/query", true);
            }
        }

        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRegister.ViewModel
{
    public class MaterialDialogViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public MaterialDialogViewModel(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }
    }
}

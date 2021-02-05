using System.Collections.Generic;
using System.Linq;

namespace OnlineUniversity.Domain.Commands
{
    public class CommandResponse
    {
        private List<string> _errors = new List<string>();

        public string Message { get; set; }

        public bool Succeeded
        {
            get
            {
                return !Errors.Any();
            }
        }

        public void AddErrorsRange(IEnumerable<string> errors)
        {
            _errors.AddRange(errors);
        }

        public void AddError(string error)
        {
            _errors.Add(error);
        }

        public IEnumerable<string> Errors 
        {
            get 
            {
                return _errors;
            }        
        }

    }
}

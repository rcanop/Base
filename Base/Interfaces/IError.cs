using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Interfaces
{
    public interface IError
    {
        public bool IsError { get; }    
        public string Messages { get; }
        public string Information { get; }
        public string Errors { get; }
        public void Reset();
        public void AddError(string message);
        public void AddInformation(string message);

    }
}

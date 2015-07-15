using System;

namespace Tyshkavets.FileSplitter
{
    internal class ErrorInformation
    {
        public int ReturnCode { get; set; }
        public String ErrorMessage { get; set; }

        public bool IsValid
        {
            get { return ReturnCode == 0; }
        }
    }
}

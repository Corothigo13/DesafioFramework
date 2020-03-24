using System;
using System.Collections.Generic;
using System.Text;

namespace Models.AbsModels
{
    public class ResultProcessing
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object AdditionalData { get; set; }

        public ResultProcessing()
        {
            Success = false;
            Message = "";
            AdditionalData = null;
        }
    }
}

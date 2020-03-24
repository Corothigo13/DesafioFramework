using System;
using System.Collections.Generic;
using System.Text;

namespace Interface
{
    public interface IBaseModel
    {
        int Id { get; set; }
        DateTime DateCreate { get; set; }
        DateTime DateUpdate { get; set; }
    }
}

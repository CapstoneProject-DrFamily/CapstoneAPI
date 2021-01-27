using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone_API_V2.ViewModels
{
    public class ResourceParameter
    {
        private const int MaxSize = 5;
        public int PageIndex { get; set; } = 1;
        private int _size = MaxSize;
        public int PageSize
        {
            get { return _size; }
            set { _size = (value < MaxSize) ? MaxSize : value; }
        }
        public string SearchValue { get; set; }
        //public int SortBy { get; set; }
    }
}

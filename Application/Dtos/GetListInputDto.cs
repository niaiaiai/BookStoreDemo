﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class GetListInputDto
    {
        public int PageSize { get; set; } = 10;

        public int Index { get; set; } = 0;
    }
}

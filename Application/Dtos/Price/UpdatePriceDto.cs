﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Price
{
    public class UpdatePriceDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
    }
}

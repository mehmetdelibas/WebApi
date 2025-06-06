﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObject
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage = "Title is a reqquired filed")]
        [MinLength(2,ErrorMessage = "Title must consist of at minimum 2 characters")]
        [MaxLength(50,ErrorMessage = "Title must consist of at maximum 50 characters")]
        public String Title { get; init; }

        [Required(ErrorMessage = " Price is a required filed")]
        [Range(10,1000)]
        public decimal Price { get; init; }
    }
}

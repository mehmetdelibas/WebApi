﻿using Entities.DataTransferObject;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookLinks
    {

        LinkResponse TryGenareteLinks(IEnumerable<BookDto> booksDto,
            string fields, HttpContext httpContext);
    }
}

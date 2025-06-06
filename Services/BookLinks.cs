﻿using Entities.DataTransferObject;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Entities.Models;
using Microsoft.Net.Http.Headers;


namespace Services
{
    public class BookLinks : IBookLinks
    {

        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<BookDto> _dataShaper;

        public BookLinks(LinkGenerator linkGenerator,
            IDataShaper<BookDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenareteLinks(IEnumerable<BookDto> booksDto,
            string fields,
            HttpContext httpContext)
        {
            var shapedBooks = ShapedData(booksDto, fields);
            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedBooks(booksDto, fields, httpContext, shapedBooks);

            return ReturnShapedBooks(shapedBooks);
        }

        private LinkResponse ReturnLinkedBooks(IEnumerable<BookDto> booksDto,
            string fields, HttpContext httpContext,
            List<Entity> shapedBooks)
        {
            var bookDtoList = booksDto.ToList();

            for (int index = 0; index < bookDtoList.Count(); index++)
            {
                var bookLinks = CreateForBook(httpContext, bookDtoList[index], fields);
                shapedBooks[index].Add("Links", bookLinks);
            }

            var bookCollection = new LinkCollectionWrapper<Entity>(shapedBooks);
            CreateForBooks(httpContext, bookCollection);
            return new LinkResponse { HasLinks = true, LinkedEntities = bookCollection };
        }

        private LinkCollectionWrapper<Entity> CreateForBooks(HttpContext httpContext,
           LinkCollectionWrapper<Entity> bookCollectionWrapper)
        {
            bookCollectionWrapper.Links.Add(new Link()
            {
                Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                Rel = "self",
                Method = "GET"
            });
            return bookCollectionWrapper;
        }

        private List<Link> CreateForBook(HttpContext httpContext,
            BookDto bookDto,
            string fields)
        {
            var links = new List<Link>()
            {
                new Link()
               {
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}" +
                   $"/{bookDto.Id}",
                   Rel = "self",
                   Method = "GET"
               },
               new Link()
               {
                   Href = $"/api/{httpContext.GetRouteData().Values["controller"].ToString().ToLower()}",
                   Rel="create",
                   Method = "POST"
               },
            };
            return links;
        }

        private LinkResponse ReturnShapedBooks(List<Entity> shapedBooks)
        {
            return new LinkResponse() { ShapedEntities = shapedBooks };
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType
                .SubTypeWithoutSuffix
                .EndsWith("hateos", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapedData(IEnumerable<BookDto> booksDto, string fields)
        {
            return  _dataShaper.
                ShapeData(booksDto, fields)
                .Select(b=>b.Entity)
                .ToList();
        }
    }
}

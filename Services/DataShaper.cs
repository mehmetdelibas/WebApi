﻿using Entities.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            Properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldString)
        {
            var requiredFields = GetRequiredProperties(fieldString);
            return FetchData(entities, requiredFields);
        }

        public ShapedEntity ShapeData(T entity, string fieldString)
        {
            var requiredProperties = GetRequiredProperties(fieldString);
            return FetchDataForEntity(entity, requiredProperties);
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties (string fieldString)
        {
            var requiredFields = new List<PropertyInfo> ();
            if(!string.IsNullOrEmpty(fieldString))
            {
                var fields = fieldString.Split (',',
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var porperty = Properties
                        .FirstOrDefault (pi=>pi.Name.Equals(field.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));
                    if (porperty is null)
                        continue;
                    requiredFields.Add(porperty);
                }
            }
            else
            {
                requiredFields = Properties.ToList();
            }
            return requiredFields;
        }

        private ShapedEntity FetchDataForEntity (T entity,
            IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ShapedEntity();

            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
            }

            var objectProperty = entity.GetType().GetProperty("Id");
            shapedObject.Id = (int)objectProperty.GetValue(entity);

            return shapedObject;
        }
        private IEnumerable<ShapedEntity> FetchData(IEnumerable<T> entities, 
            IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapeData = new List<ShapedEntity>();
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapeData.Add(shapedObject);
            }
            return shapeData;
        }
            
    }
}

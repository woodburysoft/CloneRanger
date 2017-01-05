﻿using System;
using System.Reflection;

namespace CloneRanger
{
    public class Cloner
    {
        public T Clone<T>(T objectToClone) where T: class
        {
            if (objectToClone == null)
            {
                return default(T);
            }

            var clone = Activator.CreateInstance<T>();

            foreach (PropertyInfo property in objectToClone.GetType().GetRuntimeProperties())
            {
                if (property.PropertyType.GetTypeInfo().IsPrimitive
                    || property.PropertyType.IsConstructedGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                    || property.PropertyType == typeof(string))
                {
                    property.SetValue(clone, property.GetValue(objectToClone));
                }
                else
                {
                    MethodInfo method = GetType().GetTypeInfo().GetDeclaredMethod(nameof(Clone)).MakeGenericMethod(property.PropertyType.IsConstructedGenericType ? property.PropertyType.GenericTypeArguments[0] : property.PropertyType);
                    property.SetValue(clone, method.Invoke(this, new[] { property.GetValue(objectToClone) }));
                }
            }

            return clone;
        }        
    }
}

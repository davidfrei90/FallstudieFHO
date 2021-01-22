#region

using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.DomainModel
{
    internal static class Helper
    {
        public static void AssertEmptiness(object item, params object[] propertyNames)
        {
            Type itemType = item.GetType();
            foreach (string propertyName in propertyNames)
            {
                PropertyInfo propertyInfo = itemType.GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    Type propertyType = propertyInfo.PropertyType;
                    if (propertyType == typeof (String))
                    {
                        Assert.AreEqual<String>(string.Empty, (string) propertyInfo.GetValue(item, null), string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                    }
                    else if (propertyType == typeof (int))
                    {
                        Assert.AreEqual<int>(default(int), (int) propertyInfo.GetValue(item, null), string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                    }
                    else if (propertyType == typeof (double))
                    {
                        Assert.AreEqual<double>(default(double), (double) propertyInfo.GetValue(item, null), string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                    }
                    else if (propertyType == typeof(decimal))
                    {
                        Assert.AreEqual<decimal>(default(decimal), (decimal)propertyInfo.GetValue(item, null), string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                    }
                    else if (propertyType == typeof(bool))
                    {
                        Assert.AreEqual<bool>(default(bool), (bool)propertyInfo.GetValue(item, null), string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                    }
                    else if (propertyType == typeof (DateTime?))
                    {
                        Assert.AreEqual<DateTime?>(default(DateTime?), (DateTime?) propertyInfo.GetValue(item, null), string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                    }
                    else if (propertyType == typeof(decimal?))
                    {
                        Assert.AreEqual<decimal?>(default(decimal?), (decimal?)propertyInfo.GetValue(item, null), string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                    }
                    else if (propertyType.IsGenericType)
                    {
                        object obj = propertyInfo.GetValue(item, null);
                        if (obj is IQueryable)
                        {
                            IQueryable list = (IQueryable) propertyInfo.GetValue(item, null);
                            Assert.IsNotNull(list, string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                            Assert.AreEqual<bool>(false, list.GetEnumerator().MoveNext(), string.Format("Assertion exception for property {0}.", propertyInfo.Name));
                        }

                        else
                        {
                            throw new AssertFailedException("Unknown generic type detected.");
                        }
                    }

                    else
                    {
                        throw new AssertFailedException(string.Format("Type {0} not supported in this method.", propertyType.ToString()));
                    }
                }
                else
                    throw new AssertFailedException(string.Format("Property {0} does not exist.", propertyName));
            }
        }

        public static void TestProperty<T>(T value, string propertyName, object item)
        {
            Type itemType = item.GetType();
            PropertyInfo propertyInfo = itemType.GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(item, value, null);
                Assert.AreEqual<T>(value, (T) propertyInfo.GetValue(item, null));
            }
            else
                throw new AssertFailedException(string.Format("Property {0} does not exist.", propertyName));
        }
    }
}
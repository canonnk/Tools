using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools
{
    public class QuickCode
    {
        /// <summary>
        /// 反射缓存(存储反射信息)
        /// </summary>
        public static Dictionary<int, List<PropertyInfo>> propertyInfoCache = new Dictionary<int, List<PropertyInfo>>();

        /// <summary>
        /// 为每一个String字段检查null 并赋值 ""
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TClass"></param>
        public static void CheckNull<T>(T t) where T : class
        {
            var hashCode = t.GetHashCode();
            var properties = new List<PropertyInfo>();
            var isExit = propertyInfoCache.TryGetValue(hashCode, out properties);
            if (!isExit)
            {
                properties = t.GetType().GetProperties().ToList();
                propertyInfoCache.Add(hashCode,properties);
            }
            foreach (var item in properties)
            {
                if (item.PropertyType.Name.Equals("String") && item.GetValue(t) == null)
                {
                    item.SetValue(t, "");
                }
            }
        }
        /// <summary>
        /// 为对象的每个不能为空的字段 检查空并附默认值 
        /// (不缓存反射信息)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void CheckNullNotCache<T>(T t) where T : class
        {
            var hashCode = t.GetHashCode();
            var properties = t.GetType().GetProperties();
            foreach (var item in properties)
            {
                if (item.PropertyType.Name.Equals("String") && item.GetValue(t) == null)
                {
                    item.SetValue(t, "");
                }
            }
        }

        /// <summary>
        /// 为对象的每个字段检查空 并执行回调 func
        /// (不缓存反射信息)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static T2 CheckNull<T,T2>(T t,Func<string,T2> func) where T : class
        {
            var hashCode = t.GetHashCode();
            var properties = t.GetType().GetProperties();
            foreach (var item in properties)
            {
                if (item.GetValue(t) == null)
                {
                    return func(item.Name);
                }
            }
            return default(T2);
        }
    }
}

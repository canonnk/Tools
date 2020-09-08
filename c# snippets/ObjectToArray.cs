using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class ObjectToArray {

        /// <summary>
        /// 吧对象转换为字符串数组
        /// </summary>
        /// <param name="objList">数据</param>
        /// <param name="tpye">类型</param>
        /// <param name="containCode">是否包含Code结尾的字段</param>
        /// <returns></returns>
        public static List<string[]> IEnumerableToListArray(IEnumerable<object> objList, Type tpye, bool containCode = false) {
            List<string[]> result = new List<string[]>( );

            PropertyInfo[] properties = tpye.GetProperties( );
            List<string> tempList;
            object temp;
            foreach (var dataItem in objList) {
                tempList = new List<string>( );

                foreach (var proItem in properties) {
                    temp = proItem.GetValue(dataItem, null);
                    
                    // 如果不需要包含Code字段，则跳过
                    if(!containCode && proItem.Name.EndsWith("Code")) {
                        continue;
                    }

                    if (proItem.PropertyType == typeof(int)) {
                        tempList.Add((int) temp + "");
                    } else if (proItem.PropertyType == typeof(int?)) {
                        tempList.Add(((int?) temp).HasValue ? ((int?) temp).Value + "" : "");
                    } else if (proItem.PropertyType == typeof(string)) {
                        tempList.Add(string.IsNullOrEmpty((string) temp) ? "" : (string) temp);
                    } else if (proItem.PropertyType == typeof(bool)) {
                        tempList.Add((bool) temp ? "是" : "否");
                    } else if (proItem.PropertyType == typeof(bool?)) {
                        tempList.Add(((bool?) temp).HasValue ? ((bool?) temp).Value ? "是" : "否" : "");
                    } else if (proItem.PropertyType == typeof(DateTime)) {
                        tempList.Add(((DateTime) temp).ToString("yyyy-MM-dd"));
                    } else if (proItem.PropertyType == typeof(DateTime?)) {
                        tempList.Add(((DateTime?) temp).HasValue ? ((DateTime?) temp).Value.ToString("yyyy-MM-dd") : "");
                    }
                }

                result.Add(tempList.ToArray( ));
            }

            return result;
        }
    }
}

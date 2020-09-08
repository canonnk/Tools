using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class DataTableHelper {
        /// <summary>
        /// 将DataTable 转换成 List<dynamic>
        /// reverse 反转：控制返回结果中是只存在 FilterField 指定的字段,还是排除.
        /// [flase 返回FilterField 指定的字段]|[true 返回结果剔除 FilterField 指定的字段]
        /// FilterField  字段过滤，FilterField 为空 忽略 reverse 参数；返回DataTable中的全部数
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="reverse">
        /// 反转：控制返回结果中是只存在 FilterField 指定的字段,还是排除.
        /// [flase 返回FilterField 指定的字段]|[true 返回结果剔除 FilterField 指定的字段]
        ///</param>
        /// <param name="FilterField">字段过滤，FilterField 为空 忽略 reverse 参数；返回DataTable中的全部数据</param>
        /// <returns>List<dynamic></returns>
        public static List<dynamic> DataTableToDynamicList(DataTable table, bool reverse = true, params string[] FilterField) {
            var modelList = new List<dynamic>( );
            foreach (DataRow row in table.Rows) {
                dynamic model = new ExpandoObject( );

                var dict = (IDictionary<string, object>) model;
                foreach (DataColumn column in table.Columns) {
                    if (FilterField.Length != 0) {
                        if (reverse == true) {
                            if (!FilterField.Contains(column.ColumnName)) {
                                dict[column.ColumnName] = row[column];
                            }
                        } else {
                            if (FilterField.Contains(column.ColumnName)) {
                                dict[column.ColumnName] = row[column];
                            }
                        }
                    } else {
                        dict[column.ColumnName] = row[column];
                    }
                }
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>
        /// 吧DataSet转为动态的对象列表
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="reverse"></param>
        /// <param name="FilterField"></param>
        /// <returns></returns>
        public static List<List<dynamic>> DataSetToDynamicList(DataSet ds, bool reverse = true, params string[] FilterField) {
            List<List<dynamic>> result = new List<List<dynamic>>( );
            foreach (DataTable item in ds.Tables) {
                result.Add(DataTableToDynamicList(item, reverse, FilterField));
            }
            return result;
        }
    }
}

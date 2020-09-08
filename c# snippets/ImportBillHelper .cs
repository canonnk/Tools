using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OHM.Tools
{
    public class ImportBillHelper
    {
        /// <summary>
        /// 单据导入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">中文名和英文名的键值对</param>
        /// <param name="stream">文件流</param>
        /// <param name="Type">后缀名</param>
        /// <param name="PersonCode">创建人</param>
        /// <returns></returns>
        public static Tuple<List<T>, int> ImportBill<T>(Dictionary<string, string> data, Stream stream, string Type,string PersonCode) where T : new()
        {
            Dictionary<string, string> ColNum = new Dictionary<string, string>();
            Type types = typeof(T);
            List<T> list = new List<T>();
            IWorkbook workbook; //创建工作簿
           //xls使用HSSFWorkbook类实现，xlsx使用XSSFWorkbook类实现
            switch (Type)
            {
                case ".xlsx":
                    workbook = new XSSFWorkbook(stream);//07版
                    break;
                default:
                    workbook = new HSSFWorkbook(stream);//03版
                    break;
            }

            ISheet sheet = workbook.GetSheetAt(0);//获取工作页
            IRow headerRow = sheet.GetRow(0);////获取第一行

            int cellCount = headerRow.LastCellNum;//列数
            int rowCount = sheet.LastRowNum;//行数

            #region 获取列位置
            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                if (cell != null)
                {
                    ColNum.Add(cell.ToString(), j.ToString());
                }
                else
                {
                    ColNum.Add("", j.ToString());
                }
            }
            #endregion

            //验证至少有一行数据
            if (rowCount > 0)
            {
                IDataFormat dataFormat = workbook.CreateDataFormat();
                string returnValues = "";
                for (int i = sheet.FirstRowNum + 1; i <= rowCount; i++)
                {
                    T entity = new T();
                    IRow row = sheet.GetRow(i);
                    if (row != null)
                    {
                        foreach (var colname in data)//英文名
                        {
                            string CnName = "";//中文名
                            if (data.ContainsKey(colname.Key))
                            {
                                CnName = data[colname.Key];
                            }
                            string colnum = "";//中文名的列数

                            if (ColNum.ContainsKey(CnName))
                            {
                                colnum = ColNum[CnName];
                            }
                            if (row.GetCell(int.Parse(colnum)) != null)
                            {
                                switch (row.GetCell(int.Parse(colnum)).CellType)
                                {

                                    case CellType.Unknown:
                                        returnValues = "";
                                        break;
                                    case CellType.Numeric:
                                        //valTemp = cellTemp.NumericCellValue.ToString("0.00");
                                        returnValues = NpoiExcelDateForm.GetInstance().GetDateString(dataFormat.GetFormat(row.GetCell(int.Parse(colnum)).CellStyle.DataFormat), row.GetCell(int.Parse(colnum)));
                                        break;
                                    case CellType.String:
                                        returnValues = row.GetCell(int.Parse(colnum)).StringCellValue;
                                        break;
                                    case CellType.Formula:
                                        returnValues = row.GetCell(int.Parse(colnum)).NumericCellValue.ToString("0.00");
                                        break;
                                    case CellType.Blank:
                                        returnValues = "";
                                        break;
                                    case CellType.Boolean:
                                        returnValues = row.GetCell(int.Parse(colnum)).BooleanCellValue ? "是" : "否";
                                        types.GetProperty(colname.Key).SetValue(entity, returnValues);
                                        break;
                                    case CellType.Error:
                                        break;
                                    default:
                                        returnValues = "";
                                        break;
                                }
                                //是否时间或者bool类型
                                if (types.GetProperty(colname.Key).PropertyType == typeof(DateTime) || types.GetProperty(colname.Key).PropertyType == typeof(bool))
                                {
                                    if (types.GetProperty(colname.Key).PropertyType == typeof(bool))
                                    {
                                        types.GetProperty(colname.Key).SetValue(entity, returnValues=="是"?true:false);
                                    }
                                    else
                                    {
                                        types.GetProperty(colname.Key).SetValue(entity, Convert.ToDateTime(returnValues));
                                    }
                                }
                                else
                                {
                                    types.GetProperty(colname.Key).SetValue(entity, returnValues);
                                }
                            }
                        }

                        types.GetProperty("Creator").SetValue(entity, PersonCode);
                        types.GetProperty("CreationTime").SetValue(entity, DateTime.Now);
                        //如果缺少人员代码，数据不添加
                        if (types.GetProperty("PersonCode").GetValue(entity, null) != null)
                        {
                            list.Add(entity);
                        }
                    }
                }
            }
            return new Tuple<List<T>, int>(list, rowCount);
        }
    }
}





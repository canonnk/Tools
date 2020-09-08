using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class NpoiExcelDateForm {
        private static object _lock = new object( );
        private static NpoiExcelDateForm instance;
        private static Dictionary<string, string> dateFormatNumber;

        private NpoiExcelDateForm( ) {
            // 从webcongfi加载数据
            //string configData = ConfigurationManager.AppSettings["excel_date_format"].ToString( );
            string configData = SystemGlobalConfiguration.GetInstance( )["excel_date_format"].ToString( );
            if ( string.IsNullOrEmpty(configData) ) {
                dateFormatNumber = null;
            }

            try {
                dateFormatNumber = new Dictionary<string, string>( );
                foreach ( var item in configData.Split('&') ) {
                    dateFormatNumber.Add(
                        item.Split('=')[0],
                        item.Split('=')[1]
                    );
                }
            } catch ( Exception ) {
                configData = null;
            }
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        public static NpoiExcelDateForm GetInstance( ) {
            if ( instance == null ) {
                lock ( _lock ) {
                    if ( instance == null ) {
                        instance = new NpoiExcelDateForm( );
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 判断是否为date格式
        /// </summary>
        /// <param name="npoiFormat"></param>
        /// <returns></returns>
        public bool IsDate(string npoiFormat) {
            if( dateFormatNumber == null || dateFormatNumber.Count() <= 0 ) {
                return false;
            }

            return dateFormatNumber.ContainsKey(npoiFormat);
        }

        /// <summary>
        /// 转为时间格式的字符串
        /// </summary>
        /// <param name="formatStr">nopi获取的datafrom的value值</param>
        /// <param name="cell">要判断的单元格</param>
        /// <returns></returns>
        public string GetDateString(string formatStr, ICell cell) {
            if( IsDate(formatStr) ) {
                return cell.DateCellValue.ToString(dateFormatNumber[formatStr]);
            }
            return cell.NumericCellValue.ToString("0.00");
        }
    }
}

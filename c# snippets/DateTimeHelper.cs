using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public static class DateTimeHelper {
        /// <summary>
        /// 获取今天0点的值
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static DateTime ToDateFirstTime(this DateTime _this) {
            return new DateTime(_this.Year, _this.Month, _this.Day, 0, 0, 0);
        }

        /// <summary>
        /// 获取今天23点59分59秒的值
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static DateTime ToDateLastTime(this DateTime _this) {
            return new DateTime(_this.Year, _this.Month, _this.Day, 23, 59, 59);
        }

        /// <summary>
        /// 获取今天0点的值
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static DateTime? ToDateFirstTime(this DateTime? _this) {
            if (_this != null && _this.HasValue) {
                return _this.Value.ToDateFirstTime( );
            }
            return _this;
        }

        /// <summary>
        /// 获取今天23点59分59秒的值
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static DateTime? ToDateLastTime(this DateTime? _this) {
            if(_this!=null && _this.HasValue) {
                return _this.Value.ToDateLastTime( );
            }
            return _this;
        }
    }
}

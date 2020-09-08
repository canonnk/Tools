using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public static class NullableHelper {

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int? HasValueOrDefault(this int? _this, int val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double? HasValueOrDefault(this double? _this, double val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime? HasValueOrDefault(this DateTime? _this, DateTime val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool? HasValueOrDefault(this bool? _this, bool val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal? HasValueOrDefault(this decimal? _this, decimal val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float? HasValueOrDefault(this float? _this, float val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static sbyte? HasValueOrDefault(this sbyte? _this, sbyte val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static short? HasValueOrDefault(this short? _this, short val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static uint? HasValueOrDefault(this uint? _this, uint val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ulong? HasValueOrDefault(this ulong? _this, ulong val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }

        /// <summary>
        /// 如果有值则返回当前值，没有则使用默认值
        /// </summary>
        /// <param name="_this"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ushort? HasValueOrDefault(this ushort? _this, ushort val) {
            if (_this.HasValue) {
                return _this.Value;
            }
            return val;
        }
    }
}

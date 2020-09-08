using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    /// <summary>
    /// 地理位置帮助
    /// </summary>
    public class GeographyHelper {
        // 地球半径（米）
        private const double EARTH_RADIUS = 6378137;
        // 经纬度转换成弧度
        private static double Rad(double d) {
            return (double) d * Math.PI / 180d;
        }

        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lat1">第一点纬度</param>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <param name="lng2">第二点经度</param>
        /// <returns>单位米</returns>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2) {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 判断是否在圆形指定的区域内
        /// </summary>
        /// <param name="areaLatitude">区域纬度</param>
        /// <param name="areaLongitude">区域经度</param>
        /// <param name="areaRadius">区域半径</param>
        /// <param name="positionLatitude">所在位置纬度</param>
        /// <param name="positionLongitude">所在位置经度</param>
        /// <returns></returns>
        public static bool IsOnArea(double areaLatitude, double areaLongitude, double areaRadius, double positionLatitude, double positionLongitude) {
            return GetDistance(areaLatitude, areaLongitude, positionLatitude, positionLongitude) >= areaRadius;
        }

    }
}

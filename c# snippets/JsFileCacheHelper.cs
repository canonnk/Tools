using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class JsFileHelper {
        private static string _CachePara = DateTime.Now.Ticks + "";

        /// <summary>
        /// 用于控制JS文件缓存的参数，每次启动时唯一
        /// 这样既能保证调试时不会取到缓存数据，也能在正式环境运行时使用缓存
        /// </summary>
        public static string CachePara { get { return _CachePara; } }

        /// <summary>
        /// 当为DEBUG时，不输出“.min”，使用非压缩的进行调试js文件
        /// 当不为DEBUG是，输出“.min”压缩空间
        /// </summary>
        public static string UseMinFile {
            get {
#if DEBUG
                return "";
#else
                return ".min";
#endif
            }
        }
    }
}

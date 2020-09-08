using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class SystemGlobalConfiguration {
        private static SystemGlobalConfiguration _instance;
        private static readonly object Locker = new object( );
        private static IDictionary<string, string> configs;

        private SystemGlobalConfiguration( ) {
            LoadData( );
        }


        // 单例
        public static SystemGlobalConfiguration GetInstance( ) {
            if (_instance == null) {
                lock (Locker) {
                    if (_instance == null) {
                        _instance = new SystemGlobalConfiguration( );
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// 从数据库加载配置文件数据
        /// </summary>
        private void LoadData() {
            /*
            ISYS_SystemGlobalConfigurationRepository entity =
                new EFSYS_SystemGlobalConfigurationRepository( );

            configs = entity.ListEntities(p => !p.IsDeleted)
                .ToDictionary(p => p.ConfigCode, p => p.ConfigValue);
            */
            configs = new Dictionary<string, string>( );
            DataSet ds =
                SqlServerHelp.Query("SELECT ConfigCode, ConfigValue FROM SYS_SystemGlobalConfigurations WHERE IsDeleted = 0");
            foreach (DataRow item in ds.Tables[0].Rows) {
                configs.Add(item["ConfigCode"].ToString( ), item["ConfigValue"].ToString( ));
            }
        }

        /// <summary>
        /// 获取对应key的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key = "") {
            if(!configs.ContainsKey(key)) {
                return null;
            }
            return configs[key];
        }

        public string this[string key] {
            get { return Get(key); }
        }


        /// <summary>
        /// 刷新配置数据
        /// </summary>
        /// <returns></returns>
        public bool Refresh( ) {
            LoadData( );
            return true;
        }
    }
}

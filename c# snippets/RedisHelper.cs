using ServiceStack.Redis;
using System;
using System.Configuration;

namespace OHM.Tools {
    public class RedisHelper<T> {
        private static readonly object __locker = new object( );
        private RedisClient client;
        private string keyPrefixes;
        private double expriesAtDay;
        // 获取数据的代理
        private Func<string, T> getDataFunc;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="getDataFunc">获取外部数据的接口</param>
        /// <param name="keyPrefixes">前缀</param>
        /// <param name="expriesAtDay">保存时间</param>
        public RedisHelper(Func<string, T> getDataFunc = null, string keyPrefixes = "", double expriesAtDay = -1) {
            string[] redisConfig = ConfigurationManager.AppSettings["redis_host"].Split(';');
            client = new RedisClient(redisConfig[0], int.Parse(redisConfig[1]), redisConfig[2]);
            this.keyPrefixes = keyPrefixes;
            this.expriesAtDay = expriesAtDay;
            this.getDataFunc = getDataFunc;
            if (redisConfig.Length == 4) {
                client.Db = int.Parse(redisConfig[3]);
            }
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetData(string key, T data) {
            if (expriesAtDay <= 0) {
                client.Set(keyPrefixes + key, data);
            } else {
                client.Set(keyPrefixes + key, data, DateTime.Now.AddDays(expriesAtDay));
            }
        }

        /// <summary>
        /// 获取数据，如果没有在redis则通过外部数据获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetData(string key) {
            lock (__locker) {
                T data = client.Get<T>(keyPrefixes + key);
                if (data == null && getDataFunc != null) {
                    data = getDataFunc(key);
                    if (data != null) {
                        SetData(key, data);
                    }
                    //return default(T);
                    return data;
                }

                return data;
            }
        }

        /// <summary>
        /// 判断是否存在符合条件的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public bool IsHas(string key, Func<T, bool> func) {
            T data = client.Get<T>(keyPrefixes + key);
            if (data == null) {
                return false;
            }
            return func(data);
        }

        /// <summary>
        /// 删除一个数据
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key) {
            client.Remove(keyPrefixes + key);
        }

        /// <summary>
        /// 删除当前前缀的数据
        /// </summary>
        public void ClearWherePrefixes( ) {
            if (!string.IsNullOrEmpty(keyPrefixes)) {
                byte[][] keys = client.Keys(keyPrefixes + "*");
                foreach (byte[] item in keys) {
                    client.Remove(System.Text.Encoding.Default.GetString(item));
                }
            }
        }

        /// <summary>
        /// 删除指定前缀的数据
        /// </summary>
        public void ClearWherePrefixes(string prefixes) {
            if (!string.IsNullOrEmpty(prefixes)) {
                byte[][] keys = client.Keys(prefixes + "*");
                foreach (byte[] item in keys) {
                    client.Remove(System.Text.Encoding.Default.GetString(item));
                }
            }
        }

        /// <summary>
        /// 删除整个redis数据库
        /// </summary>
        public void ClearDB( ) {
            client.FlushDb( );
        }

        /// <summary>
        /// 清除整个redis下所有的数据库
        /// </summary>
        public void ClearAllDB( ) {
            client.FlushAll( );
        }
    }
}

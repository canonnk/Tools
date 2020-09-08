using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHM.Tools {
    public class CustonEqualityComparer<T> : IEqualityComparer<T> {
        public delegate bool EqualsDelegate(T x, T y);
        public event EqualsDelegate equalsComparer = null;

        public delegate int GetHashCodeDelegate(T obj);
        public event GetHashCodeDelegate getHashCodeComparer = null;

        public CustonEqualityComparer(
            EqualsDelegate e = null,
            GetHashCodeDelegate g = null) {
            if (e != null) {
                equalsComparer += e;
            }
            if(g != null) {
                getHashCodeComparer += g;
            }
        }

        public bool Equals(T x, T y) {
            if(equalsComparer != null) {
                return equalsComparer(x, y);
            }
            return x.Equals(y);
        }

        public int GetHashCode(T obj) {
            if (getHashCodeComparer != null) {
                return getHashCodeComparer(obj);
            }
            return obj.GetHashCode( );
        }
    }
}

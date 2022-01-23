using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFynd.Application.Contracts
{
    public interface IDataReader<T> where T : class
    {
        public Task<IList<T>> LoadAsync(string path);
    }
}

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SMS.Shared.DAL
{
    public interface IDataAccess
    {
        Task ExecuteACommand<T>(
            string sqlStatement,
            T parameters,
            string connectionString,
            CommandType commandType = CommandType.Text);
        Task<IEnumerable<T>> RunAQuery<T, U>(
            string sqlStatement,
            U parameters,
            string connectionString,
            CommandType commandType = CommandType.Text);
    }
}
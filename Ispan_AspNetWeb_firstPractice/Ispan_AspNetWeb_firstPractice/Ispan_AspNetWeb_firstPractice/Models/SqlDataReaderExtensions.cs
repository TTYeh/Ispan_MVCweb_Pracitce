using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Ispan_AspNetWeb_firstPractice.Models
{
    public static class SqlDataReaderExtensions
    {
        public static T GetFieldValue<T>(this SqlDataReader source, string columnName)
    => source[columnName].Equals(DBNull.Value)
        ? default(T)
        : (T)source[columnName];

    }
}
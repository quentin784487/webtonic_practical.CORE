using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DAL.DataModel
{
    public abstract class Model
    {
        public long Id { get; set; }
        public abstract void Populate(DbDataReader reader);

        protected bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        protected long GetLong(DbDataReader reader, string columnName, long defaultValue)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    object val = reader[ordinal];
                    return Convert.ToInt64(val);
                }
            }

            return defaultValue;
        }

        protected double GetDouble(DbDataReader reader, string columnName, double defaultValue)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    return reader.GetDouble(ordinal);
                }
            }

            return defaultValue;
        }

        protected int GetInt(DbDataReader reader, string columnName, int defaultValue)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    return reader.GetInt32(ordinal);
                }
            }

            return defaultValue;
        }

        protected double GetFloat(DbDataReader reader, string columnName, float defaultValue)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    return reader.GetDouble(ordinal);
                }
            }
            return defaultValue;
        }

        protected bool GetBoolean(DbDataReader reader, string columnName, bool defaultValue)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    return reader.GetBoolean(ordinal);
                }
            }

            return defaultValue;
        }

        protected bool? GetBoolean(DbDataReader reader, string columnName, bool? defaultValue)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    return reader.GetBoolean(ordinal);
                }
            }

            return defaultValue;
        }

        protected int GetNullableBoolean(DbDataReader reader, string columnName)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    bool value = reader.GetBoolean(ordinal);
                    return value ? 1 : 0;
                }
            }

            return -1;
        }

        protected string GetString(DbDataReader reader, string columnName, string defaultValue)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    return reader.GetString(ordinal);
                }
            }

            return defaultValue;
        }

        protected DateTime? GetDateTime(DbDataReader reader, string columnName)
        {
            if (HasColumn(reader, columnName))
            {
                int ordinal = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(ordinal))
                {
                    return reader.GetDateTime(ordinal);
                }
            }

            return null;
        }
    }
}

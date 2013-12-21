using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// SqlHelper 的摘要说明
/// </summary>
public class SqlHelper
{
	public SqlHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //获取连接字符串
    private static string connStr = ConfigurationManager.ConnectionStrings["dbConnectString"].ConnectionString;


    /// <summary>
    /// 返回受影响的行数
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns>执行 sql 语句之后受影响的行数</returns>
    public static int ExecuteNonQuery(string sql,params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// 执行查询，并返回结果集中的第一行第一列
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns>执行 sql 查询语句得到的结果集的第 1 行第 1 列结果</returns>
    public static object ExecuteScalar(string sql,params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }
    }


    /// <summary>
    /// 将结果放入 Dataset 中（DataTable 集），用来查询结果数量小的情况
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns>执行 sql 查询语句的结果集（DataTable 集合）</returns>
    public static DataSet ExecuteDataSet(string sql,params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
                return dataset;
            }
        }
    }

    /// <summary>
    /// 将结果集填入一张表中
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns>执行 sql 查询语句的结果（DataTable ）</returns>
    
    public static DataTable ExecuteDataTable(string sql,params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                return dataset.Tables[0];
            }
        }
    }
    #region  抽象类，用于解决数据库中的NULL问题

    public static object FromDBValue(object value)
    {
        if (value == DBNull.Value)
        {
            return null;
        }
        else
        {
            return value;
        }
    }
    public static object ToDBValue(object value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }
        else
        {
            return value;
        }
    }
    #endregion
}
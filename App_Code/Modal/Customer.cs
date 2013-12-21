using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Customer 的摘要说明
/// </summary>
public class Customer
{
	public Customer()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public System.Guid CustID { get; set; }
    public System.String CustName { get; set; }
    public System.String CustPassword { get; set; }
    public System.String CustPhone { get; set; }
    public System.Boolean CustIsDeleted { get; set; }
}
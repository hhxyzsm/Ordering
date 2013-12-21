using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Store 的摘要说明
/// </summary>
public class Store
{
	public Store()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public System.Guid StoreID { get; set; }
    public System.String StoreName { get; set; }
    public System.String StorePassword { get; set; }
    public System.Boolean StoreIsDeleted { get; set; }
    public System.Guid StoreBusinessID { get; set; }
}
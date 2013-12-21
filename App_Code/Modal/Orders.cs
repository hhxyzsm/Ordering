using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Orders 的摘要说明
/// </summary>
public class Orders
{
	public Orders()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }
    public System.Guid OrdersID { get; set; }
    public System.Guid OrdersBusinessID { get; set; }
    public System.Guid OrdersCustID { get; set; }
    public System.Guid OrdersFoodInfoID { get; set; }
    public System.DateTime OrdersData { get; set; }
    public System.Int32 OrdersNum { get; set; }
    public System.Boolean OrdersIsdeleted { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// FoodKind 的摘要说明
/// </summary>
public class FoodKind
{
	public FoodKind()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public System.Guid KindID { get; set; }
    public System.String KindName { get; set; }
    public System.Guid BusinessID { get; set; }
    public System.Boolean IsDeleted { get; set; }
}
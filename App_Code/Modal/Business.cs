using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Business 的摘要说明
/// </summary>
public class Business
{
	public Business()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public System.Guid BusinessID { get; set; }
    public System.String BusinessName { get; set; }
    public System.Byte[] BusinessImage { get; set; }
    public System.Double? BusinessGrade { get; set; }
    public System.Decimal? BusinessAvePay { get; set; }
    public System.String BusinessStyle { get; set; }
    public System.String BusinessAddress { get; set; }
    public System.String BusinessPhone { get; set; }
    public System.String BusinessDescribe { get; set; }
    public System.String BusinessRemind { get; set; }
    public System.Boolean BusinessIsdeleted { get; set; }
}
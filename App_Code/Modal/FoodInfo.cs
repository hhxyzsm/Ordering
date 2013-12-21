using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// FoodInfo 的摘要说明
/// </summary>
public class FoodInfo
{
	public FoodInfo()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public System.Guid FoodID { get; set; }
    public System.Guid FoodBusinessID { get; set; }
    public System.String FoodName { get; set; }
    public System.String FoodImgURL { get; set; }
    public System.Byte[] FoodImage { get; set; }
    public System.Int32 FoodPopularIndex { get; set; }
    public System.Int32 FoodPrice { get; set; }
    public System.String FoodKind { get; set; }
    public System.String FoodTaste { get; set; }
    public System.Boolean FoodIsdeleted { get; set; }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AccountManagementBLL 的摘要说明
/// </summary>
public class AccountManagementBLL
{
	public AccountManagementBLL()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static void Update(Store store)
    {
        new StoreDAL().Update(store);
    }
}
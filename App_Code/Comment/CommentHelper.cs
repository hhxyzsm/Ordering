using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using NVelocity.App;
using NVelocity.Runtime;

/// <summary>
/// CommentHelper 的摘要说明
/// </summary>
public class CommentHelper
{
	public CommentHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// Md5 加密算法
    /// </summary>
    /// <param name="sDataIn">需要加密的内容</param>
    /// <returns>加密后的结果</returns>
    public static string GetMD5(string sDataIn)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] bytValue, bytHash;
        bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
        bytHash = md5.ComputeHash(bytValue);
        md5.Clear();
        string sTemp = "";
        for (int i = 0; i < bytHash.Length; i++)
        {
            sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
        }
        return sTemp.ToLower();
    }


    /// <summary>
    /// 用 data 来渲染 templateName 页面
    /// </summary>
    /// <param name="templateName"></param>
    /// <param name="data"></param>
    /// <returns>被渲染的页面的 html 代码</returns>
    public static string RenderHtml(string templateName,object data)
    {
        VelocityEngine vltEngine = new VelocityEngine();
        vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
        vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH,
            System.Web.Hosting.HostingEnvironment.MapPath("~/Templates"));//模板文件所在的文件夹
        vltEngine.Init();

        NVelocity.VelocityContext vltContext = new NVelocity.VelocityContext();
        //设置参数，在模板中可以通过$data来引用

        vltContext.Put("data", data);

        NVelocity.Template vltTemplate = vltEngine.GetTemplate(templateName);
        System.IO.StringWriter vltWriter = new System.IO.StringWriter();
        vltTemplate.Merge(vltContext, vltWriter);

        string html = vltWriter.GetStringBuilder().ToString();
        return html;
    }
}
using Java2Dotnet.Spider.Extension.Configuration;
using Java2Dotnet.Spider.Extension.Configuration.Json;
using Newtonsoft.Json;
using System;

namespace Java2Dotnet.Spider.Extension
{
	public abstract class SpiderBuilder
	{
		protected virtual Action AfterSpiderFinished { get; set; }

		protected abstract SpiderContext GetSpiderContext();

		public void Run(params string[] args)
		{
			var context = GetSpiderContext();
			if (context != null)
			{
				if (context.Scheduler == null)
				{
					context.Scheduler = new QueueScheduler();
				}
#if Test
	// ת��JSON��ת����SpiderContext, ���ڲ���JsonSpiderContext�Ƿ�����
			string json = JsonConvert.SerializeObject(GetSpiderContext());
			ModelSpider spider = new ModelSpider(JsonConvert.DeserializeObject<JsonSpiderContext>(json).ToRuntimeContext());
#elif Publish
			ModelSpider spider = new ModelSpider(context);
#endif
				spider.AfterSpiderFinished = AfterSpiderFinished;
				spider.Run(args);
			}
		}
	}
}
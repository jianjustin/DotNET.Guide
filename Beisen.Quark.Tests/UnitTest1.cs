using Beisen.Common.Context;
using Beisen.MultiTenant.ServiceInterface;
using Beisen.Quark.QuarkClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Beisen.Quark.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetFormComponentV2_BusinessDataTest()
        {
            ApplicationContext.Current.TenantId = 410068;
            ApplicationContext.Current.ApplicationName = "BusinessDataTest";
            ApplicationContext.Current.UserId = 460000899;

            string jsonStr = "{\"metaFields\":[{\"name\":\"text1\",\"text\":\"fdfd7777\",\"value\":\"fdfd7777\",\"metaObjName\":\"BusinessDataTest.danhangwenben\"},{\"name\":\"text3\",\"text\":\"dadada\",\"value\":\"dadada\",\"metaObjName\":\"BusinessDataTest.danhangwenben\"},{\"name\":\"text6\",\"text\":\"大大擦发放\",\"value\":\"大大擦发放\",\"metaObjName\":\"BusinessDataTest.danhangwenben\"},{\"name\":\"SystemAccessSign\",\"text\":\"\",\"value\":\"8B757254B3A98F98B2F6B613F0A3C792\",\"metaObjName\":\"BusinessDataTest.danhangwenben\"}],\"viewName\":\"BusinessDataTest.textForm\",\"checkRequired\":false,\"formState\":\"edit\"}";

            var objectData = Newtonsoft.Json.JsonConvert.DeserializeObject<Beisen.MultiTenant.Model.ObjectDataFromJson>(jsonStr);
            objectData.id = Guid.Parse("4f10976f-0bbf-4585-bab8-219448c36046");

            var UserInterfaceV2Provider = QuarkClient.QuarkClientProxy<IUserInterfaceV2Provider>
                .GetInstance("MultiTenant_UI")
                .WithTimeoutPolicy(millisecondsTimeout: 5000)//设置超时策略
                .WithRetryPolicy(retryTimes: 3);//设置重试策略

            var result = UserInterfaceV2Provider.CheckPageAccessSignIsSecurity(
                ApplicationContext.Current.ApplicationName,
                ApplicationContext.Current.TenantId,
                ApplicationContext.Current.UserId,
                objectData);

            Assert.IsTrue(result);
        }
    }
}

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunFor591.CrawlerUtility;
using RunFor591.Entity;

namespace UnitTest
{
    [TestClass]
    public class GenerateUrlTest
    {
        [TestMethod]
        public void OnlySelectRegion_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add("1");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);
            Assert.IsTrue(result.First().txt== "台北市");

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count>1);
        }
        
        //選取多個region(縣市)

        [TestMethod]
        public void SelectRegionAndSection_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add("1");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count > 0);
        }

        [TestMethod]
        public void SelectRegionAndMrtLine_Expect_Exception()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add("1");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count > 0);
        }

        [TestMethod]
        public void SelectRegionAndMrtLineAndMrtCoods_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add("1");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count > 0);
        }

        [TestMethod]
        public void NoSelectRegion_Expect_Exception()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add("1");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count > 0);
        }

        [TestMethod]
        //測試跨縣市的鄉鎮
        public void CrossRegionSection_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add("1");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count > 0);
        }

        [TestMethod]
        //測試跨縣市的捷運線
        public void CrossRegionMrtLine_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add("1");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count > 0);
        }

        [TestMethod]
        //測試跨捷運線的捷運站
        public void CrossMrtLineStation_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add("1");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count > 0);
        }

        [TestMethod]
        //只有選擇捷運站 沒選捷運線
        public void OnlyChooseMrtCoods()
        {
            var searchCondition = new RegionCondition();
            //南港展覽館
            searchCondition.mrtcoods.Add("4257");
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
            Assert.IsTrue(urlResult.Count > 0);
        }


    }
}

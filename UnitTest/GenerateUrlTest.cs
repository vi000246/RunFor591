﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunFor591.Common;
using RunFor591.CrawlerUtility;
using RunFor591.Entity;

namespace UnitTest
{
    [TestClass]
    public class GenerateUrlTest
    {
        [TestMethod]
        //只選一個縣市
        public void OnlySelectRegion_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            searchCondition.Region.Add(Utility.GetLocationIdByName("台北市")); 
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);
            Assert.IsTrue(result.First().txt== "台北市");

//            var urlResult = generator.GenerateUrlBySearchModel(searchCondition);
//            Assert.IsTrue(urlResult.Count>1);
        }

       
        [TestMethod]
        //選取多個region(縣市)
        public void SelectMultiRegion_Expect_Success()
        {
            var searchCondition = new RegionCondition();
           
            searchCondition.Region.Add(Utility.GetLocationIdByName("台北市"));
            searchCondition.Region.Add(Utility.GetLocationIdByName("新北市"));
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        //選取縣市及其底下的鄉鎮
        public void SelectRegionAndSection_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            
            searchCondition.Region.Add(Utility.GetLocationIdByName("台北市"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("大安區"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("萬華區"));
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.First().section.Count == 2);
        }

        [TestMethod]
        //選取多個縣市及底下的鄉鎮
        public void SelectRegionsAndSection_Expect_Success()
        {
            var searchCondition = new RegionCondition();

            searchCondition.Region.Add(Utility.GetLocationIdByName("高雄市"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("鼓山區"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("楠梓區"));

            searchCondition.Region.Add(Utility.GetLocationIdByName("南投縣"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("埔里鎮"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("中寮鄉"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("仁愛鄉"));
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.First(x => x.txt=="高雄市").section.Count == 2);
            Assert.IsTrue(result.First(x => x.txt== "南投縣").section.Count == 3);
        }


        [TestMethod]
        public void SelectRegionAndMrtLineAndMrtCoods_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            searchCondition.Region.Add(Utility.GetLocationIdByName("高雄市"));
            //紅線底下的捷運站
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("高雄國際機場"));
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("凱旋"));
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("三多商圈"));
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);
            Assert.IsTrue(result.First().mrt.mrtline.First().station.Count == 3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoSelectRegion_Expect_Exception()
        {
            var searchCondition = new RegionCondition();
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);
        }

        [TestMethod]
        //如果鄉鎮不在該縣市底下，略過
        public void CrossRegionSection_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            searchCondition.Region.Add(Utility.GetLocationIdByName("高雄市"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("安平區"));
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First(x=>x.txt == "高雄市").section.Count == 0);
        }

        [TestMethod]
        //一個縣市全選，一個只選特定鄉鎮
        public void CrossRegionAndSelectAllRegion_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            searchCondition.Region.Add(Utility.GetLocationIdByName("台北市"));
            searchCondition.Region.Add(Utility.GetLocationIdByName("高雄市"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("左營區"));
            searchCondition.Section.Add(Utility.GetLocationIdByName("鳥松區"));
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.First(x => x.txt == "高雄市").section.Count == 2);
            Assert.IsTrue(result.First(x => x.txt == "台北市").section.Count == 0);
        }

        [TestMethod]
        //捷運線不在縣市底下, 會略過
        public void CrossRegionMrtLine_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            searchCondition.Region.Add(Utility.GetLocationIdByName("台北市"));
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("凱旋"));
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("三多商圈"));
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().mrt == null);
        }

        [TestMethod]
        //測試跨捷運線的捷運站
        public void CrossMrtLineStation_Expect_Success()
        {
            var searchCondition = new RegionCondition();
            //台北市
            searchCondition.Region.Add(Utility.GetLocationIdByName("台北市"));
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("南港展覽館"));
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("大湖公園"));
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("台北小巨蛋"));

            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.First().mrt.mrtline.First(x=>x.name=="文湖線").station.Count == 2);
            Assert.IsTrue(result.First().mrt.mrtline.First(x=>x.name== "松山新店線").station.Count == 1);
        }

        [TestMethod]
        //只有選擇捷運站
        public void OnlyChooseMrtCoods()
        {
            var searchCondition = new RegionCondition();
            searchCondition.Region.Add(Utility.GetLocationIdByName("高雄市"));
            searchCondition.mrtcoods.Add(Utility.GetMrtIdByName("鳳山國中"));
            var generator = new UrlGenerator();
            var result = generator.ConvertFilterConditionToRegionList(searchCondition);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First(x=>x.txt=="高雄市").mrt.mrtline.First().station.First().name == "鳳山國中");
        }


    }
}
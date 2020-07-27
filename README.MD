* ### Description:  

此為window service專案，使用simpleService lib，https://github.com/davidwhitney/SimpleServices  

--------  

* #### Install:  
RunFor591.exe /i or RunFor591.exe /install  

* #### UnInstall:  
RunFor591.exe /u or RunFor591.exe /uninstall  

--------  
* ### Build & Deploy:  
如果要用console application執行  
需要到專案屬性頁將output type選成console application  

---------  
* ### Line Notify設定方法:  
參考保哥這篇 上手 LINE Notify 不求人：一行代碼都不用寫的推播通知方法  
https://blog.miniasp.com/post/2020/02/17/Go-Through-LINE-Notify-Without-Any-Code  
1.先在本地架好callback url用的伺服器，連接到同意授權頁面，按下同意後，會向callback url發出post請求  
2.這時call back url的伺服器接受的post請求會有code參數  
3.將拿到的code參數，跟註冊的client id、client secret向 https://notify-bot.line.me/oauth/token 發出post請求  
4.就能拿到access token了  

呼叫notify api的方式:  
參考Notification這段  
https://notify-bot.line.me/doc/en/  

---------  
* ### Demo:
![Sample](https://github.com/vi000246/RunFor591/blob/master/Demo/demo.png?raw=true)
---------

* ### 591 api analyze:  
捷運列表  
https://s.591.com.tw/build/widget/config/_subway.js?v=a6d45d1200  
縣市列表
https://s.591.com.tw/build/widget/config/_region_rent.js?v=3f96c4eb44  

這些列表的解法  
縣市鄉鎮  
https://jsfiddle.net/ca271b8f/1/  
捷運火車  
https://jsfiddle.net/puwea23b/  


---------  
* ### 使用lib列表:
AutoFac  
LineNotify  
JsonConfig  
RestSharp  
UnitTest

---------  
* ### 其他:  
安裝的時候可能需要將專案output type選成windows應用程式  
todo:  
啟動service時，沒辨法寫log到logfile  

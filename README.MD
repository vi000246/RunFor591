*Description:  

此為window service專案，使用simpleService lib，https://github.com/davidwhitney/SimpleServices  

--------  

*Install:  
RunFor591.exe /i or RunFor591.exe /install  

*UnInstall:  
RunFor591.exe /u or RunFor591.exe /uninstall  

--------  
*Build & Deploy:  
如果要用console application執行  
需要到專案屬性頁將output type選成console application  

---------
591 api analyze:  
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
其他:  
安裝的時候可能需要將專案output type選成windows應用程式  
todo:  
啟動service時，沒辨法寫log到logfile  

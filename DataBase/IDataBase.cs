using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunFor591
{
    interface IDataBase
    {
        //依據日期，取得db資料
        void GetDataByDate(DateTime date);

        //如果日期不存在，create一個key
        void CreateDataByDate();

        //如果物件不存在，在該日期新增一個houseId
        void InsertHouseData();

        //刪除該日期資料
        void DeleteByDate();

        //更新該日期資料
        void UpdateByDate();
    }
}

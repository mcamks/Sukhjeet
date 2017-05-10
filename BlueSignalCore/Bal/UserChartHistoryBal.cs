using BlueSignalCore.Models;
using BlueSignalCore.Repository;
using System;
using System.Collections.Generic;

namespace BlueSignalCore.Bal
{
    public  class UserChartHistoryBal :BaseBal
    {
        private readonly IRepository<UserChartHistory> _usersChatRep;

        public UserChartHistoryBal(IRepository<UserChartHistory> usersChatRep)
        {
            _usersChatRep = usersChatRep;
        }


        public IEnumerable<UserChartHistory> GetUserChartHistory(long userId, string chartType)
        {
           var result= _usersChatRep.Where(x => x.UserId == userId && x.ChartType == chartType);
            return result;
        }
        public void SaveUserChartHistory(string symbol, long userId , string chartType)
        {
            var model =new  UserChartHistory();
            model.Symbol = symbol;
            model.UserId = userId;
            model.CreatedBy =Convert.ToInt32(userId);
            model.CreatedDate = DateTime.Now;
            model.ChartType = chartType;
            _usersChatRep.Insert(model);
             _usersChatRep.SaveChanges();
        }

        public void UpdateChartHistory(UserChartHistory model)
        {
            _usersChatRep.Update(model);
            _usersChatRep.SaveChanges();
        }
        
    }
}

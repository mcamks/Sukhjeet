using BlueSignalCore.Models;
using BlueSignalCore.Repository;
using System;
using System.Collections.Generic;

namespace BlueSignalCore.Bal
{
    public  class GlobalCodesBal : BaseBal
    {
        private readonly IRepository<GlobalCodes> _globalCodes;

        public GlobalCodesBal(IRepository<GlobalCodes> usersChatRep)
        {
            _globalCodes = usersChatRep;
        }


        public IEnumerable<GlobalCodes> GetGlobalCodesValue(string gCategoryValue)
        {
           var result= _globalCodes.Where(x => x.GlobalCodeCategoryValue == gCategoryValue);
            return result;
        }

        public IEnumerable< GlobalCodes> GetGlobalCodes(long gcId)
        {
            var result = _globalCodes.Where(x => x.GlobalCodeID == gcId);
            return result;
        }

        public long UpdateGlobalCodes(GlobalCodes model)
        {

             _globalCodes.Update(model);
            _globalCodes.SaveChanges();
         return   Convert.ToInt64(model.GlobalCodeID);
        }
       
        
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.ih.util.generic.Enums.Domain;
using com.ih.util.generic.Errors;

namespace com.ih.util.generic.Enums.Service
{
    public class EnumService : IEnumService
    {
        public async Task<List<EnumModel>> GetList<T>(bool getAll = false) where T : System.Enum
        {
            try
            {
                var list = new List<EnumModel>();

                await Task.Run(() =>
                {
                    list = EnumUtil.GetValuesEnum<T>();

                    if (list.Count > 0)
                    {
                        if (!getAll)
                        {
                            list = list.Where(x => x.Index > 0).ToList();
                        }

                        list = list.OrderBy(x => x.Index).ToList();
                    }
                });

                return list;
            }
            catch (MapperErrorCustomError ex)
            {
                throw ex;
            }
        }
    }
}
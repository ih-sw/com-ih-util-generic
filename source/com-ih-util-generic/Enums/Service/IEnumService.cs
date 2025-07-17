using System.Collections.Generic;
using System.Threading.Tasks;
using com.ih.util.generic.Enums.Domain;

namespace com.ih.util.generic.Enums.Service
{
    public interface IEnumService
    {
        Task<List<EnumModel>> GetList<T>(bool getAll = false) where T : System.Enum;
    }
}
using Medik.Core.ViewModel;
using System.Threading.Tasks;

namespace Medik.Infrastructure.RecordRepository
{
    public interface IRecordRepository
    {
        Task<StatViewModel> GetAllRecords();
    }
}
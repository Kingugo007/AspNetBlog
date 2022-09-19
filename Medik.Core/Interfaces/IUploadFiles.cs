using Medik.Core.ViewModel;

namespace Medik.Core.Interfaces
{
    public interface IUploadFiles
    {
        string Upload(PostViewModel contents);
    }
}
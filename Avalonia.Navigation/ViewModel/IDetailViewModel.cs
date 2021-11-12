using System.Threading.Tasks;

namespace Avalonia.Navigation.ViewModel
{
    public interface IDetailViewModel
    {
        void LoadAsync(int id);
        bool IsChanged { get; }
        bool IsValid { get; }
        int Id { get; }
        string Title { get; }
    }
}
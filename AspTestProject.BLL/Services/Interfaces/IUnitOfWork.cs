using AspTestProject.DAL.Infrastructure;

namespace AspTestProject.BLL.Services.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
}
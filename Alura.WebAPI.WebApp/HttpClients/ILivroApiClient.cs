using Alura.ListaLeitura.Modelos;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.HttpClients
{
    public interface ILivroApiClient
    {
        Task<byte[]> GetCapaLivrosync(int id);
        Task<LivroApi> GetLivroAsync(int id);
    }
}
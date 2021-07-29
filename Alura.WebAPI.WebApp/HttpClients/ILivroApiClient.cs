using Alura.ListaLeitura.Modelos;
using System.Threading.Tasks;
using Lista = Alura.ListaLeitura.Modelos.ListaLeitura;

namespace Alura.ListaLeitura.HttpClients
{
    public interface ILivroApiClient
    {
        Task DeleteLivroAsync(int id);
        Task<byte[]> GetCapaLivrosync(int id);
        Task<LivroApi> GetLivroAsync(int id);
        Task<Lista> GetListaLeituraAsync(TipoListaLeitura tipo);
    }
}
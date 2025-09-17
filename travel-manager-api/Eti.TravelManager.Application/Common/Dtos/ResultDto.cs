namespace Eti.TravelManager.Application.Common.Dtos;

public class ResultDto<T>
{
    public bool Sucesso { get; set; }
    public List<string> Mensagens { get; set; } = [];
    public T? Dados { get; set; }

    public static ResultDto<T> ComSucesso(T dados)
    {
        return new ResultDto<T>()
        {
            Sucesso = true,
            Dados = dados,
        };
    }

    public static ResultDto<T> ComErro(List<string>? mensagens)
    {
        return new ResultDto<T>()
        {
            Sucesso = false,
            Mensagens = mensagens ?? []
        };
    }
}
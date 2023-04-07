
using System;
using System.Net;
using System.Reflection.Metadata;
using System.Text;

Console.WriteLine("Digite sua KEY: ");
var key = Console.ReadLine();

ExibirOpc();

var url = "https://app24-api2.ploomes.com/Contacts";

var opc = Console.ReadLine();

bool exibirMenu = true;

string msgSaida = "Para voltar ao MENU digite 'm'";

using var client = new HttpClient();

while (exibirMenu)
{

    switch (opc)
    {
        case "1":
            try
            {

                var msg = new HttpRequestMessage(HttpMethod.Get, url);
                msg.Headers.Add($"User-Key", key);
                var res = await client.SendAsync(msg);

                var content = await res.Content.ReadAsStringAsync();

                StreamWriter sw = new StreamWriter("C:\\Users\\CaioLucas\\Workspace\\ApiPloomes\\ClientesJSON.txt", true, Encoding.ASCII);

                sw.Write(content);
                sw.Close();

                Console.WriteLine($"Status da requisição: {res.StatusCode}");
                Console.WriteLine($"Documento criado com sucesso! Verifique em:\nC:\\Users\\CaioLucas\\Workspace\\ApiPloomes\\ClientesJSON.txt");

                Console.WriteLine(msgSaida);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            break;

        case "2":
            try
            {
                Console.WriteLine("Digite o Id do cliente que dejesa consultar: \n");
                var id = Console.ReadLine();

                var msg1 = new HttpRequestMessage(HttpMethod.Get, url + $"?$filter=Id+eq+{id}");
                msg1.Headers.Add($"User-Key", key);
                var res1 = await client.SendAsync(msg1);

                var content1 = await res1.Content.ReadAsStringAsync();

                StreamWriter sw = new StreamWriter("C:\\Users\\CaioLucas\\Workspace\\ApiPloomes\\ClienteJSON.txt", true, Encoding.ASCII);

                sw.Write(content1);
                sw.Close();

                Console.WriteLine($"Status da requisição: {res1.StatusCode}");
                Console.WriteLine($"Documento criado com sucesso! Verifique em:\nC:\\Users\\CaioLucas\\Workspace\\ApiPloomes\\ClienteJSON.txt");

                Console.WriteLine(msgSaida);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            break;

        case "3":
            try
            {
                Console.Clear();

                Console.WriteLine("Digite o Id do cliente que dejesa excluir: \n");
                var idExclusao = Console.ReadLine();

                var msg2 = new HttpRequestMessage(HttpMethod.Delete, url + $"({idExclusao})");
                msg2.Headers.Add($"User-Key", key);
                var res2 = await client.SendAsync(msg2);

                if (res2.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("Cliente deletado com sucesso!");
                }
                else
                {
                    Console.WriteLine($"Houve algum erro na exclusão, o Status da requisição é: {res2.StatusCode}");
                }

                Console.WriteLine(msgSaida);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            break;

        case "5":
            CriaRegistroAsync();
            break;

        case "m":
            Console.Clear();
            ExibirOpc();
            break;

        case "0":
            exibirMenu = false;
            break;

        default:
			Console.WriteLine("Opção inválida!");
			break;
	}

	opc = Console.ReadLine();

}
void ExibirOpc()
{
    Console.WriteLine("Digite a opção desejada: \n");
    Console.WriteLine("1- Gerar TXT contendo todos os clientes");
    Console.WriteLine("2- Gerar TXT de um cliente especifico");
    Console.WriteLine("3- Exclusão de Cliente");
    Console.WriteLine("3- Criar registro no cliente");
    Console.WriteLine("0- Sair da aplicação");
}

async Task CriaRegistroAsync()
{

    var url = "https://app24-api2.ploomes.com/InteractionRecords";

    using var client = new HttpClient();

    var data = new Dictionary<string, string>
{
    {"name", "John Doe"},
    {"occupation", "gardener"}
};

    var res = await client.PostAsync(url, new FormUrlEncodedContent(data));
    res.Headers.Add($"User-Key", key);

    var content = await res.Content.ReadAsStringAsync();

    Console.WriteLine(content);



}
using Blog_Pessoal.Model;
using Blog_Pessoal_Teste.Factory;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.Ordering;

namespace Blog_Pessoal_Teste.Controller
{
    public class UserControllerTeste : IClassFixture<WebAppFactory>
    {
        protected readonly WebAppFactory _factory;
        protected HttpClient _client;

        private readonly dynamic token;
        private string Id { get; set; } = string.Empty;

        public UserControllerTeste(WebAppFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            token = GetToken();
        }
        private static dynamic GetToken()
        {
            dynamic data = new ExpandoObject();
            data.sub = "root@toot.com";
            return data;
        }

        [Fact, Order(1)]
        public async Task DeveCriarUmUsuario()
        {
            var novoUsuario = new Dictionary<string, string>()
            {
                {"nome", "Ingrid" },
                {"usuario", "ingrid@email.com" },
                {"senha", "1234567845" },
                {"foto", "" }
            };

            var usuarioJson = JsonConvert.SerializeObject(novoUsuario);
            var corpoRequisicao = new StringContent(usuarioJson, Encoding.UTF8, "application/json");

            var resposta = await _client.PostAsync("/usuarios/cadastrar", corpoRequisicao);

            resposta.EnsureSuccessStatusCode();

            resposta.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact, Order(2)]
        public async Task DeveDarErroEmail()
        {
            var novoUsuario = new Dictionary<string, string>()
            {
                {"nome", "Ingrid" },
                {"usuario", "ingrid@email.com" },
                {"senha", "1234567845" },
                {"foto", "" }
            };

            var usuarioJson = JsonConvert.SerializeObject(novoUsuario);
            var corpoRequisicao = new StringContent(usuarioJson, Encoding.UTF8, "application/json");

            var resposta = await _client.PostAsync("/usuarios/cadastrar", corpoRequisicao);

            //resposta.EnsureSuccessStatusCode();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, Order(3)]
        public async Task NaoDeveCriarUsuarioDuplicado()
        {
            var novoUsuario = new Dictionary<string, string>()
            {
                {"nome", "Karina" },
                {"usuario", "karina@email.com" },
                {"senha", "1234567845" },
                {"foto", "" }
            };

            var usuarioJson = JsonConvert.SerializeObject(novoUsuario);
            var corpoRequisicao = new StringContent(usuarioJson, Encoding.UTF8, "application/json");

            await _client.PostAsync("/usuarios/cadastrar", corpoRequisicao);

            var resposta = await _client.PostAsync("/usuarios/cadastrar", corpoRequisicao);

            //resposta.EnsureSuccessStatusCode();

            resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }
        [Fact, Order(4)]
        public async Task DeveListarTodosUsuarios()
        {
            _client.SetFakeBearerToken((object)token);

            var resposta = await _client.GetAsync("/usuarios/all");

            resposta.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Order(5)]
        public async Task DeveAtualizarUmUsuario()
        {
            var novoUsuario = new Dictionary<string, string>()
            {
                {"nome", "joao" },
                {"usuario", "joao@email.com" },
                {"senha", "1234567845" },
                {"foto", "" }
            };

            var usuarioJson = JsonConvert.SerializeObject(novoUsuario);
            var corpoRequisicao = new StringContent(usuarioJson, Encoding.UTF8, "application/json");

            var resposta = await _client.PostAsync("/usuarios/cadastrar", corpoRequisicao);

            var corpoRespostaPost = await resposta.Content.ReadFromJsonAsync<User>();

            if(corpoRespostaPost != null)
            {
                Id = corpoRespostaPost.Id.ToString();
            }

            var atualizaUsuario = new Dictionary<string, string>()
            {
                {"id", Id },
                {"nome", "joao maia" },
                {"usuario", "joao@email.com" },
                {"senha", "1234567845" },
                {"foto", "" }
            };

            var usuarioJsonAtualizar = JsonConvert.SerializeObject(atualizaUsuario);
            var corpoRequisicaoAtualizar = new StringContent(usuarioJsonAtualizar, Encoding.UTF8, "application/json");

            _client.SetFakeBearerToken((object)token);

            var respostaPut = await _client.PutAsync("/usuarios/atualizar", corpoRequisicaoAtualizar);

            respostaPut.StatusCode.Should().Be(HttpStatusCode.OK);

        }
    }
}
using System;
using EventManagement.Domain.Entities;
using Xunit;

namespace EventManagement.Domain.Tests
{
    public class EventSpecs
    {
        [Fact]
        public void Deve_Criar_Evento_Valido()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(5), TimeSpan.FromHours(2));
            Assert.Equal("Workshop", e.Title);
            Assert.Equal(string.Empty, e.EventCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void Nao_Deve_Aceitar_Id_Invalido(int id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Event(id, "Teste", DateTime.Now.AddDays(1), TimeSpan.FromHours(1)));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Nao_Deve_Aceitar_Titulo_Invalido(string? titulo)
        {
            Assert.Throws<ArgumentException>(() => new Event(1, titulo!, DateTime.Now.AddDays(1), TimeSpan.FromHours(1)));
        }

        [Fact]
        public void Nao_Deve_Aceitar_Data_No_Passado()
        {
            Assert.Throws<ArgumentException>(() => new Event(1, "Evento", DateTime.Now.AddDays(-1), TimeSpan.FromHours(2)));
        }

        [Fact]
        public void Nao_Deve_Aceitar_Duracao_Menor_Que_30_Minutos()
        {
            Assert.Throws<ArgumentException>(() => new Event(1, "Evento", DateTime.Now.AddDays(1), TimeSpan.FromMinutes(10)));
        }

        [Fact]
        public void Deve_Definir_EventCode_Valido()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            e.SetEventCode("COD123 ");
            Assert.Equal("COD123", e.EventCode);
        }

        [Fact]
        public void SetEventCode_Nao_Deve_Aceitar_Null()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            Assert.Throws<ArgumentException>(() => e.SetEventCode(null!));
        }

        [Fact]
        public void Deve_Definir_Descricao_Valida()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            e.SetDescription("Evento sobre C#");
            Assert.Equal("Evento sobre C#", e.Description);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Nula()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            e.SetDescription(null);
            Assert.Null(e.Description);
        }

        [Fact]
        public void Requirements_Aceita_Null()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            e.Requirements = null;
            Assert.Equal(string.Empty, e.Requirements);
        }

        [Fact]
        public void Notes_Aceita_Null()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            e.Notes = null;
            Assert.Equal(string.Empty, e.Notes);
        }

        [Fact]
        public void Venue_Deve_Carregar_Default_Quando_Nulo()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            Assert.Equal("Evento Online", e.Venue.Name);
        }

        [Fact]
        public void Venue_Deve_Manter_Mesma_Instancia_Apos_Acesso()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            var v1 = e.Venue;
            var v2 = e.Venue;
            Assert.Same(v1, v2);
        }

        [Fact]
        public void Deve_Associar_Palestrante_Principal()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            var s = new Speaker(1, "João", "a@a.com");
            e.AssignMainSpeaker(s);
            Assert.Equal(s, e.MainSpeaker);
        }

        [Fact]
        public void MainSpeaker_Pode_Ser_Null()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(1), TimeSpan.FromHours(2));
            Assert.Null(e.MainSpeaker);
        }

        [Fact]
        public void ToString_Deve_Conter_Titulo_E_Data()
        {
            var e = new Event(1, "Workshop", DateTime.Now.AddDays(2), TimeSpan.FromHours(2));
            e.SetEventCode("COD");
            var text = e.ToString();
            Assert.Contains("Workshop", text);
            Assert.Contains("COD", text);
        }
    }
}

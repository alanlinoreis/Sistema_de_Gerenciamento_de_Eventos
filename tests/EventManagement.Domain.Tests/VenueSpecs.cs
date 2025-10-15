using System;
using EventManagement.Domain.Entities;
using Xunit;

namespace EventManagement.Domain.Tests
{
    public class VenueSpecs
    {
        [Fact]
        public void Deve_Criar_Venue_Valido()
        {
            var v = new Venue(1, "Centro", "Rua A", 100);
            Assert.Equal(1, v.VenueId);
            Assert.Equal("Centro", v.Name);
            Assert.Equal("Rua A", v.Address);
            Assert.Equal(100, v.Capacity);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Nao_Deve_Aceitar_Id_Invalido(int id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Venue(id, "Local", "Rua B", 10));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Nao_Deve_Aceitar_Nome_Invalido(string? nome)
        {
            Assert.Throws<ArgumentException>(() => new Venue(1, nome!, "Rua B", 10));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Nao_Deve_Aceitar_Endereco_Invalido(string? endereco)
        {
            Assert.Throws<ArgumentException>(() => new Venue(1, "Local", endereco!, 10));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Nao_Deve_Aceitar_Capacidade_Invalida(int cap)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Venue(1, "Local", "Rua X", cap));
        }

        [Fact]
        public void Deve_Definir_Descricao_Valida()
        {
            var v = new Venue(1, "Centro", "Rua A", 50);
            v.SetDescription("Espaço amplo");
            Assert.Equal("Espaço amplo", v.Description);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia()
        {
            var v = new Venue(1, "Centro", "Rua A", 50);
            v.SetDescription("  ");
            Assert.Null(v.Description);
        }

        [Fact]
        public void ParkingInfo_Aceita_Null_E_Retorna_Vazio()
        {
            var v = new Venue(1, "Local", "Rua", 10);
            v.ParkingInfo = null;
            Assert.Equal(string.Empty, v.ParkingInfo);
        }

        [Fact]
        public void Default_Deve_Ser_OnlineEvent()
        {
            var d = Venue.Default;
            Assert.Equal("Evento Online", d.Name);
            Assert.Equal("Virtual", d.Address);
        }

        [Fact]
        public void Equals_Deve_Retornar_Verdadeiro_Para_Mesmo_Id()
        {
            var v1 = new Venue(1, "A", "Rua", 10);
            var v2 = new Venue(1, "B", "Outra", 20);
            Assert.True(v1.Equals(v2));
        }

        [Fact]
        public void Equals_Deve_Retornar_Falso_Para_Ids_Diferentes()
        {
            var v1 = new Venue(1, "A", "Rua", 10);
            var v2 = new Venue(2, "A", "Rua", 10);
            Assert.False(v1.Equals(v2));
        }

        [Fact]
        public void GetHashCode_Baseado_Em_Id()
        {
            var v1 = new Venue(1, "A", "Rua", 10);
            var v2 = new Venue(1, "B", "Outra", 20);
            Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
        }

        [Fact]
        public void ToString_Deve_Conter_Nome_E_Endereco()
        {
            var v = new Venue(1, "Centro", "Av. Principal", 100);
            var text = v.ToString();
            Assert.Contains("Centro", text);
            Assert.Contains("Av. Principal", text);
        }

        [Fact]
        public void Descricao_Deve_Manter_Trim()
        {
            var v = new Venue(1, "Centro", "Rua A", 100);
            v.SetDescription("   Sala ampla   ");
            Assert.Equal("Sala ampla", v.Description);
        }

        [Fact]
        public void ParkingInfo_Pode_Ser_Modificada()
        {
            var v = new Venue(1, "Local", "Rua", 10);
            v.ParkingInfo = "Subsolo";
            Assert.Equal("Subsolo", v.ParkingInfo);
        }
    }
}
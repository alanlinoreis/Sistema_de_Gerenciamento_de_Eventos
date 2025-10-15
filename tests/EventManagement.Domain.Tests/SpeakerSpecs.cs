using System;
using EventManagement.Domain.Entities;
using Xunit;

namespace EventManagement.Domain.Tests
{
    public class SpeakerSpecs
    {
        [Fact]
        public void Deve_Criar_Speaker_Valido()
        {
            var speaker = new Speaker(1, "João Silva", "joao@email.com");

            Assert.Equal(1, speaker.SpeakerId);
            Assert.Equal("João Silva", speaker.FullName);
            Assert.Equal("joao@email.com", speaker.Email);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Nao_Deve_Permitir_SpeakerId_Invalido(int id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Speaker(id, "Fulano", "teste@teste.com"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Nao_Deve_Permitir_Nome_Invalido(string? nome)
        {
            Assert.Throws<ArgumentException>(() => new Speaker(1, nome!, "teste@teste.com"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("semarroba")]
        [InlineData(" ")]
        public void Nao_Deve_Permitir_Email_Invalido(string? email)
        {
            Assert.Throws<ArgumentException>(() => new Speaker(1, "Fulano", email!));
        }

        [Fact]
        public void Deve_Definir_Biografia_Valida()
        {
            var s = new Speaker(1, "Fulano", "a@a.com");
            s.SetBiography("Palestrante experiente");
            Assert.Equal("Palestrante experiente", s.Biography);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Deve_Definir_Biografia_Nula_Quando_Vazia(string? bio)
        {
            var s = new Speaker(1, "Fulano", "a@a.com");
            s.SetBiography(bio);
            Assert.Null(s.Biography);
        }

        [Fact]
        public void Company_Aceita_Null_E_Retorna_Vazio()
        {
            var s = new Speaker(1, "Fulano", "a@a.com");
            s.Company = null;
            Assert.Equal(string.Empty, s.Company);
        }

        [Fact]
        public void LinkedIn_Aceita_Null_E_Retorna_Vazio()
        {
            var s = new Speaker(1, "Fulano", "a@a.com");
            s.LinkedInProfile = null;
            Assert.Equal(string.Empty, s.LinkedInProfile);
        }

        [Fact]
        public void Equals_Deve_Retornar_Verdadeiro_Para_Mesmo_Id()
        {
            var a = new Speaker(1, "João", "a@a.com");
            var b = new Speaker(1, "Outro", "b@b.com");
            Assert.True(a.Equals(b));
        }

        [Fact]
        public void Equals_Deve_Retornar_Falso_Para_Id_Diferente()
        {
            var a = new Speaker(1, "João", "a@a.com");
            var b = new Speaker(2, "João", "a@a.com");
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void GetHashCode_Deve_Ser_Baseado_Em_Id()
        {
            var s1 = new Speaker(1, "João", "a@a.com");
            var s2 = new Speaker(1, "Maria", "b@b.com");
            Assert.Equal(s1.GetHashCode(), s2.GetHashCode());
        }

        [Fact]
        public void ToString_Deve_Trazer_Informacoes_Basicas()
        {
            var s = new Speaker(1, "João", "a@a.com");
            s.SetBiography("Experiente");
            Assert.Contains("João", s.ToString());
            Assert.Contains("Experiente", s.ToString());
        }

        [Fact]
        public void ToString_Deve_Ser_Limpo_Quando_Sem_Biografia()
        {
            var s = new Speaker(1, "João", "a@a.com");
            Assert.DoesNotContain("-", s.ToString());
        }

        [Fact]
        public void Deve_Permitir_Modificar_Company_E_LinkedIn()
        {
            var s = new Speaker(1, "João", "a@a.com");
            s.Company = "NovaCorp";
            s.LinkedInProfile = "linkedin.com/joao";
            Assert.Equal("NovaCorp", s.Company);
            Assert.Equal("linkedin.com/joao", s.LinkedInProfile);
        }

        [Fact]
        public void Biografia_Deve_Manter_Trim()
        {
            var s = new Speaker(1, "João", "a@a.com");
            s.SetBiography("   Texto com espaços   ");
            Assert.Equal("Texto com espaços", s.Biography);
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities
{   
    // Representa um palestrante de um evento
    public class Speaker
    {
        public int SpeakerId { get; }
        public string FullName { get; }
        public string Email { get; }

        private string? _biography;

        [AllowNull]
        public string Company
        {
            get => _company ?? string.Empty;
            set => _company = value ?? string.Empty;
        }

        [AllowNull]
        public string LinkedInProfile
        {
            get => _linkedInProfile ?? string.Empty;
            set => _linkedInProfile = value ?? string.Empty;
        }

        private string? _company;
        private string? _linkedInProfile;

        public string? Biography => _biography;

        public Speaker(int speakerId, string fullName, string email)
        {
            // Validações básicas
            Guard.AgainstNegativeOrZero(speakerId, nameof(speakerId));
            Guard.AgainstNullOrWhiteSpace(fullName, nameof(fullName));

            if (!Guard.IsValidEmail(email))
                throw new ArgumentException("Email com o formato invalido.", nameof(email));

            SpeakerId = speakerId;
            FullName = fullName.Trim();
            Email = email.Trim();
        }

        // Define a biografia de forma segura (pode ser nula)
        public void SetBiography(string? biography)
        {
            Guard.TryParseNonEmpty(biography, out var result);
            _biography = result;
        }

        public override bool Equals(object? obj)
        {
            return obj is Speaker other && SpeakerId == other.SpeakerId;
        }

        public override int GetHashCode() => SpeakerId.GetHashCode();

        public override string ToString() =>
            $"{FullName} ({Email}){(string.IsNullOrWhiteSpace(Biography) ? "" : $" - {Biography}")}";
    }
}

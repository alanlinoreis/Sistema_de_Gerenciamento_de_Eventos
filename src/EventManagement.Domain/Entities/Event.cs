using System;
using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities
{
    // Representa um evento no sistema
    public class Event
    {
        public int EventId { get; }
        public string Title { get; }
        public DateTime EventDate { get; }
        public TimeSpan Duration { get; }

        [DisallowNull]
        public string EventCode
        {
            get => _eventCode ?? string.Empty;
            private set => _eventCode = value ?? throw new ArgumentNullException(nameof(EventCode));
        }

        private string? _eventCode;
        private string? _description;
        private Venue? _venue;

        [AllowNull]
        public string Requirements
        {
            get => _requirements ?? string.Empty;
            set => _requirements = value ?? string.Empty;
        }

        [AllowNull]
        public string Notes
        {
            get => _notes ?? string.Empty;
            set => _notes = value ?? string.Empty;
        }

        private string? _requirements;
        private string? _notes;

        [MemberNotNull(nameof(_venue))]
        public Venue Venue
        {
            // Lazy loading — cria o local padrão caso não tenha sido definido
            get
            {
                _venue ??= Venue.Default;
                return _venue;
            }
            set => _venue = value ?? Venue.Default;
        }

        public Speaker? MainSpeaker { get; private set; }

        public string? Description => _description;

        public Event(int eventId, string title, DateTime eventDate, TimeSpan duration)
        {
            Guard.AgainstNegativeOrZero(eventId, nameof(eventId));
            Guard.AgainstNullOrWhiteSpace(title, nameof(title));
            Guard.AgainstPastDate(eventDate, nameof(eventDate));

            if (duration.TotalMinutes < 30)
                throw new ArgumentException("A duração precisa ser no mínimo 30 minutos.", nameof(duration));

            EventId = eventId;
            Title = title.Trim();
            EventDate = eventDate;
            Duration = duration;
            _eventCode = string.Empty;
        }

        public void SetEventCode(string code)
        {
            Guard.AgainstNullOrWhiteSpace(code, nameof(code));
            EventCode = code.Trim();
        }

        // Define a descrição de forma segura (pode ser nula)
        public void SetDescription(string? description)
        {
            Guard.TryParseNonEmpty(description, out var result);
            _description = result;
        }

        public void AssignMainSpeaker(Speaker speaker)
        {
            Guard.AgainstNull(speaker, nameof(speaker));
            MainSpeaker = speaker;
        }

        public override string ToString() =>
            $"{Title} ({EventCode}) - {EventDate:dd/MM/yyyy} | Duração: {Duration.TotalHours}h";
    }
}
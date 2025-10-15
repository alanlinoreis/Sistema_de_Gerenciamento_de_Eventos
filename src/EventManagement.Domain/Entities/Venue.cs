using System;
using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities
{
    // Representa o local onde o evento será realizado
    public class Venue
    {
        public int VenueId { get; }
        public string Name { get; }
        public string Address { get; }
        public int Capacity { get; }

        private string? _description;

        [AllowNull]
        public string ParkingInfo
        {
            get => _parkingInfo ?? string.Empty;
            set => _parkingInfo = value ?? string.Empty;
        }

        private string? _parkingInfo;

        public string? Description => _description;

        // Local padrão para eventos online (lazy loading)
        private static readonly Venue _default = new(1, "Evento Online", "Virtual", 1);
        public static Venue Default => _default;
        public Venue(int venueId, string name, string address, int capacity)
        {
            Guard.AgainstNegativeOrZero(venueId, nameof(venueId));
            Guard.AgainstNullOrWhiteSpace(name, nameof(name));
            Guard.AgainstNullOrWhiteSpace(address, nameof(address));
            Guard.AgainstNegativeOrZero(capacity, nameof(capacity));

            VenueId = venueId;
            Name = name.Trim();
            Address = address.Trim();
            Capacity = capacity;
        }

        // Define a descrição de forma segura (pode ser nula)
        public void SetDescription(string? description)
        {
            
            Guard.TryParseNonEmpty(description, out var result);
            _description = result;
            
        }
        
        public override bool Equals(object? obj) =>
            obj is Venue other && VenueId == other.VenueId;

        public override int GetHashCode() => VenueId.GetHashCode();

        public override string ToString() => $"{Name} - {Address} (Capacidade: {Capacity})";
    }
}

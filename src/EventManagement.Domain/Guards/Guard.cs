using System;

namespace EventManagement.Domain.Guards
{
    // Classe utilitária para validações de parâmetros (programação defensiva)
    public static class Guard
    {
        // Garante que o valor não seja nulo
        public static void AgainstNull(object? value, string paramName)
        {
            if (value is null)
                throw new ArgumentNullException(paramName);
        }
        // Garante que a string não seja nula, vazia ou apenas espaços
        public static void AgainstNullOrWhiteSpace(string? value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{paramName} cannot be null or whitespace.", paramName);
        }

        // Garante que o valor numérico seja maior que zero
        public static void AgainstNegativeOrZero(int value, string paramName)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater than zero.");
        }

        // Garante que a data não esteja no passado
        public static void AgainstPastDate(DateTime date, string paramName)
        {
            if (date < DateTime.Now)
                throw new ArgumentException($"{paramName} cannot be in the past.", paramName);
        }

        // Tenta extrair uma string válida (sem lançar exceção)
        public static bool TryParseNonEmpty(string? input, out string? result)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                result = null;
                return false;
            }

            result = input.Trim();
            return true;
        }
        
        // Validação simples de e-mail (contém texto e '@')
        public static bool IsValidEmail(string? email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains('@');
        }
    }
}

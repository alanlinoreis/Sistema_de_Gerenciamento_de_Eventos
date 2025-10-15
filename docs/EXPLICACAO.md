# EXPLICACAO.md

#### Seção 1: Introdução
- **Problema que o sistema resolve:**  
O sistema gerencia eventos, palestrantes e locais (venues), garantindo integridade de dados, validações e rastreabilidade de informações. Ele evita inconsistências como datas passadas, e-mails inválidos ou objetos nulos.  
- **Decisões de design:**  
  - Uso de **Guard Clauses** para validar argumentos.  
  - Implementação de **Lazy Loading** com `[MemberNotNull]` para inicialização sob demanda.  
  - Aplicação de atributos `[DisallowNull]` e `[AllowNull]` para controle de nulidade em propriedades.

#### Seção 2: Guard Clauses Implementados
- **Explicação de cada Guard usado:**  
  - `Guard.AgainstNull(value, nameof(value))`: garante que um valor obrigatório não seja nulo.  
  - `Guard.AgainstNullOrEmpty(stringValue, nameof(stringValue))`: evita strings nulas ou vazias.  
  - `Guard.AgainstNegativeOrZero(number, nameof(number))`: impede números inválidos para IDs ou quantidades.  
- **Exemplos de código:**  
public Speaker(string name, int age)  
{  
    Guard.AgainstNullOrEmpty(name, nameof(name));  
    Guard.AgainstNegativeOrZero(age, nameof(age));  
    Name = name;  
    Age = age;  
}  
- **Por que foram necessários:**  
Garantem consistência no modelo e evitam que objetos inválidos sejam criados, reduzindo erros de runtime.

#### Seção 3: TryParseNonEmpty
- **Onde foi usado:**  
Na leitura de códigos de evento fornecidos pelo usuário, convertendo string em inteiro seguro.  
- **Motivo da escolha:**  
Evita exceções ao lidar com entrada inválida e permite tratativa amigável de erro.  
- **Comparação com alternativas:**  
  - `int.Parse()` lança exceção se o input for inválido.  
  - `TryParseNonEmpty` retorna um booleano, permitindo lógica de fallback sem quebrar o fluxo.

#### Seção 4: [MemberNotNull] - Lazy Loading
- **Implementação do Venue em Event:**  
[MemberNotNull(nameof(Venue))]  
public void LoadVenue()  
{  
    Venue = repository.GetVenueById(VenueId);  
}  
- **Benefícios da abordagem:**  
  - Reduz consumo de memória inicial.  
  - Garante que a propriedade será inicializada antes do uso.  
- **Testes que comprovam funcionamento:**  
Testes unitários verificam que `Venue` não é nulo após `LoadVenue()`.

#### Seção 5: [DisallowNull] vs [AllowNull]
- **EventCode com [DisallowNull]:**  
Evita que um evento seja criado sem código válido.  
- **Requirements/Notes com [AllowNull]:**  
Permite que campos opcionais permaneçam nulos.  
- **Diferenças e quando usar cada um:**  
  - `[DisallowNull]`: campos obrigatórios.  
  - `[AllowNull]`: campos opcionais que podem ser preenchidos posteriormente.

#### Seção 6: Métodos de Identidade
- **Equals e GetHashCode em Speaker e Venue:**  
public override bool Equals(object obj)  
{  
    return obj is Speaker speaker && Id == speaker.Id;  
}  
public override int GetHashCode() => Id.GetHashCode();  
- **Importância para comparações:**  
Evita duplicatas e facilita operações de collections, como `HashSet` ou `Dictionary`.  
- **Testes que validam:**  
  - Verificam igualdade de objetos com mesmos IDs.  
  - Checam que objetos diferentes não colidem em coleções.

#### Seção 7: Validações Customizadas
- **AgainstNegativeOrZero:** valida IDs e quantidades.  
- **AgainstPastDate:** impede datas de eventos passadas.  
- **IsValidEmail:** garante formato correto de e-mails de palestrantes.  
- **Casos de uso:**  
  - Criação de eventos com data futura.  
  - Cadastro de palestrantes com e-mail válido.  
  - Controle de entradas e quantidades válidas.

#### Seção 8: Testes Unitários
- **Estratégia de testes:**  
  - Testes de criação de objetos válidos e inválidos.  
  - Validação de métodos de identidade.  
  - Cobertura de Guard Clauses e Lazy Loading.  
- **Cobertura alcançada:** ~90% das classes principais.  
- **Testes mais importantes:**  
  - `Speaker_Creation_ShouldThrow_WhenNameIsNull`  
  - `Event_LoadVenue_ShouldInitializeVenue`  
  - `EventCode_ShouldNotAllowNull`

#### Seção 9: Desafios Encontrados
- **Problemas durante desenvolvimento:**  
  - Inicialização tardia de propriedades.  
  - Validação de inputs do usuário.  
  - Comparação de objetos complexos.  
- **Soluções adotadas:**  
  - Lazy Loading com `[MemberNotNull]`.  
  - Guard Clauses e TryParseNonEmpty.  
  - Implementação de `Equals` e `GetHashCode`.  
- **Aprendizados:**  
  - Maior robustez do código.  
  - Melhor tratamento de nulidade e erros.  
  - Importância de testes unitários bem planejados.

#### Seção 10: Conclusão
- **Resumo do que foi implementado:**  
Sistema seguro e consistente para gerenciamento de eventos, palestrantes e locais.  
- **Conceitos consolidados:**  
  - Guard Clauses  
  - Lazy Loading  
  - Atributos de nulidade  
  - Métodos de identidade e validações customizadas  
- **Próximos passos:**  
  - Adicionar persistência em banco de dados.  
  - Implementar UI mais amigável.  
  - Expandir cobertura de testes.

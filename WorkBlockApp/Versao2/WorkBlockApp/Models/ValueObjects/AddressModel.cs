using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WorkBlockApp.Models.ValueObjects;

public class AddressModel
{
    [Required(ErrorMessage = "O campo rua é obrigatório")]
    [Display(Name = "Localidade", Prompt = "Localidade"), StringLength(100)]
    [JsonPropertyName("street")]
    public string Street { get; private set; }

    [Required(ErrorMessage = "O campo número é obrigatório")]
    [Display(Name = "Número", Prompt = "000"), StringLength(7)]
    [JsonPropertyName("number")]
    public string Number { get; private set; }

    [Required(ErrorMessage = "O campo bairro é obrigatório")]
    [Display(Name = "Bairro", Prompt = "Bairro"), StringLength(100)]
    [JsonPropertyName("neighborhood")]
    public string Neighborhood { get; private set; }

    [Required(ErrorMessage = "O campo cidade é obrigatório")]
    [Display(Name = "Cidade", Prompt = "Cidade"), StringLength(100)]
    [JsonPropertyName("city")]
    public string City { get; private set; }

    [Required(ErrorMessage = "O campo estado é obrigatório")]
    [Display(Name = "Estado", Prompt = "Estado"), StringLength(100)]
    [JsonPropertyName("state")]
    public string State { get; private set; }

    [Required(ErrorMessage = "O campo país é obrigatório")]
    [Display(Name = "País", Prompt = "País"), StringLength(100)]
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [Required(ErrorMessage = "O campo cep é obrigatório")]
    [RegularExpression("\\d{2}\\.\\d{3}-\\d{3}", ErrorMessage = "O campo CPF deve estar em formato correto")]
    [Display(Name = "CEP", Prompt = "00.000-000"), StringLength(100)]
    [JsonPropertyName("zipCode")]
    public string Zip { get; private set; }

    public AddressModel(string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string country,
        string zip)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        Zip = zip;
    }

    public override string ToString()
    {
        return $"{Street}, {Number} - {Neighborhood}, {City}/{State} - {Country}";
    }
}

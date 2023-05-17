using System.ComponentModel.DataAnnotations;

namespace WorkBlockApi.Models.ValueObjects;

public class Address
{
    [Required(ErrorMessage = "Street is required")]
    public string Street { get; private set; }

    [Required(ErrorMessage = "Number is required")]
    public string Number { get; private set; }

    [Required(ErrorMessage = "Neighborhood is required")]
    public string Neighborhood { get; private set; }

    [Required(ErrorMessage = "City is required")]
    public string City { get; private set; }

    [Required(ErrorMessage = "State is required")]
    public string State { get; private set; }

    [Required(ErrorMessage = "Country is required")]
    public string Country { get; private set; }

    [Required(ErrorMessage = "ZipCode is required")]
    public string ZipCode { get; private set; }

    public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public override string ToString()
    {
        return $"{Street}, {Number}. {Neighborhood}. City: {City} - {State}. ZipCode: {ZipCode}. {Country}.";
    }
}
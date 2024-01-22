using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;



/// <summary>
/// Enkelt adressboksprogram i konsolen.
/// </summary>
class Program
{
    // Lista för att lagra kontakter
    static readonly List<Contact> addressBook = [];

    /// <summary>
    /// Main-metoden, där programmet börjar.
    /// </summary>
    static void Main()
    {
        // Ladda tidigare sparade kontakter vid programmets start
        LoadContacts();

        while (true)
        {
            Console.WriteLine("Välkommen till Adressboken");
            Console.WriteLine("1. Lägg till kontakt");
            Console.WriteLine("2. Visa alla kontakter");
            Console.WriteLine("3. Visa detaljerad information om en kontakt");
            Console.WriteLine("4. Ta bort kontakt");
            Console.WriteLine("5. Avsluta");
            Console.WriteLine("#########################");
            Console.Write("Välj en alternativ (1-5): ");

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AddContact();
                    break;
                case "2":
                    Console.Clear();
                    DisplayAllContacts();
                    break;
                case "3":
                    Console.Clear();
                    DisplayContactDetails();
                    break;
                case "4":
                    Console.Clear();
                    RemoveContact();
                    break;
                case "5":
                    Console.Clear();
                    SaveContacts(); // Spara kontakter innan du avslutar programmet
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }

            Console.WriteLine();
        }
    }

    /// Lägger till en ny kontakt i adressboken.
    static void AddContact()
    {
        Console.Write("Ange namn: ");
        string name = Console.ReadLine()!;

        Console.Write("Ange telefonnummer: ");
        string phoneNumber = Console.ReadLine()!;

        Console.Write("Ange e-postadress: ");
        string email = Console.ReadLine()!;

        Console.Write("Ange adress: ");
        string address = Console.ReadLine()!;

        Contact newContact = new Contact(name, phoneNumber, email, address);
        addressBook.Add(newContact);

        Console.Clear();
        Console.WriteLine("Kontakten har lagts till!");
    }

    /// Visar alla kontakter i adressboken med numrering.
    static void DisplayAllContacts()
    {
        if (addressBook.Count == 0)
        {
            Console.WriteLine("Adressboken är tom.");
        }
        else
        {
            Console.WriteLine("Alla kontakter:");

            // Använda en räknare för att numrera kontakterna
            for (int i = 0; i < addressBook.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {addressBook[i]}");
            }
        }
    }


    /// Visar detaljerad information om en specifik kontakt.
    static void DisplayContactDetails()
    {
        Console.Write("Ange namn att söka efter: ");
        string searchName = Console.ReadLine()!;

        Contact foundContact = addressBook.Find(contact => contact.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase))!;

        if (foundContact != null)
        {
            Console.WriteLine("Detaljerad information om kontakt:");
            Console.WriteLine(foundContact);
        }
        else
        {
            Console.WriteLine("Ingen kontakt hittad med det angivna namnet.");
        }
    }

    /// Tar bort en kontakt baserat på e-postadress.
    static void RemoveContact()
    {
        Console.Write("Ange e-postadress för att ta bort kontakten: ");
        string emailToRemove = Console.ReadLine()!;

        Contact contactToRemove = addressBook.Find(contact => contact.Email.Equals(emailToRemove, StringComparison.OrdinalIgnoreCase))!;

        if (contactToRemove != null)
        {
            addressBook.Remove(contactToRemove);
            Console.WriteLine("Kontakt borttagen!");
        }
        else
        {
            Console.WriteLine("Ingen kontakt hittad med den angivna e-postadressen.");
        }
    }

    /// Sparar alla kontakter i en JSON-fil.
    static void SaveContacts()
    {
        string json = JsonConvert.SerializeObject(addressBook, Formatting.Indented);
        File.WriteAllText("contacts.json", json);
        Console.WriteLine("Kontakter sparade till contacts.json.");
    }


    /// Laddar tidigare sparade kontakter från en JSON-fil.
    static void LoadContacts()
    {
        if (File.Exists("contacts.json"))
        {
            string json = File.ReadAllText("contacts.json");
            addressBook.AddRange(JsonConvert.DeserializeObject<List<Contact>>(json)!);
            Console.WriteLine("Kontakter laddade från contacts.json.");
        }
        else
        {
            Console.WriteLine("Ingen tidigare sparad fil hittad. En ny fil kommer att skapas när du sparar kontakter.");
        }
    }
}


/// Klassen som representerar en kontakt.
class Contact(string name, string phoneNumber, string email, string address)
{
    public string Name { get; set; } = name;
    public string PhoneNumber { get; set; } = phoneNumber;
    public string Email { get; set; } = email;
    public string Address { get; set; } = address;


    /// Överskriden ToString-metod för att enkelt visa kontaktinformation.
    public override string ToString()
    {
        return $"Namn: {Name}, Telefon: {PhoneNumber}, E-post: {Email}, Address: {Address}";
    }
}

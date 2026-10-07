using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace PRG_MAUI_Car_Register
{
    class Vehicle
    {
        // Medlemsvariabler
        public enum Type { Bil, MC, Lastbil };
        private Type vehicleType;
        private string registrationNumber = string.Empty;
        private string manufacturer = string.Empty;
        private string model = string.Empty;

        // Konstruktor (en metod med samma namn som klassen, som returnerar ett objekt)
        public Vehicle(Type vehicleType) // en konstruktor kan, men måste inte, ta parametrar
        {
            this.vehicleType = vehicleType;
        }

        // Get-Set för att hålla variablerna privata, och för att validera inkommande värden från UI (user interface, användargränssnittet)
        public string RegistrationNumber
        {
            get { return registrationNumber; }

            set
            {
                Regex registration = new Regex(@"^[a-zA-Z]{3}[0-9]{2}[a-zA-ZZ0-9]$");

                if (!String.IsNullOrWhiteSpace(value))
                {
                    Match match = registration.Match(value);
                    if (!match.Success)
                    {
                        throw new ArgumentException("Ett registreringsnummer måste bestå av exakt 6 tecken, med tre bokstäver följt av två siffror och en siffra eller bokstav. Inga speciella tecken.");
                    }
                }
                else
                {
                    throw new ArgumentException("Ett registreringsnummer måste bestå av exakt 6 tecken, med tre bokstäver följt av två siffror och en siffra eller bokstav.");
                }

                registrationNumber = value.ToUpper();
            }
        }

        // Fordonstyp tas in från dropdown-menyn, och behöver därför inte valideras
        public Type VehicleType
        {
            get { return vehicleType; }
            set { this.vehicleType = value; }
        }

        //TODO Tillverkare ska valideras, sparas i objektet och visas i UI
        public string Model
        {
            get { return model; }
            set 
            {

                if (!String.IsNullOrWhiteSpace(value))
                {
                    string yeardate = DateTime.Today.ToString("yyyy");
                    Regex model = new Regex(@"^[1-2][0-9]{3}$");
                    Match match = model.Match(value);

                    if (!match.Success)
                    {
                        throw new ArgumentException("Vänligen bara siffror för året modelen var gjord, eller ändra årtusende");
                    }

                    int year = int.Parse(value);
                    int date = int.Parse(yeardate);
                    
                    if (year < 1876)
                    {
                        throw new ArgumentException("Första bilen var skapat 1876. Innan det är omöjligt, försök igen.");
                    }

                    if (year > date)
                    {
                        throw new ArgumentException("Det finns igen bil skapat efter dagens år. Efter i år är omöjligt, försök igen.");
                    }

                }
                else
                {
                    throw new ArgumentException("Du måste skriva in bil modell");
                }

                this.model = value; 
            }
        }

        //TODO Modell ska valideras, sparas i objektet och visas i UI
        public string Manufacturer
        {
            get { return manufacturer; }
            set {
                    if (!String.IsNullOrWhiteSpace(value))
                    {
                        for (int i = 0; i < value.Length; i++)
                        {
                            if (char.IsLetterOrDigit(value[i]))
                            {
                                if (char.IsNumber(value[i]))
                                {
                                    throw new ArgumentException("Inget bilföretag har en siffra is sig. Vänligen använd bara bokstäver");
                                }
                            }
                            else
                            {
                                throw new ArgumentException("Det kan bara vara Bokstäver för tillverkare, inga special tecken.");
                            }
                        }
                        this.manufacturer = value;
                    }
                    else
                    {
                        throw new ArgumentException("Du måste fylla i vilket företag har skapat bilen.");
                    }
                }
        }

        //TODO Lägg till möjligheten att spara realistisk årsmodell, validera, spara och visa i objektet och visas i UI. Tips: Regex.IsMatch()


        //TODO Modifiera overriden på ToString() så att allt visas som önskat i UIs listBox
        public override string ToString()
        {
            return this.registrationNumber + "\t" + this.vehicleType + "\t" + this.manufacturer + "\t" + this.model;
        }
    }
}

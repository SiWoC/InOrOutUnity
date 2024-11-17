﻿using System;
using System.Collections.Generic;

public static class WordSets
{

    private static Random random = new Random();

    private static readonly List<WordSet> sets = new List<WordSet>
{
    new WordSet {InsiderWord = "Voetbal", OutsiderWord = "Bal"},
    new WordSet {InsiderWord = "Fiets", OutsiderWord = "Trappers"},
    new WordSet {InsiderWord = "TikTok", OutsiderWord = "Dansje"},
    new WordSet {InsiderWord = "Gaming", OutsiderWord = "Controller"},
    new WordSet {InsiderWord = "Netflix", OutsiderWord = "Aflevering"},
    new WordSet {InsiderWord = "Koken", OutsiderWord = "Pan"},
    new WordSet {InsiderWord = "Skaten", OutsiderWord = "Asfalt"},
    new WordSet {InsiderWord = "Fotografie", OutsiderWord = "Camera"},
    new WordSet {InsiderWord = "Concert", OutsiderWord = "Muziek"},
    new WordSet {InsiderWord = "Sneakers", OutsiderWord = "Veters"},
    new WordSet {InsiderWord = "Winkelen", OutsiderWord = "Tas"},
    new WordSet {InsiderWord = "Kunst", OutsiderWord = "Verf"},
    new WordSet {InsiderWord = "Boek", OutsiderWord = "Bladzijden"},
    new WordSet {InsiderWord = "Pizza", OutsiderWord = "Kaas"},
    new WordSet {InsiderWord = "Social media", OutsiderWord = "Bericht"},
    new WordSet {InsiderWord = "Festival", OutsiderWord = "Podium"},
    new WordSet {InsiderWord = "Hardlopen", OutsiderWord = "Schoenen"},
    new WordSet {InsiderWord = "Basketbal", OutsiderWord = "Net"},
    new WordSet {InsiderWord = "Musical", OutsiderWord = "Licht"},
    new WordSet {InsiderWord = "Hond", OutsiderWord = "Riempje"},
    new WordSet {InsiderWord = "Wandelen", OutsiderWord = "Bos"},
    new WordSet {InsiderWord = "Strand", OutsiderWord = "Zand"},
    new WordSet {InsiderWord = "Gitaar", OutsiderWord = "Snaren"},
    new WordSet {InsiderWord = "Films", OutsiderWord = "Popcorn"},
    new WordSet {InsiderWord = "Vissen", OutsiderWord = "Hengel"},
    new WordSet {InsiderWord = "Zeilen", OutsiderWord = "Boot"},
    new WordSet {InsiderWord = "Zwemmen", OutsiderWord = "Water"},
    new WordSet {InsiderWord = "Schaken", OutsiderWord = "Koning"},
    new WordSet {InsiderWord = "Lego", OutsiderWord = "Stenen"},
    new WordSet {InsiderWord = "Paardrijden", OutsiderWord = "Zadel"},
    new WordSet {InsiderWord = "Bioscoop", OutsiderWord = "Scherm"},
    new WordSet {InsiderWord = "Fotoboek", OutsiderWord = "Afbeelding"},
    new WordSet {InsiderWord = "Tuinieren", OutsiderWord = "Bloemen"},
    new WordSet {InsiderWord = "Bakfiets", OutsiderWord = "Wiel"},
    new WordSet {InsiderWord = "Voetbalstadion", OutsiderWord = "Tribune"},
    new WordSet {InsiderWord = "Koffie", OutsiderWord = "Beker"},
    new WordSet {InsiderWord = "IJssalon", OutsiderWord = "Bolletjes"},
    new WordSet {InsiderWord = "Pretpark", OutsiderWord = "Achtbaan"},
    new WordSet {InsiderWord = "Camping", OutsiderWord = "Tent"},
    new WordSet {InsiderWord = "Boerderij", OutsiderWord = "Dieren"},
    new WordSet {InsiderWord = "Markt", OutsiderWord = "Kraampje"},
    new WordSet {InsiderWord = "Koekjes", OutsiderWord = "Deeg"},
    new WordSet {InsiderWord = "Kerst", OutsiderWord = "Lichtjes"},
    new WordSet {InsiderWord = "Carnaval", OutsiderWord = "Masker"},
    new WordSet {InsiderWord = "Wielrennen", OutsiderWord = "Helm"},
    new WordSet {InsiderWord = "Bakken", OutsiderWord = "Recept"},
    new WordSet {InsiderWord = "Vliegeren", OutsiderWord = "Wind"},
    new WordSet {InsiderWord = "Hockey", OutsiderWord = "Stick"},
    new WordSet {InsiderWord = "Schaatsen", OutsiderWord = "IJsbaan"},
    new WordSet {InsiderWord = "Kermis", OutsiderWord = "Rit"},
    new WordSet {InsiderWord = "Quiz", OutsiderWord = "Vraag"},
    new WordSet {InsiderWord = "Kleding", OutsiderWord = "Maat"},
    new WordSet {InsiderWord = "Darten", OutsiderWord = "Pijlen"},
    new WordSet {InsiderWord = "Kaarten", OutsiderWord = "Stapel"},
    new WordSet {InsiderWord = "Terras", OutsiderWord = "Stoel"},
    new WordSet {InsiderWord = "Bier", OutsiderWord = "Schuim"},
    new WordSet {InsiderWord = "Wijn", OutsiderWord = "Glas"},
    new WordSet {InsiderWord = "Chocolade", OutsiderWord = "Reep"},
    new WordSet {InsiderWord = "Reizen", OutsiderWord = "Koffer"},
    new WordSet {InsiderWord = "Koken", OutsiderWord = "Ingrediënten"},
    new WordSet {InsiderWord = "Gaming", OutsiderWord = "Headset"},
    new WordSet {InsiderWord = "Piano", OutsiderWord = "Toetsen"},
    new WordSet {InsiderWord = "Tennis", OutsiderWord = "Racket"},
    new WordSet {InsiderWord = "Museum", OutsiderWord = "Galerij"},
    new WordSet {InsiderWord = "Kamperen", OutsiderWord = "Slaapzak"},
    new WordSet {InsiderWord = "Mountainbiken", OutsiderWord = "Trail"},
    new WordSet {InsiderWord = "DJ", OutsiderWord = "Mix"},
    new WordSet {InsiderWord = "Mode", OutsiderWord = "Outfit"},
    new WordSet {InsiderWord = "Zonsondergang", OutsiderWord = "Horizon"},
    new WordSet {InsiderWord = "Kitesurfen", OutsiderWord = "Golven"},
    new WordSet {InsiderWord = "Knuffel", OutsiderWord = "Kussen"},
    new WordSet {InsiderWord = "Astronomie", OutsiderWord = "Sterren"},
    new WordSet {InsiderWord = "Barbecue", OutsiderWord = "Kolengloed"},
    new WordSet {InsiderWord = "Popmuziek", OutsiderWord = "Hits"},
    new WordSet {InsiderWord = "Escape room", OutsiderWord = "Puzzel"},
    new WordSet {InsiderWord = "Puzzel", OutsiderWord = "Stukjes"},
    new WordSet {InsiderWord = "Karaoke", OutsiderWord = "Microfoon"},
    new WordSet {InsiderWord = "Fotobewerking", OutsiderWord = "Filters"},
    new WordSet {InsiderWord = "Speelhal", OutsiderWord = "Tokens"},
    new WordSet {InsiderWord = "Vriendengroep", OutsiderWord = "Appje"},
    new WordSet {InsiderWord = "Klimmen", OutsiderWord = "Touw"},
    new WordSet {InsiderWord = "Boogschieten", OutsiderWord = "Pijl"},
    new WordSet {InsiderWord = "Zandkastelen", OutsiderWord = "Emmer"},
    new WordSet {InsiderWord = "Drones", OutsiderWord = "Propellers"},
    new WordSet {InsiderWord = "Schilderen", OutsiderWord = "Doek"},
    new WordSet {InsiderWord = "Skateboarden", OutsiderWord = "Ramp"},
    new WordSet {InsiderWord = "Racen", OutsiderWord = "Bocht"},
    new WordSet {InsiderWord = "Paard", OutsiderWord = "Manen"},
    new WordSet {InsiderWord = "Zomer", OutsiderWord = "Zon"},
    new WordSet {InsiderWord = "Winter", OutsiderWord = "Sneeuw"},
    new WordSet {InsiderWord = "Herfst", OutsiderWord = "Bladeren"},
    new WordSet {InsiderWord = "Lente", OutsiderWord = "Bloesem"},
    new WordSet {InsiderWord = "Wandelen", OutsiderWord = "Paden"},
    new WordSet {InsiderWord = "Sushi", OutsiderWord = "Rijst"},
    new WordSet {InsiderWord = "Avocado", OutsiderWord = "Toast"},
    new WordSet {InsiderWord = "Yoga", OutsiderWord = "Mat"},
    new WordSet {InsiderWord = "Workout", OutsiderWord = "Gewichten"},
    new WordSet {InsiderWord = "Fitness", OutsiderWord = "Spieren"},
    new WordSet {InsiderWord = "Karten", OutsiderWord = "Helm"},
    new WordSet {InsiderWord = "Lasergamen", OutsiderWord = "Stralen"},
    new WordSet {InsiderWord = "Knutselen", OutsiderWord = "Schaar"},
    new WordSet {InsiderWord = "Theater", OutsiderWord = "Acteurs"},
    new WordSet {InsiderWord = "Hiken", OutsiderWord = "Rugzak"},
    new WordSet {InsiderWord = "Suppen", OutsiderWord = "Peddel"},
    new WordSet {InsiderWord = "Fotografie", OutsiderWord = "Lens"},
    new WordSet {InsiderWord = "Bioscoop", OutsiderWord = "Projector"},
    new WordSet {InsiderWord = "E-sports", OutsiderWord = "Toernooi"},
    new WordSet {InsiderWord = "Roeien", OutsiderWord = "Spanen"},
    new WordSet {InsiderWord = "Kampeerplek", OutsiderWord = "Vuurtje"},
    new WordSet {InsiderWord = "Stand-up comedy", OutsiderWord = "Grap"},
    new WordSet {InsiderWord = "Aquarel", OutsiderWord = "Kwast"},
    new WordSet {InsiderWord = "Hoepelen", OutsiderWord = "Cirkel"},
    new WordSet {InsiderWord = "Fietstocht", OutsiderWord = "Route"},
    new WordSet {InsiderWord = "Bungeejumpen", OutsiderWord = "Springen"},
    new WordSet {InsiderWord = "Surfen", OutsiderWord = "Plank"},
    new WordSet {InsiderWord = "Bergbeklimmen", OutsiderWord = "Rotsen"},
    new WordSet {InsiderWord = "Kayakken", OutsiderWord = "Stroomversnelling"},
    new WordSet {InsiderWord = "Museumbezoek", OutsiderWord = "Tentoonstelling"},
    new WordSet {InsiderWord = "Fotoboek", OutsiderWord = "Kaft"},
    new WordSet {InsiderWord = "Zeilen", OutsiderWord = "Mast"},
    new WordSet {InsiderWord = "Rolschaatsen", OutsiderWord = "Wieltjes"},
    new WordSet {InsiderWord = "Wielrennen", OutsiderWord = "Fietsketting"},
    new WordSet {InsiderWord = "Schilderij", OutsiderWord = "Lijst"},
    new WordSet {InsiderWord = "Tekenen", OutsiderWord = "Potlood"},
    new WordSet {InsiderWord = "Podcast", OutsiderWord = "Stem"},
    new WordSet {InsiderWord = "Fotowedstrijd", OutsiderWord = "Thema"},
    new WordSet {InsiderWord = "Skatepark", OutsiderWord = "Grindrail"},
    new WordSet {InsiderWord = "Zomerfestival", OutsiderWord = "Vuurwerk"},
    new WordSet {InsiderWord = "Drummen", OutsiderWord = "Stokken"},
    new WordSet {InsiderWord = "Tuinfeest", OutsiderWord = "Lampionnen"},
    new WordSet {InsiderWord = "Pretpark", OutsiderWord = "Attracties"},
    new WordSet {InsiderWord = "Carnaval", OutsiderWord = "Kostuum"},
    new WordSet {InsiderWord = "Strandwandeling", OutsiderWord = "Schelp"},
    new WordSet {InsiderWord = "Mountainbike", OutsiderWord = "Banden"},
    new WordSet {InsiderWord = "Straatkunst", OutsiderWord = "Muurschildering"},
    new WordSet {InsiderWord = "DJ-set", OutsiderWord = "Beats"},
    new WordSet {InsiderWord = "Pianospelen", OutsiderWord = "Muzieknoten"},
    new WordSet {InsiderWord = "Boetseren", OutsiderWord = "Klei"},
    new WordSet {InsiderWord = "Boekenclub", OutsiderWord = "Hoofdstuk"},
    new WordSet {InsiderWord = "Marktplaats", OutsiderWord = "Advertentie"},
    new WordSet {InsiderWord = "Schaaktoernooi", OutsiderWord = "Bord"},
    new WordSet {InsiderWord = "Zomerkamp", OutsiderWord = "Vriendschap"},
    new WordSet {InsiderWord = "Groepsfoto", OutsiderWord = "Lach"},
    new WordSet {InsiderWord = "Zangwedstrijd", OutsiderWord = "Stem"},
    new WordSet {InsiderWord = "Dronesport", OutsiderWord = "Camera"},
    new WordSet {InsiderWord = "Boogschutterij", OutsiderWord = "Doelwit"},
    new WordSet {InsiderWord = "Streetdance", OutsiderWord = "Moves"},
    new WordSet {InsiderWord = "Beachvolleybal", OutsiderWord = "Net"},
    new WordSet {InsiderWord = "Duiken", OutsiderWord = "Flippers"},
    new WordSet {InsiderWord = "Roadtrip", OutsiderWord = "Kaart"},
    new WordSet {InsiderWord = "Klimpark", OutsiderWord = "Harnas"},
    new WordSet {InsiderWord = "Bordspel", OutsiderWord = "Dobbelsteen"},
    new WordSet {InsiderWord = "Kamperen", OutsiderWord = "Vuur"},
    new WordSet {InsiderWord = "Streetfood", OutsiderWord = "Kraampje"},
    new WordSet {InsiderWord = "Rollenspel", OutsiderWord = "Verhaal"},
    new WordSet {InsiderWord = "Tuinieren", OutsiderWord = "Schep"},
    new WordSet {InsiderWord = "Zonnebaden", OutsiderWord = "Handdoek"},
    new WordSet {InsiderWord = "Hockeytoernooi", OutsiderWord = "Keeper"},
    new WordSet {InsiderWord = "Winterwandeling", OutsiderWord = "Sjaal"},
    new WordSet {InsiderWord = "Filmavond", OutsiderWord = "Banken"},
    new WordSet {InsiderWord = "Fotowandeling", OutsiderWord = "Natuur"},
    new WordSet {InsiderWord = "Boulderen", OutsiderWord = "Muur"},
    new WordSet {InsiderWord = "Airsoft", OutsiderWord = "Masker"},
    new WordSet {InsiderWord = "Kookclub", OutsiderWord = "Recepten"},
    new WordSet {InsiderWord = "Kerstmarkt", OutsiderWord = "Kraampjes"},
    new WordSet {InsiderWord = "Kampvuur", OutsiderWord = "Vlammen"},
    new WordSet {InsiderWord = "Horrornacht", OutsiderWord = "Schaduw"},
    new WordSet {InsiderWord = "Tekenwedstrijd", OutsiderWord = "Winnaar"},
    new WordSet {InsiderWord = "Winterfair", OutsiderWord = "Decoratie"},
    new WordSet {InsiderWord = "Paaseieren zoeken", OutsiderWord = "Mandje"},
    new WordSet {InsiderWord = "Gala", OutsiderWord = "Jurk"},
    new WordSet {InsiderWord = "Vliegvissen", OutsiderWord = "Lijn"},
    new WordSet {InsiderWord = "Geocaching", OutsiderWord = "Schat"},
    new WordSet {InsiderWord = "Jeu de boules", OutsiderWord = "Bal"},
    new WordSet {InsiderWord = "Zomervakantie", OutsiderWord = "Koffer"},
    new WordSet {InsiderWord = "Zwemles", OutsiderWord = "Slagen"},
    new WordSet {InsiderWord = "Lego bouwen", OutsiderWord = "Blokjes"},
    new WordSet {InsiderWord = "Modeltreinen", OutsiderWord = "Rails"},
    new WordSet {InsiderWord = "Parkour", OutsiderWord = "Muur"},
    new WordSet {InsiderWord = "Bungeejumpen", OutsiderWord = "Diepte"},
    new WordSet {InsiderWord = "Waterpolo", OutsiderWord = "Bal"},
    new WordSet {InsiderWord = "Schuimparty", OutsiderWord = "Bellen"},
    new WordSet {InsiderWord = "Streetfoodmarkt", OutsiderWord = "Geuren"},
    new WordSet {InsiderWord = "Videobewerking", OutsiderWord = "Clips"},
    new WordSet {InsiderWord = "Deltavliegen", OutsiderWord = "Vleugel"},
    new WordSet {InsiderWord = "Skimboarden", OutsiderWord = "Schuim"},
    new WordSet {InsiderWord = "Basketbalwedstrijd", OutsiderWord = "Dribbel"},
    new WordSet {InsiderWord = "Minigolf", OutsiderWord = "Putter"},
    new WordSet {InsiderWord = "Rolschaatsdisco", OutsiderWord = "Muziek"},
    new WordSet {InsiderWord = "Dierentuin", OutsiderWord = "Kooi"},
    new WordSet {InsiderWord = "Duinwandeling", OutsiderWord = "Zand"},
    new WordSet {InsiderWord = "Filmfestival", OutsiderWord = "Première"},
    new WordSet {InsiderWord = "Waterballonnengevecht", OutsiderWord = "Spetters"},
    new WordSet {InsiderWord = "Nachtzwemmen", OutsiderWord = "Sterren"},
    new WordSet {InsiderWord = "Trampolinespringen", OutsiderWord = "Sprongen"},
    new WordSet {InsiderWord = "Streetart", OutsiderWord = "Graffiti"},
    new WordSet {InsiderWord = "Haakclub", OutsiderWord = "Naald"},
    new WordSet {InsiderWord = "Graffitiworkshop", OutsiderWord = "Spuitbus"},
    new WordSet {InsiderWord = "Midzomerfeest", OutsiderWord = "Fakkels"},
    new WordSet {InsiderWord = "Historisch festival", OutsiderWord = "Kostuums"},
    new WordSet {InsiderWord = "Vuurwerkshow", OutsiderWord = "Knal"},
    new WordSet {InsiderWord = "Parachutespringen", OutsiderWord = "Val"},
    new WordSet {InsiderWord = "Snorkelen", OutsiderWord = "Visjes"},
    new WordSet {InsiderWord = "Klimhal", OutsiderWord = "Grepen"},
    new WordSet {InsiderWord = "Rivierwandeling", OutsiderWord = "Stenen"},
    new WordSet {InsiderWord = "Stadswandeling", OutsiderWord = "Gids"},
    new WordSet {InsiderWord = "Strandvolleybal", OutsiderWord = "Zand"},
    new WordSet {InsiderWord = "Koffieworkshop", OutsiderWord = "Bonen"},
    new WordSet {InsiderWord = "Mocktailbar", OutsiderWord = "Shaker"},
    new WordSet {InsiderWord = "Luchtballonvaart", OutsiderWord = "Mand"},
    new WordSet {InsiderWord = "Zandschulptoernooi", OutsiderWord = "Schep"},
    new WordSet {InsiderWord = "Paintball", OutsiderWord = "Masker"},
    new WordSet {InsiderWord = "Parkfestival", OutsiderWord = "Dekens"},
    new WordSet {InsiderWord = "Laserlightshow", OutsiderWord = "Stralen"},
    new WordSet {InsiderWord = "Goochelen", OutsiderWord = "Kaarten"},
    new WordSet {InsiderWord = "Magische show", OutsiderWord = "Illusie"},
    new WordSet {InsiderWord = "Circus", OutsiderWord = "Clown"},
    new WordSet {InsiderWord = "Ritje in het reuzenrad", OutsiderWord = "Uitzicht"},
    new WordSet {InsiderWord = "Sterrenkijken", OutsiderWord = "Telescoop"},
    new WordSet {InsiderWord = "Vlotvaren", OutsiderWord = "Hout"},
    new WordSet {InsiderWord = "Weerwolvenspel", OutsiderWord = "Kaarten"},
    new WordSet {InsiderWord = "Kaarsen maken", OutsiderWord = "Was"},
    new WordSet {InsiderWord = "Kringspel", OutsiderWord = "Cirkel"},
    new WordSet {InsiderWord = "Fotomarathon", OutsiderWord = "Tijdslimiet"},
    new WordSet {InsiderWord = "Strandjutten", OutsiderWord = "Schatten"},
    new WordSet {InsiderWord = "Zeekajak", OutsiderWord = "Golf"},
    new WordSet {InsiderWord = "Lantaarnoptocht", OutsiderWord = "Lichtjes"},
    new WordSet {InsiderWord = "Kanoën", OutsiderWord = "Peddel"},
    new WordSet {InsiderWord = "Paardenrennen", OutsiderWord = "Finish"},
    new WordSet {InsiderWord = "Zandbakspelen", OutsiderWord = "Vormpjes"},
    new WordSet {InsiderWord = "Picknick", OutsiderWord = "Mand"},
    new WordSet {InsiderWord = "Luchtkussenfeest", OutsiderWord = "Springen"},
    new WordSet {InsiderWord = "Fotocollage", OutsiderWord = "Plakboek"},
    new WordSet {InsiderWord = "Vliegerwedstrijd", OutsiderWord = "Koord"},
    new WordSet {InsiderWord = "Vogels spotten", OutsiderWord = "Verrekijker"},
    new WordSet {InsiderWord = "Lampionnenfestival", OutsiderWord = "Schijnsel"},
    new WordSet {InsiderWord = "Avonturenpark", OutsiderWord = "Klimmen"}
};

    public static WordSet GetRandomSet()
    {
        int index = random.Next(sets.Count);
        return sets[index];
    }

}
public class WordSet
{
    public string InsiderWord { get; set; }
    public string OutsiderWord { get; set; }

    public override string ToString()
    {
        return $"InsiderWord: {InsiderWord}, OutsiderWord: {OutsiderWord}";
    }
}

# Blockchain

## Kaip paleisti?

Blockchain'as yra parašytas su c#, todėl papraščiausia būtų atsisiųsti Visual Studio ir užkrauti projektą per jį. Užkrovus spausti *start* ir blockchainas bus pradedamas kurti.

## Kas vyksta?

Pirmiausia yra sugeneruojama 1000 user'ų, 10 miner'ų ir sugeneruojamos transakcijų pool'as. Iš transakcijų poolo imama po 150 transakcijų, blokas yra kasamas ir atkasęs mineris "apdovanojamas" tam tikru rewardu, šiuo atveju 1 valiuta.

![alt text](https://github.com/benaspuckus/Hash-Function/blob/master/Capture.JPG)

Kol vyksta kasimas, konsolėje yra matoma likusių transakcijų skaičius, perspėjama jei kas bandė "pervesti" daugiau, nei turėjo ir su kiek iteracijų pavyko iškasti bloką (pradedama nuo 500)

Susitvarkius su visomis transakcijomis yra parodomas visų user'ų ir simuliuotų "minerių" balansas:
#### Useriai
![alt text](https://github.com/benaspuckus/Hash-Function/blob/master/Capture2.JPG)
#### Mineriai
![alt text](https://github.com/benaspuckus/Hash-Function/blob/master/Capture3.JPG)

# Hash-Function

## Idėja

Kadangi buvo patartina neskaityti kito hash'avimo pseudo ar bet kokio kito kodo, manasis gavosi liaudiškai tariant "bootleg'as". Funkcija "važiuoja" iteracija paremtu skaičiavimu t.y su kiekvienu vykdomu veiksmu, ar ciklo iteracija daugiklio reikšmė didėja po +1 arba kita *įhardcode'inta* reikšme.

## Kas po kapotu?

- Sukuriamas pradinis hash'as iš 32 baitų, o kiekvienam baitui priskiriama dešimtainėje sistemoje atitinkantis skaičius 48 (priskiriamas 0)
- 32 baitų masyvas yra padalinamas į 4 dalis (kad maišos funckija atrodytų labiau atsitiktinė). 
- Kiekvienam 8 baitų masyvui yra paduodamas string'as, kuriame yra išsaugotas visas įvestas tekstas.
- Ciklas "eina" per kiekvieną raidę, o jo viduje dar vienas ciklas "eina" per pirmus 8 masyvo baitus. Prie esamos masyvo reikšmės(dešimtainės) yra pridedama dešimtainė "einamos" raidės reikšmė ir padauginama iš daugiklio (pradedamas skaičiuoti nuo 1)
- Jeigu baito dešimtainė reikšmė atitinka intervalus [48,57]&&[65,90]&&[97,122] (Kas yra mažosios, didžiosios raidės ir skaičiai)
  - Baito reikšmė yra paliekama ir einama prie kito baito
  - Pradedamas vykdyti ciklas, kol baitas patenka į minėtus intervalus, taigi vėl yra dauginamas iš daugiklio, tačiau šįkart simbolio reikšmė nėra dauginama 
- Yra einama prie kito 8 baitų masyvo, daugiklio reikšmė pasiliekia tokia, kokia buvo (jau nebe 1)
- Galų gale visi 4 masyvai yra sudedami į vieną, baitai paverčiami į string'ą ir išvedamas rezultatas

## Tyrimų rezultatai

Visi naudoti failai yra *įpush'inti* kartu su kodu. Algoritmui įgyvendinti naudojau .net platformą, c# kalbą. Visi reikalaujami unit test'ai taip pat yra kode įkelti kaip atskiri projektai. Taip pad juos leidžiant visi bandymų rezultatai yra įrašomi ir į failą. Bandymų metu visi testai *pass*'ino.

### Tyrimo skaičiavimai


#### 2 failai su 100 000 simbolių ir 1 simbolis skirtingas
| Failo pavadinimas | Gautas Hash kodas |Ar testas sėkmingas?|
| :--------------------: | :------------------------------: | :--: |
| manySymbols1Difference.txt | 7epAZAphrLPeeID5qSA6CeIEBepUuA1z  | |
| manySymbols1Difference2.txt | RI8beqniuE2VBJt1zRx0EBerBPjAfEVq  |PASS|

#### 2 failai su 100 000 skirtingų simbolių
| Failo pavadinimas | Gautas Hash kodas |Ar testas sėkmingas?|
| :--------------------: | :------------------------------: | :--: |
| randomSymbols.txt | iC5eea0aZa9ARuLb2a6R477DuEA6XJrV | |
| randomSymbols2.txt | dv6k2mC0UEc51auLE5hn1xeeri9ahxbz |PASS|

#### 2 failai su 1 skirtingu simboliu
| Failo pavadinimas | Gautas Hash kodas |Ar testas sėkmingas?|
| :--------------------: | :------------------------------: | :--: |
| oneSymbol.txt | jZAScq0ZXJ6iFhwVKIJumsrbBAZR7tqY  | |
| oneSymbol2.txt | p8Ev1E8iuSENeIDIhhpxJ2Aa5urDuQ82  |PASS|

#### Konstitucijos skaitymas
| Gautas laikas |
| :--------------------: |
| 209 ms |

#### Rasta tokių pačių hash'ų tarp milijono porų string'ų
| Porų skaičius | Ar testas sėkmingas|
| :--------------------: | :------------------------------: |
| 0 | PASS |

#### Rasta tokių pačių hash'ų tarp 100 000 porų string'ų su 1 simboliu skirtingu
| Porų skaičius | Ar testas sėkmingas|Skirtingumas|
| :--------------------: | :------------------------------: |:----------: |
| 0 | PASS | Min - 0, Max - 0, Average - 0|

#### Note
Šita hash funkcija "atrodo" labai random, tačiau taip "atrodo" dėl to, jog baitų masyvas nėra konvertuojamas į šešioliktainį kodą, o tikrinamas dešimtainio kodo reikšmių lygmenyje, todėl dažnai "neapataikius" į intervalą, hasho baitų reikšmės yra perskaičiuojomos ir atrodo, kad algoritmas labai random.




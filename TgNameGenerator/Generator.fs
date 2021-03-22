module TgNameGenerator.Generator

open System

type GeneratedNameInfo =
    { Name: string
      PartDescriptions: (string * string) []
      SuffixDescription: (string * string) option }

let private parts =
    [|("Ael", "рыцарь")
      ("Aer", "закон, порядок")
      ("Af", "кольцо")
      ("Ah", "хитрый")
      ("Al", "море, океан")
      ("Am", "лебедь")
      ("Ama", "прекрасный")
      ("An", "рука")
      ("Ang", "блеск, блестеть")
      ("Ansr", "руна")
      ("Ar", "золото, золотой")
      ("Arм", "серебро, серебрянный")
      ("Arn", "юг")
      ("Aza", "жизнь, живой")
      ("Bael", "защитник")
      ("Bes", "клятва")
      ("Cael", "лучник, стрела")
      ("Cal", "вера")
      ("Cas", "геральд")
      ("Cla", "роза")
      ("Cor", "легенда, легендарный")
      ("Cy", "оникс")
      ("Dae", "белый")
      ("Dho", "сокол")
      ("Dre", "собака")
      ("Du", "месяц (половинка луны)")
      ("Eil", "голубой, синий")
      ("Eir", "острый")
      ("El", "зеленый")
      ("Er", "кабан, боров")
      ("Ev", "олень")
      ("Fera", "чемпион")
      ("Fi", "река")
      ("Fir", "темный")
      ("Fis", "светлый")
      ("Gael", "pegasus")
      ("Gar", "сова")
      ("Gil", "гриффон")
      ("Ha", "свободный, свобода")
      ("Hu", "лошадь")
      ("Ia", "день")
      ("Il", "туман")
      ("Ja", "посох")
      ("Jar", "голубь")
      ("Ka", "дракон")
      ("Kan", "орел")
      ("Ker", "заклятие")
      ("Keth", "ветер")
      ("Koeh", "земля")
      ("Kor", "черный")
      ("Ky", "рубин")
      ("La", "ночь")
      ("Laf", "луна")
      ("Lam", "восток")
      ("Lue", "загадка")
      ("Ly", "волк")
      ("Mai", "смерть, убийца")
      ("Mal", "война")
      ("Mara", "священник")
      ("My", "изумруд")
      ("Na", "древний")
      ("Nai", "дуб")
      ("Nim", "глубокий")
      ("Nu", "надежда, надеющийся")
      ("Ny", "алмаз")
      ("Py", "сапфир")
      ("Raer", "единорог")
      ("Re", "медведь")
      ("Ren", "запад")
      ("Rhy", "нефрит")
      ("Ru", "мечта")
      ("Rua", "звезда")
      ("Rum", "луг")
      ("Rid", "копье")
      ("Sae", "лес")
      ("Seh", "мягкий")
      ("Sel", "высокий")
      ("Sha", "солнце")
      ("She", "век, время")
      ("Si", "кот, кошка")
      ("Sim", "север")
      ("Sol", "история, память")
      ("Sum", "вода")
      ("Syl", "faerie нечто волшебное, предположительно фея")
      ("Ta", "лиса")
      ("Tahl", "клинок")
      ("Tha", "бдительный")
      ("Tho", "правдивый, истинный, правда")
      ("Ther", "небо")
      ("Thro", "знание, мудрец")
      ("Tia", "магия")
      ("Tra", "дерево")
      ("Ty", "кристал")
      ("Uth", "волшебник")
      ("Ver", "мир")
      ("Vil", "палец, указание")
      ("Von", "лед")
      ("Ya", "мост, дорога, путь")
      ("Za", "королевский")
      ("Zy", "цвет слоновой кости") |]

let private suffixes =
    [|"ae", "шепот, шепчущий"
      "nae", "шепот, шепчущий"
      "aer", "певец, песнь, звук, поющий"
      "aera", "певец, песнь, звук, поющий"
      "en", "осень"|]
    
let random = Random()
    
let getRandom arr =
    let index = random.Next(Array.length arr)
    arr.[index]
    
let capitalizeOnlyFirst (str: string) =
    (Char.ToUpperInvariant (str.[0])).ToString() + str.Substring(1).ToLowerInvariant()
    
let private nameGenerator parts suffixOpt =
    let partsName =
        parts
        |> Array.map fst
        |> String.concat ""
        
    match suffixOpt with
    | Some (suffix, _) ->
        partsName + suffix
    | None ->
        partsName
    |> capitalizeOnlyFirst
    
let generate() =
    let partsCount = random.Next(1, 2)
    let suffixExists = random.Next(2) = 0
    
    let partDescriptions =
        Array.map (fun _ -> getRandom parts) [|0..partsCount|]
        
    let suffixDescription =
        if suffixExists then
            Some <| getRandom suffixes
        else
            None
    
    { Name = nameGenerator partDescriptions suffixDescription
      PartDescriptions = partDescriptions
      SuffixDescription = suffixDescription }
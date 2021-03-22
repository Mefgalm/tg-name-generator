open System
open System.Text.RegularExpressions
open Funogram
open Funogram.Api
open Funogram.Telegram.Bot
open Funogram.Telegram.Types
open Funogram.Telegram.Api
open Funogram.Telegram.Bot
open Funogram.Telegram.Types
open TgNameGenerator
open FsToolkit.ErrorHandling
open TgNameGenerator.Generator
open System.IO

//
//Суффиксы
//
//{1} -ae (-nae)  шепот, шепчущий
//{2} -ael  великий
//{3} -aer / -aera  певец, песнь, звук, поющий
//{4} -aias / -aia  муж / жена
//{5} -ah / -aha  палочка, жезл
//{6} -aith / -aira  дом
//{7} -al / -ala (-la; -lae; -llae)  гармония
//{8} -ali  тень
//{9} -am / -ama  strider, бродяжник, бродяга, бродящий
//{10} -an / -ana (-a; -ani; -uanna)  творить, творец, создатель
//{11} -ar / -ara (-ra)  мужчина / женщина
//{12} -ari (-ri)  весна
//{13} -aro (-ro)  лето
//{14} -as (-ash; -sah)  лук
//{15} -ath  by, of, with (думаю понятно)
//{16} -avel  меч
//{17} -brar (-abrar; -ibrar)  ремесло, ремесленник
//{18} -dar (-adar; -odar)  мир
//{19} -deth (-eath; -eth)  вечный
//{20} -dre  очарование, чарующий
//{21} -drim (-drimme; -udrim)  полет, летающий
//{22} -dul  лужайка, поляна
//{23} -ean  езда, всадник
//{24} -el (ele / -ela)  ястреб
//{25} -emar  честь
//{26} -en  осень
//{27} -er (-erl; -ern)  зима
//{28} -ess (-esti)  эльфийский
//{29} -evar  флейта
//{30} -fel (-afel; -efel)  озеро
//{31} -hal (-ahal; -ihal)  бледный, слабый
//{32} -har (-ihar; -uhar)  мудрость, мудрый
//{33} -hel (-ahel; -ihel)  печаль, слезы, печальный
//{34} -ian / ianna (-ia; -ii; -ion)  лорд / леди
//{35} -iat  огонь
//{36} -ik  сила, мощь, сильный, мощный
//{37} -il (-iel; -ila; -lie)  подарок, подающий
//{38} -im  долг
//{39} -in (-inar; -ine)  родня, брат / сестра
//{40} -ir (-ira; -ire)  сумерки
//{41} -is (-iss; -ist)  свиток
//{42} -ith (-lath; -lith; -lyth)  дитя, юный
//{43} -kash (-ashk; -okash)  судьба
//{44} -ki  пустота
//{45} -lan / -lanna (-lean; -olan / -ola)  сын / дочь
//{46} -lam (-ilam; -ulam)  fair, справделивый, прекрасный
//{47} -lar (-lirr)  сияние, сияющий
//{48} -las  дикий
//{49} -lian / -lia  хозяин, госпожа
//{50} -lis (-elis; -lys)  бриз
//{51} -lon (-ellon)  вождь, правитель
//{52} -lyn (-llinn; -lihn)  болт, луч
//{53} -mah / -ma (-mahs)  маг
//{54} -mil (-imil; -umil)  обязательство, обещание, обещающий
//{55} -mus  союзник, компаньен
//{56} -nal (-inal; -onal)  даль, далекий, отдаленный
//{57} -nes  сердце
//{58} -nin (-nine; -nyn)  обряд, ритуал
//{59} -nis (-anis)  рассвет
//{60} -on/onna хранить, хранитель
//{61} -or (oro) цветок
//{62} -oth (-othi)  врата
//{63} -que  потерянный, забытый
//{64} -quis  конечность, часть чего-то, ветвь
//{65} -rah(-rae; -raee)  зверь
//{66} -rad(-rahd)  лист
//{67} -rail/-ria (-aral; -ral; -ryl)  охота, охотник
//{68} -ran (-re; -reen)  заточение, кандалы, оковы
//{69} -reth (-rath)  тайный
//{70} -ro (-ri; -ron)  путь, путешествие, идущий, странник
//{71} -ruil (-aruil; -eruil)  благородный
//{72} -sal (-isal; -sali)  мёд, медовый, сладкий, нежный
//{73} -san  питье, напиток, вино
//{74} -sar (-asar; -isar)  задание, поиск, ищущий
//{75} -sel (-asel; -isel)  гора
//{76} -sha (-she; -shor)  океан
//{77} -spar  кулак
//{78} -tae (-itae)  возлюбленный, любовь
//{79} -tas (-itas)  стена, опека, ограда
//{80} -ten (-iten)  прядильщик; прядильщица
//{81} -thal /-tha (-ethal / -etha)  лечение, лечить, целитель
//{82} -thar (-ethar; -ithar)  друг
//{83} -ther (-ather; -thir)  броня, защита, покровительство
//{84} -thi (-ethil; -thil)  крыло, крылатый
//{85} -thus /-thas (-aethus / -aethas)  арфа, арфист
//{86} -ti (-eti;-til)  глаз, взгляд
//{87} -tril /-tria (-atri; -atril / -atria)  танец, танцор
//{88} -ual (-lua)  святой
//{89} -uath (-luth; -uth)  копье
//{90} -us /-ua  родственник, родственный
//{91} -van /-vanna  чаща, лес
//{92} -var /-vara (-avar / -avara) отец / мать
//{93} -vain (-avain)  дух
//{94} -via (-avia)  удача, удачливый
//{95} -vin (-avin)  шторм
//{96} -wyn  музыка, музыкант
//{97} -ya  шлем
//{98} -yr / -yn  вестник
//{99} -yth  народ, персона
//{100} -zair /-zara (-azair / -ezara)  молния


let private sendMessage config chatId text =
    text
    |> sendMessage chatId
    |> api config
    |> Async.RunSynchronously
    |> ignore

let lineFormat = sprintf "%s - %s"

let generatedNameInfoDetailsView (generateNameInfo: GeneratedNameInfo) =
    let partsBlock =
        generateNameInfo.PartDescriptions
        |> Array.map (fun (part, desc) -> lineFormat part desc)
        |> String.concat "\n"

    let suffixBlock =
        match generateNameInfo.SuffixDescription with
        | Some (suffix, desc) -> lineFormat suffix desc
        | None -> ""

    $"{generateNameInfo.Name}

{partsBlock}
{suffixBlock}"

let onGenerate (context: UpdateContext) =
    option {
        let! message = context.Update.Message
        let chatId = message.Chat.Id

        let nameMessage =
            Generator.generate ()
            |> generatedNameInfoDetailsView

        sendMessage context.Config chatId nameMessage
    }
    |> ignore

let onRoll (context: UpdateContext) =
    option {
        let! message = context.Update.Message
        let chatId = message.Chat.Id
        
        let rollD6 = (Roll.sd6()).ToString()

        sendMessage context.Config chatId rollD6
    }
    |> ignore

let onUpdate (context: UpdateContext) =
    processCommands
        context
        [ cmd "/generate" onGenerate
          cmd "/roll" onRoll]
    |> ignore


[<Literal>]
let tokenFileName = "token.txt"

let loadToken() =
    if File.Exists tokenFileName then
        File.ReadAllText tokenFileName
    else
        failwithf
            "File with Telegram token not found! Please create file with name '%s' in root folder"
            tokenFileName
        

[<EntryPoint>]
let main argv =
    let token = loadToken()
    startBot
        { defaultConfig with
              Token = token }
        onUpdate
        None
        |> Async.RunSynchronously
        
    0

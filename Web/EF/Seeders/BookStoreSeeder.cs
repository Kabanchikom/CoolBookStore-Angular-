using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Account.Models;
using Web.Areas.Author.Models;
using Web.Areas.Cart.Models;
using Web.Areas.Manufacturer.Models;
using Web.Areas.Product.Models;

namespace Web.EF.Seeders;

/// <summary>
/// Тестовые данные для модели предметной области.
/// </summary>
public class BookStoreSeeder
{
    public async Task SeedAsync(IApplicationBuilder app, WebApplicationBuilder builder)
    {
        var context = app.ApplicationServices.GetRequiredService<BookStoreDbContext>();
        var userManager = app.ApplicationServices.GetRequiredService<UserManager<User>>();

        await context.Database.MigrateAsync();

        if (await context.Books.AnyAsync())
        {
            return;
        }
        
        var users = CreateUsers();
        
        foreach (var user in users)
        {
            await userManager.CreateAsync(user);
        }

        var products = CreateProducts();
        var cartLines = CreateCartLines(users, products);
        
        await context.AddRangeAsync(products);
        await context.AddRangeAsync(cartLines);
        
        await context.SaveChangesAsync();
    }

    private List<User> CreateUsers()
    {
        var users = new List<User>();
        var passwordHasher = new PasswordHasher<User>();

        #region Content

        var user = new User
        {
            UserName = "test@test.test",
            Email = "test@test.test",
            FirstName = "Пользователь",
            LastName = "Тестовый",
            DateOfBirth = new DateTime(2000, 5, 19),
        };
        user.PasswordHash = passwordHasher.HashPassword(user, "Qwerty12345+");
        users.Add(user);

        #endregion

        return users;
    }

    private List<ProductCard> CreateProducts()
    {
        var manufacturers = CreateManufacturers();
        var authors = CreateAuthors();

        var products = new List<ProductCard>();

        #region Content

        products.Add(new StationeryCard
        {
            Name = "Ручка шариковая Luxury Super Flex",
            ThumbImgSrc = "/img/pencil.webp",
            ImgSrc = "/img/pencil.webp",
            ReceiptDate = new DateTime(2017, 5, 2),
            StationeryType = StationeryType.Pen,
            OldPrice = 1999.00m,
            Rating = 4.75m,
            LongDescription = "В комплекте: стержень, гарантийный талон с инструкцией, подарочная коробка.",
            Manufacturer = manufacturers.First(x => x.Name == "Вологодский ручечный завод"),
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Пикник на обочине",
            ThumbImgSrc = "/img/abs_strugatskie_piknik_na_obochine.jpeg",
            ImgSrc = "/img/abs_strugatskie_piknik_na_obochine.jpeg",
            ReceiptDate = new DateTime(2017, 7, 1),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 219.00m,
            NewPrice = 175.00m,
            Rating = 4.75m,
            LongDescription = "Пожалуй, в истории современной мировой фантастики найдется не так много произведений," +
                              " которые оставались бы популярными столь долгое время. Повесть послужила основой культового" +
                              " фильма Тарковского «Сталкер»; через три десятилетия появились не менее культовая" +
                              " компьютерная игра с тем же названием и целая серия повестей и романов," +
                              " написанных с использованием мира «Пикника».",
            Manufacturer = manufacturers.First(x => x.Name == "Издательство АСТ"),
            Authors = authors.Where(x =>
                    x.Surname == "Стругацкий"
                    && x.Patronimic == "Натанович")
                .ToList(),
            Genres = BookGenre.Fiction | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Архипелаг Гулаг",
            ThumbImgSrc = $"/img/archipelag_gulag.jpg",
            ImgSrc = $"/img/archipelag_gulag.jpg",
            ReceiptDate = new DateTime(2018, 6, 2),
            BookTypes = BookCard.BookType.Ebook,
            OldPrice = 99.00m,
            NewPrice = 89.00m,
            Rating = 4.3m,
            IsOnSale = true,
            LongDescription = "Александр Солженицын — выдающийся русский писатель XX века, классик отечественной" +
                              " литературы, лауреат Нобелевской премии («За нравственную силу, с которой он продолжил" +
                              " традиции великой русской литературы», 1970), лауреат Государственной премии Российской" +
                              " Федерации за выдающиеся достижения в области гуманитарной деятельности (2006), академик" +
                              " Российской академии наук..В настоящем издании представлен «Архипелаг ГУЛАГ» — всемирно" +
                              " известная документально-художественная эпопея о репрессиях в годы советской власти. ",
            Manufacturer = manufacturers.First(x => x.Name == "Дом Книги"),
            Authors = authors.Where(x =>
                    x.Name == "Александр"
                    && x.Patronimic == "Исаевич"
                    && x.Surname == "Солженицын")
                .ToList(),
            Genres = BookGenre.Classics | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Государственность и Анархия",
            ThumbImgSrc = "/img/bakunin_gosudarstvennost_i_anarhia.jpg",
            ImgSrc = "/img/bakunin_gosudarstvennost_i_anarhia.jpg",
            ReceiptDate = new DateTime(2018, 6, 2),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 529.00m,
            NewPrice = 429.00m,
            Rating = 4.12m,
            IsOnSale = true,
            LongDescription = "Михаил Александрович Бакунин (1814-1876) - видный философ и общественный деятель, один" +
                              " из основателей и идеологов анархизма, ярчайшая звезда на небосклоне европейского" +
                              " революционного движения..М.А. Бакунин прожил удивительную, полную идейных поисков" +
                              " жизнь: он участвовал в восстаниях в Праге, Дрездене и Польше; провел несколько лет" +
                              " в ссылке в Сибири; встречался с выдающимися людьми своего времени - Марксом," +
                              " Гарибальди, Герценом, Тургеневым. Провозгласив, что его " +
                              "\"отечество - всемирная революция\", Бакунин выступал против порабощения народа" +
                              " бюрократическим аппаратом и обосновывал необходимость бунта \"против организованного" +
                              " грабежа и утеснения - против государства\". Так на свет появилась" +
                              " \"Государственность и анархия\" - манифест анархизма и основа бакунинского учения" +
                              " о возможности перестройки жизни на началах свободы, равенства и справедливости.",
            Manufacturer = manufacturers.First(x => x.Name == "Дом Книги"),
            Authors = authors.Where(x =>
                    x.Name == "Михаил"
                    && x.Patronimic == "Александрович"
                    && x.Surname == "Бакунин")
                .ToList(),
            Genres = BookGenre.Philosophy | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Книга символов",
            ThumbImgSrc = "/img/balmont_kniga_simvolov.jpg",
            ImgSrc = "/img/balmont_kniga_simvolov.jpg",
            ReceiptDate = new DateTime(2015, 4, 3),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 749.00m,
            NewPrice = 699.00m,
            Rating = 4.67m,
            LongDescription = "Константин Дмитриевич Бальмонт - русский поэт Серебряного века, переводчик и эссеист," +
                              " один из виднейших представителей русского символизма, получивший всероссийскую" +
                              " и мировую известность.\"Книга символов\" - сборник стихотворений, наполненных" +
                              " пророческими нотками, мотивами \"солнечности\", стремлениями к постоянному обновлению." +
                              " Считающаяся сильнейшей в литературном наследии поэта, эта книга, несомненно, запомнится" +
                              " читателю и позволит услышать несмолкаемый Голос Вечности!",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Константин"
                    && x.Patronimic == "Дмитриевич"
                    && x.Surname == "Бальмонт")
                .ToList(),
            Genres = BookGenre.Classics | BookGenre.Poetry,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Дракула",
            ThumbImgSrc = "/img/brem_stoker_drakula.jpg",
            ImgSrc = "/img/brem_stoker_drakula.jpg",
            ReceiptDate = new DateTime(2017, 5, 13),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 419.00m,
            NewPrice = 399.00m,
            Rating = 4.55m,
            IsOnSale = true,
            LongDescription = "Главное детище Брэма Стокера, вампир-аристократ, ставший эталоном для последующих" +
                              " сочинений, причина массового увлечения «вампирским» мифом и получивший массовое" +
                              " же воплощение – от литературы до аниме и видеоигр.«…прочел я «Вампира — графа Дракула»." +
                              " Читал две ночи и боялся отчаянно. Потом понял еще и глубину этого, независимо от" +
                              " литературности и т.д. <…> Это — вещь замечательная и неисчерпаемая, благодарю тебя" +
                              " за то, что ты заставил меня, наконец, прочесть ее».А. А. Блок" +
                              " из письма Е. П. Иванову от 3 сентября 1908 г. ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Брэм"
                    && x.Surname == "Стокер")
                .ToList(),
            Genres = BookGenre.Novel | BookGenre.Classics | BookGenre.Horror | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Бусидо",
            ThumbImgSrc = "/img/busido.jpg",
            ImgSrc = "/img/busido.jpg",
            ReceiptDate = new DateTime(2020, 7, 1),
            BookTypes = BookCard.BookType.Ebook,
            OldPrice = 99.00m,

            Rating = 4.15m,
            LongDescription = "Инадзе Нитобэ родился в знаменитой самурайской семье в префектуре Мариока," +
                              " но, несмотря на это, всегда был близок к западной культуре.\"Я начал писать статью" +
                              " о Бусидо, в которой хочу показать, что в этих Заповедях Рыцарства раскрывается сущность" +
                              " японского характера и содержится ключ к пониманию морального духа японцев\"," +
                              " — пишет к Уильяму Гриффису, автору многих книг о Японии, Инадзе Нитобэ.С началом" +
                              " русско-японской войны дополненное издание книги стало бестселлером" +
                              " и присвоило Нитобэ статус \"публициста, выступающего от имени Японии\" —" +
                              " культурного посредника между Японией и Западом. ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Инандзо"
                    && x.Surname == "Нитобэ")
                .ToList(),
            Genres = BookGenre.Philosophy | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Божественная комедия",
            ThumbImgSrc = "/img/dante_bozestvennaya_komediya.jpg",
            ImgSrc = "/img/dante_bozestvennaya_komediya.jpg",
            ReceiptDate = new DateTime(2021, 2, 20),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 1299.00m,

            Rating = 4.35m,
            LongDescription = "\"Божественная комедия\" - грандиозный памятник поэтической культуры и" +
                              " настоящая энциклопедия средневекового мировоззрения. В ней поэт совершает" +
                              " путешествие через три царства загробного мира и с удивительной наглядностью" +
                              " и ясностью изображения дает живую, запоминающуюся картину происходящего там." +
                              " Простой народ воспринимал поэму Данте буквально. Боккаччо рассказывал о двух" +
                              " жительницах Вероны, которые, заметив проходившего мимо Данте, обменялись" +
                              " многозначительными репликами. \"Посмотри, - сказала одна, - вот тот, кто" +
                              " спускается в Ад и, возвращаясь оттуда, когда пожелает, приносит весть о" +
                              " пребывающих там грешниках. Другая ответила: \"Должно быть, ты права: посмотри," +
                              " как борода его курчава, и лицо его черно от дыма и копоти адского огня\"." +
                              " В наши же дни историки и критики до сих пор не прекращают споры о том," +
                              " чем является это великое произведение: \"путеводителем\" по загробному" +
                              " миру или попыткой познать непознаваемое, найти рациональное в иррациональном," +
                              " показать людям путь от мрака и скорби к свету и радости. ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Данте"
                    && x.Surname == "Алигьери")
                .ToList(),
            Genres = BookGenre.Poetry | BookGenre.Classics,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Гиперион",
            ImgSrc = "/img/den_simmons_hyperion.jpg",
            ThumbImgSrc = "/img/den_simmons_hyperion.jpg",
            ReceiptDate = new DateTime(2022, 2, 20),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 2095.00m,
            NewPrice = 1495.00m,
            Rating = 4.12m,
            IsOnSale = true,
            LongDescription = "Священник, Солдат, Поэт, Ученый, Детектив, Консул отправляются на планету" +
                              " Гиперион, в паломничество к таинственным Гробницам Времени, охраняемым" +
                              " кровавым убийцей Шрайком, мистическим полубожеством, святым непризнанного" +
                              " культа. Именно там могут исполниться их сокровенные желания, ставшие смыслом" +
                              " их жизни... Так начинается одна из великолепнейших саг в истории фантастики," +
                              " охватывающая все сопутствующие жанры – от космической оперы до хоррора. ",
            Manufacturer = manufacturers.First(x => x.Name == "Дом Книги"),
            Authors = authors.Where(x =>
                    x.Name == "Дэн"
                    && x.Surname == "Симмонс")
                .ToList(),
            Genres = BookGenre.Fiction | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Потерянный рай",
            ImgSrc = "/img/djon_milton_poteryaniy_rai.jpg",
            ThumbImgSrc = "/img/djon_milton_poteryaniy_rai.jpg",
            ReceiptDate = new DateTime(2022, 2, 20),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 199.00m,

            Rating = 4.23m,
            LongDescription = "Джон Мильтон — один из величайших поэтов Англии, «титан по силе мысли, страсти и" +
                              " характеру, по многосторонности и учености». Во всей мощи его поэтическое мастерство" +
                              " проявилось в созданной им поэме «Потерянный рай» (1663), белый стих которой как образец" +
                              " совершенства стоит рядом с белым стихом драм Шекспира.",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Джон"
                    && x.Surname == "Мильтон")
                .ToList(),
            Genres = BookGenre.Classics | BookGenre.Poetry,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Фауст",
            ImgSrc = "/img/gete_faust.jpg",
            ThumbImgSrc = "/img/gete_faust.jpg",
            ReceiptDate = new DateTime(2014, 11, 11),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 1199.00m,
            NewPrice = 1200.00m,
            Rating = 4.87m,
            IsOnSale = true,
            LongDescription = "Легенда об ученом Иоганне Фаустусе, продавшем Мефистофелю душу в обмен на его" +
                              " обещание открыть секреты мироздания, показать бездны ада и рая, обогатить новым знанием," +
                              " давно волновала людское воображение. Так доктор Фауст, которому щедро приписывались" +
                              " всевозможные чудеса, стал героем немецкого народа. Великий чернокнижник был персонажем" +
                              " театральных представлений, к его образу обращались многие авторы, но именно под пером" +
                              " Гёте эта история стала одной из подлинных вершин мировой литературы. \"Фауст\"" +
                              " Гёте задает загадки, и уже не одно поколение читателей пытается их разгадать." +
                              " Настоящее издание содержит признанный и наиболее точный перевод Н.А. Холодковского," +
                              " за который в 1917 г. Российской Академией наук ему была присуждена Пушкинская премия. ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Иоган"
                    && x.Patronimic == "Вольфганг"
                    && x.Surname == "Гете")
                .ToList(),
            Genres = BookGenre.Classics | BookGenre.Poetry,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Замок",
            ImgSrc = "/img/kafka-zamok.jpg",
            ThumbImgSrc = "/img/kafka-zamok.jpg",
            ReceiptDate = new DateTime(2012, 1, 10),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 199.00m,

            Rating = 4.12m,
            LongDescription =
                "\"Замок\" — одно из самых знаменитых произведений Франца Кафки и один из самых глубоких" +
                " философских романов ХХ века. Он был не закончен писателем и впервые опубликован после" +
                " его смерти в 1926 году его другом Максом Бродом. Сюжет \"Замка\" абсурден, но в то же" +
                " время правдоподобен: в некую Деревню приезжает землемер К. и пытается попасть в Замок," +
                " куда его не пускают. Сложная система бюрократии запутывается в клубок, распутать который" +
                " нет никакой возможности, а значит, нет возможности попасть в Замок, какими бы окольными" +
                " путями ты ни шел. Но сатира на бюрократическую систему — лишь один из подтекстов романа." +
                " Все художественное пространство ограничено Деревней и недоступным Замком. Время тут течет" +
                " иррационально. \"Замок\" — это в первую очередь метафора, сквозь которую проглядывают" +
                " приметы реальности. Замок — вполне конкретен, и в то же время это мираж. Может," +
                " дорога к нему — это дорога к Богу, а Деревня — всего лишь прообраз нашего земного мира? ",
            Manufacturer = manufacturers.First(x => x.Name == "Издательство АСТ"),
            Authors = authors.Where(x =>
                    x.Name == "Франц"
                    && x.Surname == "Кафка")
                .ToList(),
            Genres = BookGenre.Novel | BookGenre.Prose | BookGenre.Classics,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Над кукушкиным гнездом",
            ImgSrc = "/img/ken_kizi_nad_kukushkinim_gnezdom.jpg",
            ThumbImgSrc = "/img/ken_kizi_nad_kukushkinim_gnezdom.jpg",
            ReceiptDate = new DateTime(2015, 2, 17),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 199.00m,

            Rating = 4.43m,
            LongDescription = "Подобно многочисленным громким событиям, связанным с именем \"веселого проказника\"" +
                              " Кена Кизи, его первая книга \"Над кукушкиным гнездом\" (1962) произвела много шума в" +
                              " литературной жизни Америки. Кизи был признан талантливейшим писателем, а роман стал" +
                              " одним из главных произведений для битников и хиппи.\"Над кукушкиным гнездом\"" +
                              " – это грубое и опустошающе честное изображение границ между здравомыслием и безумием." +
                              " \"Если кто-нибудь захочет ощутить пульс нашего времени, пусть читает Кизи." +
                              " И если все будет хорошо и не изменится порядок вещей, его будут читать и в следующем" +
                              " веке\", – писали в газете \"Лос-Анджелес Таймс\". Действительно, и в наши дни книга" +
                              " продолжает жить и не теряет своей сумасшедшей популярности. По мотивам романа был снят" +
                              " одноименный фильм (реж. Милош Форман, 1975), покоривший весь мир и получивший пять" +
                              " Оскаров. А также поставлено множество спектаклей в разных странах, в том числе в России.",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Кен"
                    && x.Surname == "Кизи")
                .ToList(),
            Genres = BookGenre.Novel | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Искусство программирования",
            ImgSrc = "/img/knut_iskusstvo_programmirovaniya.jpg",
            ThumbImgSrc = "/img/knut_iskusstvo_programmirovaniya.jpg",
            ReceiptDate = new DateTime(2017, 3, 9),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 4199.00m,
            NewPrice = 4099.00m,
            Rating = 4.1m,
            LongDescription =
                "Первый том серии книг Искусство программирования начинается с описания основных понятий" +
                " и методов программирования. Затем автор переходит к рассмотрению информационных" +
                " структур - представлению информации внутри компьютера, структурных связей между" +
                " элементами данных и способам эффективной работы с ними. Для методов имитации," +
                " символьных вычислений, числовых методов, методов разработки программного обеспечения" +
                " даны примеры элементарных приложений. По сравнению с предыдущим изданием добавлены" +
                " десятки простых, но в то же время очень важных алгоритмов. В соответствии с современными" +
                " направлениями исследований был существенно переработан раздел математического введения. ",
            Manufacturer = manufacturers.First(x => x.Name == "Наука и просвещение"),
            Authors = authors.Where(x =>
                    x.Name == "Дональд"
                    && x.Patronimic == "Эрвин"
                    && x.Surname == "Кнут")
                .ToList(),
            Genres = BookGenre.Education | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Солярис",
            ImgSrc = "/img/lem_solyaris.jpg",
            ThumbImgSrc = "/img/lem_solyaris.jpg",
            ReceiptDate = new DateTime(2018, 11, 19),
            BookTypes = BookCard.BookType.Ebook,
            OldPrice = 99.00m,

            Rating = 4.65m,
            LongDescription = "«Солярис» — бесспорная вершина творчества Станислава Лема, произведение, повлиявшее на" +
                              " развитие научной фантастики XX века, в том числе и на русскую фантастику. Роман дважды" +
                              " экранизирован, по нему были поставлены радиопьесы, спектакли — и даже балет!" +
                              " Итак, что же такое — Солярис? Бескрайний мыслящий океан, преследующий непонятные" +
                              " человеку цели, тончайший камертон, преобразующий людские чувства в материальную форму?" +
                              " Воплощенный кошмар психолога или духовный целитель? Со дня публикации книги прошло больше" +
                              " 50 лет, а ее читатели все так же продолжают задаваться вопросами, ответы на которые" +
                              " невозможно получить, не заглянув в свое собственное сердце.",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Станислав"
                    && x.Surname == "Лем")
                .ToList(),
            Genres = BookGenre.Fiction | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Мастер и Маргарита",
            ImgSrc = "/img/master_i_margarita.jpg",
            ThumbImgSrc = "/img/master_i_margarita.jpg",
            ReceiptDate = new DateTime(2015, 7, 1),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 699.00m,
            NewPrice = 599.00m,
            Rating = 4.49m,
            IsOnSale = true,
            LongDescription = "Бессмертное, загадочное и остроумное «Евангелие от Сатаны» Михаила Булгакова. Роман," +
                              " уникальный в российской литературе ХХ столетия. Трудно себе представить, какое влияние" +
                              " он оказал на мировую культуру.На основе «Мастера и Маргариты» снимались и продолжают" +
                              " сниматься фильмы и телесериалы, это произведение легло в основу оперы, симфонии," +
                              " рок-оперы, его иллюстрировали самые знаменитые художники и фотографы. Достаточно" +
                              " перечислить лишь немногих создателей произведений, посвященных шедевру Булгакова и" +
                              " им вдохновленных: Анджей Вайда, Эннио Морриконе, Мик Джаггер, Дэвид Боуи. Чем же" +
                              " заворожила столь разных творческих личностей история о дьяволе и его свите, почтивших" +
                              " своим присутствием Москву 1930-х, о прокураторе Иудеи всаднике Понтии Пилате и нищем" +
                              " философе Иешуа Га-Ноцри, о талантливом и несчастном Мастере и его прекрасной и верной" +
                              " возлюбленной Маргарите? ",
            Manufacturer = manufacturers.First(x => x.Name == "Дом Книги"),
            Authors = authors.Where(x =>
                    x.Name == "Михаил"
                    && x.Patronimic == "Афанасьевич"
                    && x.Surname == "Булгаков")
                .ToList(),
            Genres = BookGenre.Novel | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Мир как воля и представление",
            ImgSrc = "/img/mir_kak_volya_i_predstavlenie.jpg",
            ThumbImgSrc = "/img/mir_kak_volya_i_predstavlenie.jpg",
            ReceiptDate = new DateTime(2016, 2, 1),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 899.00m,
            NewPrice = 599.00m,
            Rating = 4.87m,
            LongDescription = "\"Мир как воля и представление\" - главный труд А. Шопенгауэра. В нем автор стремился" +
                              " создать принципиально новое и всеобъемлющее учение, противоположное рационализму Нового" +
                              " времени.В философии Шопенгауэра движущая сила всех явлений во Вселенной - бессознательная" +
                              " мировая воля, которая проявляет себя во всем и всему остается чуждой.",
            Manufacturer = manufacturers.First(x => x.Name == "Дом Книги"),
            Authors = authors.Where(x =>
                    x.Name == "Артур"
                    && x.Surname == "Шопенгауэр")
                .ToList(),
            Genres = BookGenre.Philosophy | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Государство",
            ImgSrc = "/img/platon_gosudarstvo.jpg",
            ThumbImgSrc = "/img/platon_gosudarstvo.jpg",
            ReceiptDate = new DateTime(2016, 3, 2),
            BookTypes = BookCard.BookType.Ebook,
            OldPrice = 99.00m,

            Rating = 4.12m,
            LongDescription =
                "Диалог «Государство» занимает особое место в творчестве и мировоззрении Платона. В нем он" +
                " рисует картину идеального, по его мнению, устройства жизни людей, основанного на высшей" +
                " справедливости, и дает подробную характеристику основным существующим формам правления," +
                " таким, как аристократия, олигархия, тирания, демократия, и другим.",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Платон")
                .ToList(),
            Genres = BookGenre.Philosophy | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Преступление и наказание",
            ImgSrc = "/img/prestuplenie_i_nakazanie.jpg",
            ThumbImgSrc = "/img/prestuplenie_i_nakazanie.jpg",
            ReceiptDate = new DateTime(2017, 2, 12),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 599.00m,
            NewPrice = 499.00m,
            Rating = 4.44m,
            LongDescription = "«Преступление и наказание» (1866) — одно из самых значительных произведений в истории" +
                              " мировой литературы. Это и глубокий философский роман, и тонкая психологическая драма," +
                              " и захватывающий детектив, и величественная картина мрачного города, в недрах которого" +
                              " герои грешат и ищут прощения, жертвуют собой и отрекаются от себя ради ближних и находят" +
                              " успокоение в смирении, покаянии, вере. Главный герой романа Родион Раскольников решается" +
                              " на убийство, чтобы доказать себе и миру, что он не «тварь дрожащая», а «право имеет»." +
                              " Главным предметом исследования писателя становится процесс превращения добропорядочного," +
                              " умного и доброго юноши в убийцу, а также то, как совершивший преступление Раскольников" +
                              " может искупить свою вину. «Преступление и наказание» неоднократно экранизировали," +
                              " музыку к балету написал Чайковский, а недавно на одной из московских площадок" +
                              " по роману была поставлена рок-опера.",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Федор"
                    && x.Patronimic == "Михайлович"
                    && x.Surname == "Достоевский")
                .ToList(),
            Genres = BookGenre.Novel | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Три товарища",
            ImgSrc = $"/img/remark_tri_tovarisha.jpg",
            ThumbImgSrc = $"/img/remark_tri_tovarisha.jpg",
            ReceiptDate = new DateTime(2017, 2, 12),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 299.00m,
            NewPrice = 199.00m,
            Rating = 4.63m,
            LongDescription = "Самый красивый в двадцатом столетии роман о любви... " +
                              "Самый увлекательный в двадцатом столетии роман о дружбе..." +
                              " Самый трагический и пронзительный роман о человеческих отношениях" +
                              " за всю историю двадцатого столетия. ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Эрих"
                    && x.Patronimic == "Мария"
                    && x.Surname == "Ремарк")
                .ToList(),
            Genres = BookGenre.Novel | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Стихи о прекрасной даме",
            ImgSrc = "/img/stihi_o_prekrasnoi_dame.jpg",
            ThumbImgSrc = "/img/stihi_o_prekrasnoi_dame.jpg",
            ReceiptDate = new DateTime(2017, 2, 12),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 199.00m,

            Rating = 4.43m,
            IsOnSale = true,
            LongDescription =
                "Его называли \"трагическим тенором эпохи\".Ему посвящали свои стихи Ахматова, Цветаева," +
                " Пастернак... Его ранние стихотворения так же сумеречны, томительны и недосказанны," +
                " как эпоха модерна, вместе с которой он пришел в русскую литературу. В настоящий сборник" +
                " вошли стихотворения из двух книг Л. Блока, 1898-1904 и 1904-1908 годов. Среди них - стихи" +
                " о Прекрасной Даме, циклы \"Распутья\", \"Пузыри земли\", \"Город\", \"Снежная маска\"," +
                " \"Фаина\", \"Вольные мысли\". В сборник включено также эссе Зинаиды Гиппиус о Блоке -" +
                " \"Мой лунный друг\".\"Всякое стихотворение - покрывало, растянутое на остриях нескольких" +
                " слов. Эти слова светятся, как звезды... Из-за них существует" +
                " стихотворение...\" (Александр Блок) ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Александр"
                    && x.Patronimic == "Александрович"
                    && x.Surname == "Блок")
                .ToList(),
            Genres = BookGenre.Classics | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Сто лет одиночества",
            ImgSrc = "/img/sto_let_odinochestva.jpg",
            ThumbImgSrc = "/img/sto_let_odinochestva.jpg",
            ReceiptDate = new DateTime(2017, 2, 12),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 799.00m,
            NewPrice = 699.00m,
            Rating = 4.49m,
            LongDescription =
                "Одна из величайших книг ХХ века. Странная, поэтичная, причудливая история города Макондо," +
                " затерянного где-то в джунглях, – от сотворения до упадка. История рода Буэндиа – семьи," +
                " в которой чудеса столь повседневны, что на них даже не обращают внимания. Клан Буэндиа" +
                " порождает святых и грешников, революционеров, героев и предателей, лихих авантюристов –" +
                " и женщин, слишком прекрасных для обычной жизни. В нем кипят необычайные страсти –" +
                " и происходят невероятные события. Однако эти невероятные события снова и снова" +
                " становятся своеобразным «волшебным зеркалом», сквозь которое читателю является" +
                " подлинная история Латинской Америки…",
            Manufacturer = manufacturers.First(x => x.Name == "Дом Книги"),
            Authors = authors.Where(x =>
                    x.Name == "Габриэль"
                    && x.Patronimic == "Гарсиа"
                    && x.Surname == "Маркес")
                .ToList(),
            Genres = BookGenre.Classics | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Программирование: принципы и практика использования C++",
            ImgSrc = "/img/straustrup_srogramirovanie_cpp.jpg",
            ThumbImgSrc = "/img/straustrup_srogramirovanie_cpp.jpg",
            ReceiptDate = new DateTime(2014, 12, 12),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 4799.00m,
            NewPrice = 4499.00m,
            Rating = 4.42m,
            LongDescription = "Эта книга не является учебником по языку C++, это учебник по программированию." +
                              " Несмотря на то что ее автор — автор языка С++, книга не посвящена этому языку" +
                              " программирования; он играет в книге сугубо иллюстративную роль. Автор задумал данную" +
                              " книгу как вводный курс по программированию. Поскольку теория без практики совершенно" +
                              " бессмысленна, такой учебник должен изобиловать примерами программных решений, и" +
                              " неудивительно, что автор языка C++ использовал в книге свое детище.В книге в первую" +
                              " очередь описан широкий круг понятий и приемов программирования, необходимых для того," +
                              " чтобы стать профессиональным программистом, и в гораздо меньшей степени — возможности" +
                              " языка программирования C++.В первую очередь, книга адресована начинающим программистам" +
                              " и студентам компьютерных специальностей, которые найдут в ней много новой информации," +
                              " и смогут узнать точку зрения создателя языка С++ на современные методы программирования." +
                              "Если вы решили стать программистом, и уже знакомы с азами C++ — эта книга для вас," +
                              " в первую очередь потому, что программирование — это не только, и не столько знание" +
                              " инструмента (языка программирования C++), сколько понимание самого процесса. Автор" +
                              " недаром не ограничился своим первоклассным (но ни в коей мере не являющимся учебником" +
                              " для программистов без большого практического опыта) трудом Язык программирования" +
                              " C++.Проводя грубую аналогию — виртуозное владение топором никого не делало настоящим" +
                              " плотником. Бьярне Страуструп в очередной раз приходит на помощь программистам" +
                              " — создав уникальный язык программирования, он не ограничивается им и рассказывает" +
                              " о том, как правильно им воспользоваться, даже не зная все его тонкости и возможности" +
                              ".Основные темы книги:Подготовка к созданию реальных программ. Автор книги предполагает," +
                              " что читатели в конце концов начнут писать нетривиальные программы либо в качестве" +
                              " профессиональных разработчиков программного обеспечения, либо в качестве программистов," +
                              " работающих в других областях науки и техники.Упор на основные концепции и методы." +
                              " Основные концепции и методы программирования в книге излагаются глубже, чем это принято" +
                              " в традиционных вводных курсах. Этот подход дает основательный фундамент для разработки" +
                              " полезных, правильных, понятных и эффективных программ.Программирование на современном" +
                              " языке С++ (C++11 и C++14). Книга представляет собой введение в программирование, включая" +
                              " объектно-ориентированное и обобщенное программирование. Одновременно она представляет" +
                              " собой введение в язык С++, один из широко применяющихся языков программирования в" +
                              " современном мире. В книге описаны современные методы программирования на С++, включая" +
                              " стандартную библиотеку и возможности C++11 и C++14, позволяющие упростить" +
                              " программирование.Для начинающих программистов и всех, кто хочет научиться" +
                              " программировать. Книга предназначена в основном для людей, никогда ранее не" +
                              " программировавших, и опробована на более чем тысяче студентов университета. Однако и" +
                              " опытные программисты, и студенты, уже изучившие основы программирования, найдут в книге" +
                              " много полезной информации, которая позволит им перейти на еще более высокий уровень" +
                              " мастерства.Широкий охват тем. Первая половина книги охватывает широкий спектр основных" +
                              " понятий, методов проектирования и программирования, свойств языка С++ и его библиотек." +
                              " Это позволит читателям писать программы, выполняющие ввод и вывод данных, вычисления" +
                              " и построение простых графических изображений. Во второй половине рассматриваются более" +
                              " специализированные темы (такие как обработка текста, тестирование и язык C). В книге" +
                              " содержится много справочного материала. Исходные тексты программ и иные материалы" +
                              " читатели могут найти на веб-сайте автора.",
            Manufacturer = manufacturers.First(x => x.Name == "Дом Книги"),
            Authors = authors.Where(x =>
                    x.Name == "Бьярне"
                    && x.Surname == "Страуструп")
                .ToList(),
            Genres = BookGenre.Education | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Искусство войны",
            ImgSrc = "/img/sun_tzy_iskusstvo_voiny.jpeg",
            ThumbImgSrc = "/img/sun_tzy_iskusstvo_voiny.jpeg",
            ReceiptDate = new DateTime(2017, 2, 12),
            BookTypes = BookCard.BookType.Ebook,
            OldPrice = 99.00m,

            Rating = 4.69m,
            IsOnSale = true,
            LongDescription =
                "Тот, кто знает, когда можно сражаться, а когда нельзя, одержит победу. Тот, кто понимает," +
                " как использовать большие и малые силы, одержит победу.Тот, у кого верхи и низы горят" +
                " одним и тем же желанием, одержит победу.Тот, кто, будучи полностью готов, ждет" +
                " неподготовленного, одержит победу.Совсем не обязательно вести военные действия," +
                " чтобы использовать принципы, сформулированные Сунь-цзы. Они равно применимы в политике," +
                " управлении и даже в повседневной жизни. Победителем мечтает стать каждый. Постижение" +
                " древней мудрости и сегодня - шаг к победе. ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Сунь-Цзы")
                .ToList(),
            Genres = BookGenre.Philosophy | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Так говорил Заратустра",
            ImgSrc = "/img/tak_govoril_zaratustra.webp",
            ThumbImgSrc = "/img/tak_govoril_zaratustra.webp",
            ReceiptDate = new DateTime(2015, 9, 27),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 199.00m,
            Rating = 4.11m,
            LongDescription = "Трактат «Так говорил Заратустра» называют ницшеанской Библией. В нем сформулирована" +
                              " излюбленная идея Ницше - идея Сверхчеловека, который является для автора нравственным" +
                              " образцом, смыслом существования, тем, к чему нужно стремиться. Человек же - лишь мост" +
                              " между животным и Сверхчеловеком. Необычная форма - поэтичная, афористичная - не совсем" +
                              " соответствует нашим представлениям о философском трактате. Однако, вчитываясь, мы" +
                              " улавливаем ход мысли автора, все глубже проникаемся его идеями и убеждениями...",
            Manufacturer = manufacturers.First(x => x.Name == "Издательство АСТ"),
            Authors = authors.Where(x =>
                    x.Name == "Фридрих"
                    && x.Surname == "Ницше")
                .ToList(),
            Genres = BookGenre.Philosophy | BookGenre.Prose,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Война и мир",
            ImgSrc = "/img/voina_i_mir.jpg",
            ThumbImgSrc = "/img/voina_i_mir.jpg",
            ReceiptDate = new DateTime(2015, 9, 27),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 799.00m,
            Rating = 4.24m,
            LongDescription =
                "Величайшая эпопея, снискавшая славу во всем мире. Сотни героев, застигнутых безжалостным" +
                " потоком времени, и сотни судеб, перемолотых масштабными историческими событиями." +
                " Сменяются поколения и эпохи, а чтение \"Войны и мира\" по-прежнему дарит нам восторг" +
                " познания простых и сложных жизненных истин. Мы все так же находим себя в Наташе" +
                " Ростовой, Андрее Болконском, Пьере Безухове, все так же, подобно им, хотим жить," +
                " любить и верить наперекор войне - с неприятелем ли, с обстоятельствами или с самими собой. ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Лев"
                    && x.Patronimic == "Николаевич"
                    && x.Surname == "Толстой")
                .ToList(),
            Genres = BookGenre.Classics | BookGenre.Prose | BookGenre.Novel,
            Quantity = 10,
        });

        products.Add(new BookCard
        {
            Name = "Мы",
            ImgSrc = "/img/zamyatin_mi.jpg",
            ThumbImgSrc = "/img/zamyatin_mi.jpg",
            ReceiptDate = new DateTime(2014, 12, 17),
            BookTypes = BookCard.BookType.Paper,
            OldPrice = 499.00m,
            Rating = 4.32m,
            LongDescription = "Великий роман, ставший прародителем жанра \"антиутопия\", уже давно по праву считается" +
                              " классикой. Однако, в СССР книга была запрещена почти 70 лет. Обнаженные Е. Замятиным" +
                              " темы отсут ствия индивидуальности, тотальной слежки и стремлений к призрачному" +
                              " обществу всеоб щего равенства, стали символами тоталитар ного мира. Как близок сюжет" +
                              " романа стал современной реальности? ",
            Manufacturer = manufacturers.First(x => x.Name == "Эксмо"),
            Authors = authors.Where(x =>
                    x.Name == "Евгений"
                    && x.Patronimic == "Иванович"
                    && x.Surname == "Замятин")
                .ToList(),
            Genres = BookGenre.Fiction | BookGenre.Prose,
            Quantity = 0,
        });

        #endregion

        return products;
    }

    private List<AuthorCard> CreateAuthors()
    {
        var authors = new List<AuthorCard>();

        #region Content

        authors.Add(new AuthorCard
        {
            Name = "Аркадий",
            Patronimic = "Натанович",
            Surname = "Стругацкий"
        });

        authors.Add(new AuthorCard
        {
            Name = "Борис",
            Patronimic = "Натанович",
            Surname = "Стругацкий"
        });

        authors.Add(new AuthorCard
        {
            Name = "Александр",
            Patronimic = "Исаевич",
            Surname = "Солженицын"
        });

        authors.Add(new AuthorCard
        {
            Name = "Михаил",
            Patronimic = "Александрович",
            Surname = "Бакунин"
        });

        authors.Add(new AuthorCard
        {
            Name = "Константин",
            Patronimic = "Дмитриевич",
            Surname = "Бальмонт"
        });

        authors.Add(new AuthorCard
        {
            Name = "Брэм",
            Surname = "Стокер"
        });

        authors.Add(new AuthorCard
        {
            Name = "Инандзо",
            Surname = "Нитобэ"
        });

        authors.Add(new AuthorCard
        {
            Name = "Данте",
            Surname = "Алигьери"
        });

        authors.Add(new AuthorCard
        {
            Name = "Дэн",
            Surname = "Симмонс"
        });

        authors.Add(new AuthorCard
        {
            Name = "Джон",
            Surname = "Мильтон"
        });

        authors.Add(new AuthorCard
        {
            Name = "Иоган",
            Patronimic = "Вольфганг",
            Surname = "Гете"
        });

        authors.Add(new AuthorCard
        {
            Name = "Франц",
            Surname = "Кафка"
        });

        authors.Add(new AuthorCard
        {
            Name = "Кен",
            Surname = "Кизи"
        });

        authors.Add(new AuthorCard
        {
            Name = "Дональд",
            Patronimic = "Эрвин",
            Surname = "Кнут"
        });

        authors.Add(new AuthorCard
        {
            Name = "Станислав",
            Surname = "Лем"
        });

        authors.Add(new AuthorCard
        {
            Name = "Михаил",
            Patronimic = "Афанасьевич",
            Surname = "Булгаков"
        });

        authors.Add(new AuthorCard
        {
            Name = "Артур",
            Surname = "Шопенгауэр"
        });

        authors.Add(new AuthorCard
        {
            Name = "Платон",
        });

        authors.Add(new AuthorCard
        {
            Name = "Федор",
            Patronimic = "Михайлович",
            Surname = "Достоевский"
        });

        authors.Add(new AuthorCard
        {
            Name = "Эрих",
            Patronimic = "Мария",
            Surname = "Ремарк"
        });

        authors.Add(new AuthorCard
        {
            Name = "Александр",
            Patronimic = "Александрович",
            Surname = "Блок"
        });

        authors.Add(new AuthorCard
        {
            Name = "Габриэль",
            Patronimic = "Гарсиа",
            Surname = "Маркес"
        });

        authors.Add(new AuthorCard
        {
            Name = "Бьярне",
            Surname = "Страуструп"
        });

        authors.Add(new AuthorCard
        {
            Name = "Сунь-Цзы",
        });

        authors.Add(new AuthorCard
        {
            Name = "Лев",
            Patronimic = "Николаевич",
            Surname = "Толстой"
        });

        authors.Add(new AuthorCard
        {
            Name = "Евгений",
            Patronimic = "Иванович",
            Surname = "Замятин"
        });

        authors.Add(new AuthorCard
        {
            Name = "Фридрих",
            Surname = "Ницше"
        });

        #endregion

        return authors;
    }

    private List<ManufacturerCard> CreateManufacturers()
    {
        var manufacturers = new List<ManufacturerCard>();

        #region Content
        
        manufacturers.Add(new ManufacturerCard
        {
            Name = "Вологодский ручечный завод"
        });

        manufacturers.Add(new ManufacturerCard
        {
            Name = "Издательство АСТ"
        });

        manufacturers.Add(new ManufacturerCard
        {
            Name = "Дом Книги"
        });

        manufacturers.Add(new ManufacturerCard
        {
            Name = "Эксмо"
        });

        manufacturers.Add(new ManufacturerCard
        {
            Name = "Наука и просвещение"
        });

        #endregion

        return manufacturers;
    }
    
    private List<CartLine> CreateCartLines(List<User> users, List<ProductCard> products)
    {
        var cartLines = new List<CartLine>();
        
        #region Content

        cartLines.Add(new CartLine
        {
            Product = products.First(x => x.Name == "Пикник на обочине"),
            Owner = users.First(x => x.Email == "test@test.test"),
            Quantity = 1,
        });
        
        cartLines.Add(new CartLine
        {
            Product = products.First(x => x.Name == "Государственность и Анархия"),
            Owner = users.First(x => x.Email == "test@test.test"),
            Quantity = 1,
        });
        
        cartLines.Add(new CartLine
        {
            Product = products.First(x => x.Name == "Архипелаг Гулаг"),
            Owner = users.First(x => x.Email == "test@test.test"),
            Quantity = 2,
        });

        #endregion

        return cartLines;
    }
}
--drop table work.articletype
create table Work.ArticleType(
Id int identity(1,1) primary key,
Code varchar(16) not null unique,
Name varchar(128) not null,
Descr varchar(512) null,
IsMain bit default 0,
Ghost bit default 0,
AddDate datetime2(2) default getdate()
);

insert into work.ArticleType(code, name, IsMain, descr) values 
('MAIN', 'Zaawansowane programowanie C#', 1,'Strona poœwiêcona programowaniu w jêzyku C# w technologii .NET Framework.'),
('ASP', 'ASP.NET MVC', 0,'Sekcja poœwiêcona aplikacjom webowych w technologii ASP.NET MVC oraz technologiom frontowym: JS, HTML, CSS, JS Frameworks.'),
('WCF', 'WCF/WebAPI', 0,'Sekcja poœwiêcona us³ugom sieciowym - SOAP, REST, JSON, XML. Zawiera równie¿ informacje dotycz¹ce testowania us³ug za pomoc¹ narzêdzi SoapUI, Postman, Fiddler.'),
('PATTERN', 'Wzorce projektowe', 0, 'Sekcja poœwiêcona wzorcom projektowym. Zawiera opisy oraz przyk³ady u¿ycia.'),
('SQL', 'SQL', 0,  'Sekcja poœwiêcona relacyjnym bazom danych MS SQL i PostgreSQL.'),
('REKRU', 'Pytania rekrutacyjne', 0, 'Sekcja poœwiêcona popularnym pytaniom pojawiaj¹cym siê na rekrutacjach.');



select * from work.articletype
update work.articletype set Ghost = 1 where code = 'REKRU'

--drop table work.article
create table Work.Article(
Id int identity(1,1) primary key,
TypeId int references Work.ArticleType,
Title varchar(128) not null,
Content text,
Author varchar(64),
Ghost bit default 0,
AddDate datetime2(2) default getdate()
);


select * from Work.Article

----------------------------
----------------------------
--------- USERS ------------

create table Work.AppUserType(
Id int identity(1,1) primary key,
Code varchar(16) not null unique,
Name varchar(128) not null,
Ghost bit default 0,
AddDate datetime2(2) default getdate()
);

insert into work.AppUserType(code, name) values ('USR', 'User'), ('ADM', 'Admin'), ('MOD', 'Moderator');

select * from Work.AppUserType;


CREATE TABLE Work.AppUserRequest
(
	Id int primary key identity(1,1),
	Login varchar(50) not null unique,
	Password varchar(50) not null,
	Email varchar(64) not null,
	Comment varchar(256),
	AddDate datetime2(2) not null default getdate(),
	Ghost bit not null default 0
)

CREATE TABLE Work.AppUser
(
	Id int primary key identity(1,1),
	Login varchar(50) not null unique,
	Password varchar(50) not null,
	Email varchar(64) not null,
	TypeId int not null references Work.AppUserType,
	RequestId int references Work.AppUserRequest,
	AddDate datetime2(2) not null default getdate(),
	Ghost bit not null default 0
)

select * from work.AppUserType
select * from work.appuserrequest
select * from work.appuser

-----------------------------
-----------------------------
---------- LOGS -------------

create schema WWW;

--drop table WWW.Logs;
CREATE TABLE WWW.Logs
(
	Id bigint primary key identity(1,1),
	Type varchar(5) not null,
	Message varchar(max),
	Ip varchar(15),
	AddDate datetime2(2) not null default getdate()
)

select * from www.Logs

-----------------------------
-----------------------------
---------- ContactMessage -------------

--drop table www.ContactMessage;
CREATE TABLE WWW.ContactMessage
(
	Id int primary key identity(1,1),
	Author varchar(64) not null,
	Subject varchar(64),
	Content varchar(256),
	Ip varchar(15),
	AddDate datetime2(2) not null default getdate()
)

select * from www.ContactMessage


-----------------------------
-----------------------------
---------- Rekru -------------

--drop table www.RekruQuestions;
CREATE TABLE WWW.RekruQuestions
(
	Id int primary key identity(1,1),
	Question varchar(256) not null,
	Author varchar(64),	
	AddDate datetime2(2) not null default getdate(),
	Ghost bit not null default 0,
);

select * from www.RekruQuestions;

CREATE TABLE WWW.RekruAnswers
(
	Id int primary key identity(1,1),
	QuestionId integer not null references WWW.RekruQuestions,
	Content varchar(512) not null,
	Author varchar(64),	
	AddDate datetime2(2) not null default getdate(),
	Ghost bit not null default 0,
)

alter table www.rekruanswers alter column content text;

select * from www.RekruAnswers

-------------------------------------
-------------------------------------
---------- StaticContent -------------

--drop table www.PageContent;
CREATE TABLE WWW.PageContent
(
Id int identity(1,1) primary key,
Code varchar(16) not null unique,
Content text,
Ghost bit default 0,
AddDate datetime2(2) default getdate()
);

select * from www.PageContent;


update work.Article
set Title = 'AutoMapper w Autofac (oddzielny Module)'
where title = 'AutoMapper w Autofac ver. 2 '
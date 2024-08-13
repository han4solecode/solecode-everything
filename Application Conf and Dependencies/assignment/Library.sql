create table Users (
	userId SERIAL NOT NULL PRIMARY KEY,
	name VARCHAR(255) not null,
	email VARCHAR(255) not null,
	address VARCHAR(255)
);

create table Books (
	bookId SERIAL NOT NULL primary key,
	title VARCHAR(255) not null,
	author VARCHAR(255) not null,
	publicationYear DATE not null,
	isbn VARCHAR(17) not null
);

create table lendings (
	lendingId SERIAL not null primary key,
	userId int not null,
	bookId int not null,
	constraint fk_lendings_users_userId
		foreign key (userId)
		references Users(userId)
		on delete cascade,
	constraint fk_lendings_books_bookId
		foreign key (bookId)
		references Books(bookId)
		on delete cascade
);

alter table lendings add column borrowDate Date default current_date,
					 add column returnDate Date;
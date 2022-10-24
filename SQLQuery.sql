CREATE TABLE player(
  email nvarchar(225) PRIMARY KEY,
  password nvarchar(50)not null,
  birthday date not null,
  coins int not null,
  username nvarchar(20) not null
);


CREATE TABLE friendList(
  idFriendList INT PRIMARY KEY,
  email nvarchar (225) not null,
	CONSTRAINT [FK_friendList.email] FOREIGN KEY ([email]) REFERENCES [player]([email]),
);
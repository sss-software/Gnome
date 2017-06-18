﻿CREATE TABLE [dbo].[fio_account]
(
	[id] INT NOT NULL identity(1,1) PRIMARY KEY,
	userid int not null,
	[name] nvarchar(30),
	[token] nvarchar(64),
	[last_sync] datetime
	foreign key (userid) references [user](id)
)
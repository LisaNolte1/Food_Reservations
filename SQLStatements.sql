CREATE TABLE [USERS] (
  [user_id] integer PRIMARY KEY,
  [user_email] nvarchar(255),
  [preference_id] integer,
  [role_id] integer
)
GO

CREATE TABLE [ROLES] (
  [role_id] integer PRIMARY KEY,
  [role_name] nvarchar(255)
)
GO

CREATE TABLE [PREFERENCES] (
  [preference_id] integer PRIMARY KEY,
  [preference_type] nvarchar(255)
)
GO

CREATE TABLE [DAYS] (
  [day_id] integer PRIMARY KEY,
  [day_name] nvarchar(255)
)
GO

CREATE TABLE [CUISINES] (
  [cuisine_id] integer PRIMARY KEY,
  [cuisine_name] nvarchar(255)
)
GO

CREATE TABLE [AVAILABILITY] (
  [availability_id] integer PRIMARY KEY,
  [user_id] integer,
  [day_id] integer
)
GO

CREATE TABLE [EVENTS] (
  [event_id] integer PRIMARY KEY,
  [cuisine_id] integer,
  [day_id] integer
)
GO

CREATE TABLE [BOOKINGS] (
  [booking_id] integer PRIMARY KEY,
  [user_id] integer,
  [event_id] integer
)
GO

CREATE TABLE [CUISINES_OPTIONS] (
  [cuisine_options_id] integer PRIMARY KEY,
  [cuisine_id] integer,
  [preference_id] integer,
  [cuisine_option_name] nvarchar(255)
)
GO

ALTER TABLE [USERS] ADD FOREIGN KEY ([role_id]) REFERENCES [ROLES] ([role_id])
GO

ALTER TABLE [USERS] ADD FOREIGN KEY ([preference_id]) REFERENCES [PREFERENCES] ([preference_id])
GO

ALTER TABLE [AVAILABILITY] ADD FOREIGN KEY ([user_id]) REFERENCES [USERS] ([user_id])
GO

ALTER TABLE [AVAILABILITY] ADD FOREIGN KEY ([day_id]) REFERENCES [DAYS] ([day_id])
GO

ALTER TABLE [EVENTS] ADD FOREIGN KEY ([cuisine_id]) REFERENCES [CUISINES] ([cuisine_id])
GO

ALTER TABLE [EVENTS] ADD FOREIGN KEY ([day_id]) REFERENCES [DAYS] ([day_id])
GO

ALTER TABLE [BOOKINGS] ADD FOREIGN KEY ([user_id]) REFERENCES [USERS] ([user_id])
GO

ALTER TABLE [BOOKINGS] ADD FOREIGN KEY ([event_id]) REFERENCES [EVENTS] ([event_id])
GO

ALTER TABLE [CUISINES_OPTIONS] ADD FOREIGN KEY ([cuisine_id]) REFERENCES [CUISINES] ([cuisine_id])
GO

ALTER TABLE [CUISINES_OPTIONS] ADD FOREIGN KEY ([preference_id]) REFERENCES [PREFERENCES] ([preference_id])
GO


--Insert Statements
INSERT INTO [dbo].[CUISINES]
           ([cuisine_id]
           ,[cuisine_name])
     VALUES
           (1, 'KFC'),
		   (2, 'Rockmamas')
GO

INSERT INTO [dbo].[PREFERENCES]
           ([preference_id]
           ,[preference_type])
     VALUES
           (2, 'Halal'),
		   (3, 'Vegan'),
		   (4, 'None')
GO

INSERT INTO [dbo].[CUISINES_OPTIONS]
           ([cuisine_options_id]
           ,[cuisine_id]
           ,[preference_id]
           ,[cuisine_option_name])
     VALUES
           (1, 1, 2, 'KFC Chicken Burger'),
		   (2, 1, 3, 'KFC Salad'),
		   (3, 1, 4, 'KFC Dunken Chicken Burger'),
		   (4, 2, 2, 'Roco Mamas Halal Pizza'),
		   (5, 2, 3, 'Roco Mamas Vegan Burger '),
		   (6, 2, 4, 'Roco Mamas Beef Burger'),
		   (7, 2, 4, 'Roco Mamas Chicken Burger')
GO
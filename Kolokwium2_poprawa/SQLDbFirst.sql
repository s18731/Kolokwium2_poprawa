-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2020-06-25 12:31:42.856

-- tables
-- Table: Artist
CREATE TABLE Artist (
    IdArtist int  NOT NULL,
    Nickname nvarchar(30)  NOT NULL,
    CONSTRAINT Artist_pk PRIMARY KEY  (IdArtist)
);

-- Table: Artist_Event
CREATE TABLE Artist_Event (
    IdArtist int  NOT NULL,
    IdEvent int  NOT NULL,
    PerformanceDate datetime  NOT NULL,
    CONSTRAINT Artist_Event_pk PRIMARY KEY  (IdArtist,IdEvent)
);

-- Table: Event
CREATE TABLE Event (
    IdEvent int  NOT NULL,
    Name nvarchar(100)  NOT NULL,
    StartDate datetime  NOT NULL,
    EndDate datetime  NOT NULL,
    CONSTRAINT Event_pk PRIMARY KEY  (IdEvent)
);

-- Table: Event_Organiser
CREATE TABLE Event_Organiser (
    IdOrganiser int  NOT NULL,
    IdEvent int  NOT NULL,
    CONSTRAINT Event_Organiser_pk PRIMARY KEY  (IdOrganiser,IdEvent)
);

-- Table: Organiser
CREATE TABLE Organiser (
    IdOrganiser int  NOT NULL,
    Name nvarchar(30)  NOT NULL,
    CONSTRAINT Organiser_pk PRIMARY KEY  (IdOrganiser)
);

-- foreign keys
-- Reference: Artist_Event_Artist (table: Artist_Event)
ALTER TABLE Artist_Event ADD CONSTRAINT Artist_Event_Artist
    FOREIGN KEY (IdArtist)
    REFERENCES Artist (IdArtist);

-- Reference: Artist_Event_Event (table: Artist_Event)
ALTER TABLE Artist_Event ADD CONSTRAINT Artist_Event_Event
    FOREIGN KEY (IdEvent)
    REFERENCES Event (IdEvent);

-- Reference: Event_Organiser_Event (table: Event_Organiser)
ALTER TABLE Event_Organiser ADD CONSTRAINT Event_Organiser_Event
    FOREIGN KEY (IdEvent)
    REFERENCES Event (IdEvent);

-- Reference: Event_Organiser_Organiser (table: Event_Organiser)
ALTER TABLE Event_Organiser ADD CONSTRAINT Event_Organiser_Organiser
    FOREIGN KEY (IdOrganiser)
    REFERENCES Organiser (IdOrganiser);

-- End of file.


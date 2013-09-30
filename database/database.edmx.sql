



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/05/2013 04:08:12
-- Generated from EDMX file: C:\Users\thebigbang\Documents\Visual Studio 2010\Projects\Server\database\database.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `HostedGamesAccounts` DROP CONSTRAINT `FK_HostedGamesAccounts_HostedGames`;
--    ALTER TABLE `HostedGamesAccounts` DROP CONSTRAINT `FK_HostedGamesAccounts_Accounts`;
--    ALTER TABLE `HostedGames` DROP CONSTRAINT `FK_HostedGamesPlayers`;
--    ALTER TABLE `PlayersData` DROP CONSTRAINT `FK_PlayersDataHostedGames`;
--    ALTER TABLE `Accounts` DROP CONSTRAINT `FK_PlayersDataAccounts`;
--    ALTER TABLE `Accounts_Players` DROP CONSTRAINT `FK_Players_inherits_Accounts`;
--    ALTER TABLE `Accounts_Anon` DROP CONSTRAINT `FK_Anon_inherits_Accounts`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `HostedGames`;
    DROP TABLE IF EXISTS `Accounts`;
    DROP TABLE IF EXISTS `Versions`;
    DROP TABLE IF EXISTS `PlayersData`;
    DROP TABLE IF EXISTS `Accounts_Players`;
    DROP TABLE IF EXISTS `Accounts_Anon`;
    DROP TABLE IF EXISTS `HostedGamesAccounts`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'HostedGames'

CREATE TABLE `HostedGames` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `CardsOrderList` longtext  NOT NULL,
    `PlayerMax` int  NOT NULL,
    `GameType` longtext  NOT NULL,
    `IsLaunched` bool  NOT NULL,
    `CardsInTurn` longtext  NULL,
    `Hoster_Id` CHAR(36) BINARY  NOT NULL
);

-- Creating table 'Accounts'

CREATE TABLE `Accounts` (
    `Id` CHAR(36) BINARY  NOT NULL,
    `LastLogin` datetime  NOT NULL,
    `AttemptedBlock` longtext  NOT NULL,
    `PlayersData_Id` int  NULL
);

-- Creating table 'Versions'

CREATE TABLE `Versions` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Server` longtext  NOT NULL,
    `Db` longtext  NOT NULL,
    `Client` longtext  NOT NULL
);

-- Creating table 'PlayersData'

CREATE TABLE `PlayersData` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Cards` longtext  NOT NULL,
    `Points` decimal(2,0)  NOT NULL,
    `GameJoined` bool  NOT NULL,
    `IsPlayersTurn` bool  NOT NULL,
    `HostedGames_Id` int  NULL
);

-- Creating table 'Accounts_Players'

CREATE TABLE `Accounts_Players` (
    `Username` longtext  NOT NULL,
    `ScreenName` longtext  NOT NULL,
    `Pwd` longtext  NOT NULL,
    `Email` longtext  NOT NULL,
    `Id` CHAR(36) BINARY  NOT NULL
);

-- Creating table 'Accounts_Anon'

CREATE TABLE `Accounts_Anon` (
    `ScreenName` longtext  NOT NULL,
    `Id` CHAR(36) BINARY  NOT NULL
);

-- Creating table 'HostedGamesAccounts'

CREATE TABLE `HostedGamesAccounts` (
    `GameJoined_Id` int  NOT NULL,
    `Players_Id` CHAR(36) BINARY  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on `Id` in table 'Accounts'

ALTER TABLE `Accounts`
ADD CONSTRAINT `PK_Accounts`
    PRIMARY KEY (`Id` );

-- Creating primary key on `Id` in table 'Accounts_Players'

ALTER TABLE `Accounts_Players`
ADD CONSTRAINT `PK_Accounts_Players`
    PRIMARY KEY (`Id` );

-- Creating primary key on `Id` in table 'Accounts_Anon'

ALTER TABLE `Accounts_Anon`
ADD CONSTRAINT `PK_Accounts_Anon`
    PRIMARY KEY (`Id` );

-- Creating primary key on `GameJoined_Id`, `Players_Id` in table 'HostedGamesAccounts'

ALTER TABLE `HostedGamesAccounts`
ADD CONSTRAINT `PK_HostedGamesAccounts`
    PRIMARY KEY (`GameJoined_Id`, `Players_Id` );



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `GameJoined_Id` in table 'HostedGamesAccounts'

ALTER TABLE `HostedGamesAccounts`
ADD CONSTRAINT `FK_HostedGamesAccounts_HostedGames`
    FOREIGN KEY (`GameJoined_Id`)
    REFERENCES `HostedGames`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Players_Id` in table 'HostedGamesAccounts'

ALTER TABLE `HostedGamesAccounts`
ADD CONSTRAINT `FK_HostedGamesAccounts_Accounts`
    FOREIGN KEY (`Players_Id`)
    REFERENCES `Accounts`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HostedGamesAccounts_Accounts'

CREATE INDEX `IX_FK_HostedGamesAccounts_Accounts` 
    ON `HostedGamesAccounts`
    (`Players_Id`);

-- Creating foreign key on `Hoster_Id` in table 'HostedGames'

ALTER TABLE `HostedGames`
ADD CONSTRAINT `FK_HostedGamesPlayers`
    FOREIGN KEY (`Hoster_Id`)
    REFERENCES `Accounts_Players`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HostedGamesPlayers'

CREATE INDEX `IX_FK_HostedGamesPlayers` 
    ON `HostedGames`
    (`Hoster_Id`);

-- Creating foreign key on `HostedGames_Id` in table 'PlayersData'

ALTER TABLE `PlayersData`
ADD CONSTRAINT `FK_PlayersDataHostedGames`
    FOREIGN KEY (`HostedGames_Id`)
    REFERENCES `HostedGames`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayersDataHostedGames'

CREATE INDEX `IX_FK_PlayersDataHostedGames` 
    ON `PlayersData`
    (`HostedGames_Id`);

-- Creating foreign key on `PlayersData_Id` in table 'Accounts'

ALTER TABLE `Accounts`
ADD CONSTRAINT `FK_PlayersDataAccounts`
    FOREIGN KEY (`PlayersData_Id`)
    REFERENCES `PlayersData`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayersDataAccounts'

CREATE INDEX `IX_FK_PlayersDataAccounts` 
    ON `Accounts`
    (`PlayersData_Id`);

-- Creating foreign key on `Id` in table 'Accounts_Players'

ALTER TABLE `Accounts_Players`
ADD CONSTRAINT `FK_Players_inherits_Accounts`
    FOREIGN KEY (`Id`)
    REFERENCES `Accounts`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Id` in table 'Accounts_Anon'

ALTER TABLE `Accounts_Anon`
ADD CONSTRAINT `FK_Anon_inherits_Accounts`
    FOREIGN KEY (`Id`)
    REFERENCES `Accounts`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

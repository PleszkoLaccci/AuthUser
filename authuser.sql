-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Nov 05. 11:28
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `authuser`
--
CREATE DATABASE IF NOT EXISTS `authuser` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `authuser`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roles`
--

CREATE TABLE `roles` (
  `Id` char(36) NOT NULL,
  `Name` varchar(30) DEFAULT NULL,
  `RegTime` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `roles`
--

INSERT INTO `roles` (`Id`, `Name`, `RegTime`) VALUES
('61279793-2eeb-412d-bb98-90c9148350c7', 'tanulo', '2026-11-05 11:25:31'),
('74742a09-cd33-46a5-83a4-fac3bdb42f44', 'admin', '2026-11-05 11:25:24'),
('83bdc951-3b27-454c-afe4-c54a9f8c2e59', 'tanar', '2026-11-05 11:25:39');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `userroles`
--

CREATE TABLE `userroles` (
  `UserId` char(36) NOT NULL,
  `RoleId` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Password` text DEFAULT NULL,
  `RegTime` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`Id`, `Name`, `Email`, `Password`, `RegTime`) VALUES
('019de952-a155-4359-af6c-880864327b3c', 'BosskingPleszko Laszlo', 'boss@gmail.com', 'sajt32', '2026-11-05 11:27:19'),
('4dfab9dd-ba99-4ac6-b0cd-b46b2627a0ec', 'kaksi zoltan zalan', 'larpingkaksi@gmail.com', 'kakamvagy', '2026-11-05 11:26:48'),
('ad66de40-2b34-4868-a7c1-e333dc72d792', 'Enger milian', 'larp@gmail.com', 'melegv', '2026-11-05 11:27:02');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `userroles`
--
ALTER TABLE `userroles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `UserId` (`UserId`,`RoleId`),
  ADD KEY `RoleId` (`RoleId`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `userroles`
--
ALTER TABLE `userroles`
  ADD CONSTRAINT `userroles_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `userroles_ibfk_2` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

CREATE DATABASE IF NOT EXISTS `csatahajok`
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_hungarian_ci;

USE `csatahajok`;

DROP TABLE IF EXISTS `kimenet`;
DROP TABLE IF EXISTS `csata`;
DROP TABLE IF EXISTS `hajo`;

CREATE TABLE `hajo` (
  `nev` VARCHAR(100) NOT NULL,
  `osztaly` VARCHAR(100) NOT NULL,
  `felavatva` INT NOT NULL,
  `agyukSzama` INT NOT NULL,
  `kaliber` INT NOT NULL,
  `vizkiszoritas` INT NOT NULL,
  PRIMARY KEY (`nev`)
) ENGINE=InnoDB;

CREATE TABLE `csata` (
  `nev` VARCHAR(100) NOT NULL,
  `kezdes` DATE NOT NULL,
  `befejezes` DATE NOT NULL,
  PRIMARY KEY (`nev`)
) ENGINE=InnoDB;

CREATE TABLE `kimenet` (
  `hajo` VARCHAR(100) NOT NULL,
  `csata` VARCHAR(100) NOT NULL,
  `eredmeny` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`hajo`, `csata`),
  CONSTRAINT `fk_kimenet_hajo`
    FOREIGN KEY (`hajo`) REFERENCES `hajo` (`nev`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_kimenet_csata`
    FOREIGN KEY (`csata`) REFERENCES `csata` (`nev`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
) ENGINE=InnoDB;

INSERT INTO `csata` (`nev`, `kezdes`, `befejezes`) VALUES
('Denmark Strait', '1941-05-24', '1941-05-27'),
('Guadalcanal', '1942-11-15', '1942-11-15'),
('North Cape', '1943-12-26', '1943-12-26'),
('Pearl Harbour', '1941-12-07', '1941-12-07'),
('Surigao Strait', '1944-10-25', '1944-10-25');

INSERT INTO `hajo` (`nev`, `osztaly`, `felavatva`, `agyukSzama`, `kaliber`, `vizkiszoritas`) VALUES
('Arizona', 'Pennsylvania', 1915, 12, 18, 31400),
('Bismarck', 'Bismarck', 1939, 8, 15, 42000),
('California', 'Tennesse', 1921, 12, 14, 32000),
('Duke of York', 'George V', 1941, 12, 18, 31400),
('Fuso', 'Pennsylvania', 1913, 12, 18, 31400),
('Haruna', 'Kongo', 1915, 8, 14, 36600),
('Hiei', 'Kongo', 1914, 8, 14, 32000),
('Hood', 'Admiral', 1918, 8, 40, 45200),
('Iowa', 'Iowa', 1943, 9, 16, 48000),
('King George V', 'George V', 1940, 12, 18, 31400),
('Kirishima', 'Kongo', 1915, 8, 14, 32000),
('Kongo', 'Kongo', 1913, 8, 14, 32000),
('Missuri', 'Iowa', 1944, 9, 16, 46000),
('Musashi', 'Yamato', 1942, 9, 18, 65000),
('New Jersey', 'Iowa', 1943, 9, 16, 46000),
('North Carolina', 'North Carolina', 1941, 9, 16, 37000),
('Prince of Wales', 'George V', 1940, 12, 18, 31400),
('Ramillies', 'Revenge', 1917, 8, 15, 29000),
('Renown', 'Renown', 1916, 6, 15, 32000),
('Repulse', 'Renown', 1916, 6, 15, 32000),
('Resolution', 'Revenge', 1916, 8, 15, 29000),
('Revenge', 'Revenge', 1916, 8, 15, 29000),
('Rodney', 'Nelson ', 1925, 9, 45, 33000),
('Royal Oak', 'Revenge', 1916, 8, 15, 29000),
('Royal Sovereign', 'Revenge', 1916, 8, 15, 29000),
('Scharnhorst', 'Scharnhorst', 1936, 9, 54, 32000),
('South of Dakota', 'Dakota', 1941, 9, 16, 35000),
('Tennessee', 'Tennesse', 1920, 12, 14, 32000),
('Washington', 'North Carolina', 1941, 9, 16, 37000),
('West Virginia', 'North Carolina', 1921, 9, 16, 37000),
('Wisconsin', 'Iowa', 1944, 9, 16, 48500),
('Yamashiro', 'Fuso', 1913, 12, 45, 34700),
('Yamato', 'Yamato', 1941, 9, 18, 65000);

INSERT INTO `kimenet` (`hajo`, `csata`, `eredmeny`) VALUES
('Arizona', 'Pearl Harbour', 'elsulyedt'),
('Bismarck', 'Denmark Strait', 'elsulyedt'),
('California', 'Surigao Strait', 'Ok'),
('Duke of York', 'North Cape', 'Ok'),
('Fuso', 'Surigao Strait', 'elsulyedt'),
('Hood', 'Denmark Strait', 'elsulyedt'),
('King George V', 'Denmark Strait', 'Ok'),
('Kirishima', 'Guadalcanal', 'elsulyedt'),
('Prince of Wales', 'Denmark Strait', 'serult'),
('Rodney', 'Denmark Strait', 'Ok'),
('Scharnhorst', 'North Cape', 'elsulyedt'),
('South of Dakota', 'Guadalcanal', 'serult'),
('Tennessee', 'Surigao Strait', 'Ok'),
('Washington', 'Guadalcanal', 'Ok'),
('West Virginia', 'Surigao Strait', 'Ok'),
('Yamashiro', 'Surigao Strait', 'elsulyedt');